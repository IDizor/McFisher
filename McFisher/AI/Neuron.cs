using Newtonsoft.Json;

namespace McFisher.AI;

public class Neuron
{
    [JsonProperty]
    public Guid Id { get; set; }
    [JsonProperty]
    public int Layer { get; set; }
    [JsonProperty]
    public float Memory { get; set; } // 0 .. 0.99
    [JsonIgnore]
    public float Result { get; private set; }
    [JsonIgnore]
    public List<Synapse> Inputs { get; set; } = new();
    [JsonIgnore]
    public List<Synapse> Outputs { get; set; } = new();

    public void SetResult(float value)
    {
        Result = value > 0
            ? Math.Max(Result * Memory, value)
            : Math.Min(Result * Memory, value);
    }

    public void Resolve()
    {
        if (Inputs.Count == 0)
        {
            throw new Exception("Neuron has no inputs.");
        }

        var result = Inputs.Sum(i => i.Power * i.FromNeuron.Result);
        SetResult(result);
    }
}
