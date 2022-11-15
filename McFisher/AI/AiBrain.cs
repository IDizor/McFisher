using McFisher.Training;
using Newtonsoft.Json;

namespace McFisher.AI;

public class AiBrain
{
    private bool _populated = false;
    private int _fatalErrorValue = 50;

    [JsonProperty]
    public int Generation { get; set; } = 0;

    [JsonProperty]
    public int PerceptronsCount { get; set; } = 0;

    [JsonProperty]
    public int SynapsesCount => Synapses.Count;

    [JsonProperty]
    public int NeuronsCount => Neurons.Count;

    [JsonProperty]
    public float NeuronsMemory { get; set; } = 0f;

    [JsonProperty]
    public int LastLayerIndex => ResultNeuron.Layer;

    [JsonProperty]
    public List<Synapse> Synapses { get; set; } = new();

    [JsonProperty]
    public List<Neuron> Neurons { get; set; } = new();

    [JsonProperty]
    public int ErrorsCount { get; set; }

    [JsonIgnore]
    public IEnumerable<Neuron> Hidden => Neurons.Where(n => n.Layer > 0 && n.Layer < LastLayerIndex);

    [JsonIgnore]
    public string? Info { get; set; }

    [JsonIgnore]
    public Neuron ResultNeuron { get; set; }

    [JsonIgnore]
    public bool Result => ResultNeuron.Result > 0.5f;

    public AiBrain()
    {
    }

    public static AiBrain CreateEmptyBrain(int perceptronsCount)
    {
        var brain = new AiBrain();
        brain.PerceptronsCount = perceptronsCount;

        // create perceptrons with random memory power
        for (int i = 0; i < brain.PerceptronsCount; i++)
        {
            brain.Neurons.Add(new()
            {
                Id = Guid.NewGuid(),
                Layer = 0,
                Memory = 0f,
            });
        }

        // create result neuron
        brain.ResultNeuron = new()
        {
            Id = Guid.NewGuid(),
            Layer = brain.Neurons.Max(n => n.Layer) + 1,
            Memory = 0f,
        };

        brain.Neurons.Add(brain.ResultNeuron);

        return brain;
    }

    public AiBrain Clone()
    {
        var clone = this.JsonClone();
        clone.ResultNeuron = clone.Neurons.Single(n => n.Id == ResultNeuron.Id);
        clone.Populate();

        return clone;
    }

    public void Populate()
    {
        if (!_populated)
        {
            foreach (var synapse in Synapses)
            {
                var fromNeuron = Neurons.First(n => n.Id == synapse.From);
                var toNeuron = Neurons.First(n => n.Id == synapse.To);
                synapse.FromNeuron = fromNeuron;
                synapse.ToNeuron = toNeuron;
                fromNeuron.Outputs.Add(synapse);
                toNeuron.Inputs.Add(synapse);
            }

            ResultNeuron = Neurons.MaxBy(n => n.Layer);
            _populated = true;
        }
    }

    public void ProcessSignals(float[] signals1, float[] signals2)
    {
        // put signals into perceptrons
        for (int i = 0; i < PerceptronsCount; i++)
        {
            if (Neurons[i].Layer != 0)
            {
                throw new Exception("Too many signals to process. Not enough perceptrons in this AI.");
            }

            if (i < signals1.Length)
            {
                Neurons[i].SetResult(signals1[i]);
            }
            else
            {
                int j = i - signals1.Length;
                if (signals2 != null && j < signals2.Length)
                {
                    Neurons[i].SetResult(signals2[j]);
                }
                else
                {
                    break;
                }
            }
        }

        // resolve
        try
        {
            Neurons.Where(n => n.Layer > 0)
                .OrderBy(n => n.Layer)
                .ToList()
                .ForEach(n => n.Resolve());
        }
        catch (Exception e)
        {
            this.SaveAsJsonPretty($"jsons\\errors\\{Neurons[0].Id}.json");
            new Thread(() => MessageBox.Show(e.Message, "Error")).Start();
            throw;
        }
    }

    /// <summary>
    /// Processes the training data and sets <see cref="ErrorsCount"/>.
    /// </summary>
    public AiBrain ProcessTraining(AiConfig config)
    {
        ErrorsCount = 0;
        var goalZone = false;
        var goalZoneAlreadyHit = false;

        // process training data
        for (int i = config.FrequenciesPrevMax; i < config.Training.Blocks.Count; i++)
        {
            // track goal zone
            if (goalZone)
            {
                if (!config.Training.Blocks[i].IsGoal)
                {
                    goalZone = false;
                }
            }
            else
            {
                if (config.Training.Blocks[i].IsGoal)
                {
                    goalZone = true;
                    goalZoneAlreadyHit = false;
                    ErrorsCount += _fatalErrorValue;
                }
            }

            // process signals
            if (config.FrequenciesPrevMax > 0)
            {
                ProcessSignals(config.Training.Blocks[i].Values, config.Training.Blocks[i - config.FrequenciesPrevMax].Values);
            }
            else
            {
                ProcessSignals(config.Training.Blocks[i].Values, null);
            }
            

            if (goalZone)
            {
                if (!goalZoneAlreadyHit && Result)
                {
                    goalZoneAlreadyHit = true;
                    ErrorsCount -= _fatalErrorValue;
                }
            }
            else
            {
                if (Result)
                {
                    ErrorsCount++;
                }
            }
        }

        return this;
    }

    public void MutateSynapse()
    {
        var synapse = Synapses.RandomItem();
        synapse.Power = AiTools.RandomPower;
    }

    public void MutateNeuron()
    {
        var neuron = Hidden.ToList().RandomItem();
        neuron.Memory = AiTools.Random(NeuronsMemory);
    }

    public void AddRandomNeuron(AiConfig config)
    {
        // useful neuron should have at least 2 inputs
        var fromNeuron1 = GetRandomNeuron(GetRandomLayerForNewNeuron(0, LastLayerIndex - 1), config);
        var fromNeuron2 = fromNeuron1;

        while (fromNeuron2 == fromNeuron1)
        {
            fromNeuron2 = GetRandomNeuron(GetRandomLayerForNewNeuron(0, LastLayerIndex - 1), config);
        }

        // useful neuron should have at least 1 output
        var fromLayerMax = Math.Max(fromNeuron1.Layer, fromNeuron2.Layer);
        var toNeuron = GetRandomNeuron(GetRandomLayerForNewNeuron(fromLayerMax + 1, LastLayerIndex));

        var newNeuron = new Neuron()
        {
            Id = Guid.NewGuid(),
            Layer = fromLayerMax + 1,
            Memory = AiTools.Random(NeuronsMemory),
        };

        if (newNeuron.Layer == toNeuron.Layer)
        {
            InsertNewLayer(toNeuron.Layer);
        }

        Neurons.Add(newNeuron);

        // create synapses
        Synapses.Add(new Synapse(fromNeuron1, newNeuron));
        Synapses.Add(new Synapse(fromNeuron2, newNeuron));
        Synapses.Add(new Synapse(newNeuron, toNeuron));
    }

    public void AddRandomSynapse(AiConfig config, int attemptsLimit = 1000)
    {
        var attempt = 0;
        Synapse? synapse = null;

        while (synapse == null && attempt < attemptsLimit)
        {
            attempt++;
            var fromLayer = AiTools.RandomInt(0, LastLayerIndex - 1);
            var fromNeuron = GetRandomNeuron(fromLayer, config);
            var toLayer = AiTools.RandomInt(fromLayer + 1, LastLayerIndex);
            var toNeuron = GetRandomNeuron(toLayer);

            if (!Synapses.Any(s => s.From == fromNeuron.Id && s.To == toNeuron.Id))
            {
                synapse = new Synapse()
                {
                    From = fromNeuron.Id,
                    FromNeuron = fromNeuron,
                    To = toNeuron.Id,
                    ToNeuron = toNeuron,
                    Power = AiTools.RandomPower,
                };

                fromNeuron.Outputs.Add(synapse);
                toNeuron.Inputs.Add(synapse);
                Synapses.Add(synapse);
            }
        }
    }

    public void RemoveRandomHiddenNeuron()
    {
        var hidden = Hidden.ToArray();

        if (hidden.Length > 1)
        {
            var neuronToRemove = hidden.RandomItem();
            var tempClone = Clone();

            tempClone.RemoveNeurons(tempClone.Neurons.Where(n => n.Id == neuronToRemove.Id));
            tempClone.RemoveUselessNeurons();
            if (tempClone.ResultNeuron.Inputs.Count > 0)
            {
                tempClone = null;
                RemoveNeurons(new Neuron[] { neuronToRemove });
                RemoveUselessNeurons();
            }
        }
    }

    public void RemoveRandomSynapse()
    {
        if (SynapsesCount > 1)
        {
            var synapseToRemove = Synapses.RandomItem();
            var tempClone = Clone();

            tempClone.RemoveSynapses(tempClone.Synapses.Where(s => s.Equals(synapseToRemove)));
            tempClone.RemoveUselessNeurons();
            if (tempClone.ResultNeuron.Inputs.Count > 0)
            {
                tempClone = null;
                RemoveSynapses(new Synapse[] { synapseToRemove });
                RemoveUselessNeurons();
            }
        }
    }

    private int GetRandomLayerForNewNeuron(int minLayer, int maxLayer)
    {
        var layersChances = new List<float>();
        for (int i = minLayer; i <= maxLayer; i++)
        {
            layersChances.Add((float)Math.Pow(Neurons.Count(n => n.Layer == i), 0.3));
        }
        return layersChances.GetRandomIndex() + minLayer;
    }

    private Neuron GetRandomNeuron(int layer, AiConfig? config = null)
    {
        return layer == 0 && config != null
            ? Neurons[config.Training.GetRandomSynapseIndex(config.FrequenciesPrevMax > 0)]
            : Neurons.Where(n => n.Layer == layer).ToList().RandomItem();
    }

    private void RemoveUselessNeurons()
    {
        var neuronsWithNoInputs = Hidden.Where(n => n.Inputs.Count == 0).ToArray();
        var neuronsWithNoOutputs = Hidden.Where(n => n.Outputs.Count == 0).ToArray();
        var neuronsToRemove = neuronsWithNoInputs.Concat(neuronsWithNoOutputs).ToArray();

        if (neuronsToRemove.Length > 0)
        {
            RemoveNeurons(neuronsToRemove);

            // recursive check for useless neurons
            RemoveUselessNeurons();
        }
        else
        {
            RemoveEmptyLayers();
        }
    }

    private void RemoveEmptyLayers()
    {
        for (int i = LastLayerIndex - 1; i > 0; i--)
        {
            if (Neurons.All(n => n.Layer != i))
            {
                foreach (var neuron in Neurons)
                {
                    if (neuron.Layer > i)
                    {
                        neuron.Layer--;
                    }
                }
            }
        }
    }

    private void RemoveNeurons(IEnumerable<Neuron> neurons)
    {
        if (neurons.Count() > 0)
        {
            var synapsesToRemove = neurons.SelectMany(n => n.Inputs).ToList();
            synapsesToRemove.AddRange(neurons.SelectMany(n => n.Outputs));

            RemoveSynapses(synapsesToRemove);
            Neurons.RemoveAll(n => neurons.Contains(n));
        }
    }

    private void RemoveSynapses(IEnumerable<Synapse> synapses)
    {
        if (synapses.Count() > 0)
        {
            // remove from inputs
            Neurons.ForEach(n => n.Inputs.RemoveAll(i => synapses.Contains(i)));

            // remove from outputs
            Neurons.ForEach(n => n.Outputs.RemoveAll(o => synapses.Contains(o)));

            // remove synapses
            Synapses.RemoveAll(s => synapses.Contains(s));
        }
    }

    private void InsertNewLayer(int beforeLayer)
    {
        if (beforeLayer == 0)
        {
            throw new Exception("Unable to shift perceptrons layer.");
        }

        foreach (var neuron in Neurons)
        {
            if (neuron.Layer >= beforeLayer)
            {
                neuron.Layer++;
            }
        }
    }
}
