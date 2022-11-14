using Newtonsoft.Json;

namespace McFisher.AI;

public class Synapse
{
    public float Power { get; set; }
    public Guid From { get; set; }
    public Guid To { get; set; }

    [JsonIgnore]
    public Neuron FromNeuron { get; set; }
    [JsonIgnore]
    public Neuron ToNeuron { get; set; }

    public Synapse()
    {
    }

    public Synapse(Neuron fromNeuron, Neuron toNeuron)
    {
        Power = AiTools.RandomPower;
        FromNeuron = fromNeuron;
        ToNeuron = toNeuron;
        From = FromNeuron.Id;
        To = ToNeuron.Id;
    }
}
