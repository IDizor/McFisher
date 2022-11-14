using Newtonsoft.Json;

namespace McFisher.Training;

public class TrainingBlock
{
    [JsonProperty]
    public int TimeMs { get; set; }

    [JsonProperty]
    public bool IsGoal { get; set; }

    [JsonProperty]
    public float[] Values { get; set; }

    public TrainingBlock(float[] values, bool isGoal = false)
    {
        TimeMs = 0;
        Values = values;
        IsGoal = isGoal;
    }
}