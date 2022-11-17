using McFisher.Training;

namespace McFisher.AI;

public class AiConfig
{
    public int GenerationPopulationLimit { get; set; } = 20000;
    public int ThreadsCount { get; set; } = 5;
    public int GenerationMomentum { get; set; } = 2000;
    public string EvolutionDir { get; set; } = "";
    public TrainingSet Training { get; set; } = null;
    public List<AiBrain> Prototypes { get; set; } = new();
    public int ErrorsGoal { get; set; } = 5;
    public float AcceptableErrorsThreshold { get; set; } = 0.01f;
    public float NeuronsMemory { get; set; } = 0.7f;
    public int FrequenciesBlocksToUse { get; set; } = 0;
}
