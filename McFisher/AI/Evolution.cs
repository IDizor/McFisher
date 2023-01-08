using System.IO;
using McFisher.Training;

namespace McFisher.AI;

public class Evolution
{
    public int Generation { get; private set; } = 0;
    public int PopulationPrev { get; private set; } = 0;
    public int PopulationTotal { get; private set; } = 0;
    public int ErrorsCount { get; private set; } = int.MaxValue;
    public int ErrorsCountPrev { get; private set; } = int.MaxValue;
    public int SurvivalsCount { get; private set; } = 0;
    public AiConfig? Config { get; private set; }

    public AiBrain? BestMutant { get; private set; } = null;
    public List<Mutator> Mutators = new();
    public bool Running { get; private set; } = false;
    public bool StopRequested { get; set; } = false;

    private Task? Task;

    public Evolution(AiConfig config)
    {
        if (config == null)
        {
            throw new Exception("Config is null.");
        }

        Config = config;
        Config.EvolutionDir = Config.EvolutionDir.IsEmpty()
            ? GetEvolutionDirPath()
            : Path.GetRelativePath(".", Config.EvolutionDir);
    }

    private static string GetEvolutionDirPath()
    {
        int n = 0;
        string dir;

        do
        {
            dir = $"jsons\\Evolution-{n++:000}";
        } while (AiTools.DirExists(dir));

        return dir;
    }

    public void Start()
    {
        if (Task == null)
        {
            Running = true;
            StopRequested = false;

            Task = Task.Factory.StartNew(() =>
            {
                var parents = new List<AiBrain>(); // count is always equals to ThreadsCount

                if (Config.Prototypes.Any())
                {
                    Generation = Config.Prototypes[0].Generation;
                    PopulateParents(ref parents, Config.Prototypes, Config.ThreadsCount);
                    parents.ForEach(p => p.Populate());
                    parents.ForEach(p => p.ProcessTraining(Config));
                    ErrorsCount = parents.Min(p => p.ErrorsCount);
                }
                else
                {
                    Generation = -1;
                }

                BestMutant = null;
                PopulationTotal = 0;
                int mutatorLimit = Config.GenerationPopulationLimit / Config.ThreadsCount;

                // evolution loop
                while (Running && !StopRequested)
                {
                    Generation++;
                    int mutatedTotalWhenFound = 0;
                    bool generationCompleted = false;
                    PopulationPrev = PopulationTotal;
                    var acceptableErrorsCount = ErrorsCount - Math.Max(1, (int)(ErrorsCount * Config.AcceptableErrorsThreshold));
                    ErrorsCountPrev = ErrorsCount;

                    // start mutators
                    Mutators.Clear();
                    for (int i = 0; i < Config.ThreadsCount; i++)
                    {
                        if (parents.Any())
                            Mutators.Add(Mutator.StartNew(parents[i], Config));
                        else
                            Mutators.Add(Mutator.StartNew(null, Config));
                    }

                    // generation loop
                    while (!generationCompleted && Running)
                    {
                        Task.Delay(500).Wait();

                        var mutatorsErrorsCount = Mutators.Min(m => m.ErrorsCount);
                        var mutatedTotal = Mutators.Sum(m => m.MutatedTotal);

                        // update errors count and population total
                        ErrorsCount = Math.Min(ErrorsCount, mutatorsErrorsCount);
                        PopulationTotal = PopulationPrev + mutatedTotal;
                        SurvivalsCount = Mutators.Sum(m => m.Survivals.Count);

                        // check errors and save mutated total
                        if (mutatedTotalWhenFound == 0 && mutatorsErrorsCount <= acceptableErrorsCount)
                        {
                            mutatedTotalWhenFound = Mutators.Sum(m => m.MutatedTotal);
                        }

                        // finish generation if mutated total exceeded momentum size
                        if (mutatedTotalWhenFound > 0 && mutatedTotal - mutatedTotalWhenFound >= Config.GenerationMomentum)
                        {
                            generationCompleted = true;
                            StopGeneration();
                        }

                        // finish generation if all mutators completed
                        if (Mutators.All(m => !m.Running))
                        {
                            generationCompleted = true;
                        }

                        // find the best mutant
                        var bestMutants = Mutators
                            .Where(m => m.BestMutant != null)
                            .Select(m => m.BestMutant)
                            .ToArray();

                        if (bestMutants.Any())
                        {
                            var bestMutant = bestMutants
                                .OrderBy(m => m.ErrorsCount)
                                .FirstOrDefault();

                            if (BestMutant == null || BestMutant.ErrorsCount > bestMutant.ErrorsCount)
                            {
                                BestMutant = bestMutant;
                            }
                        }
                    }

                    if (!Running)
                    {
                        StopGeneration();
                    }

                    ErrorsCount = Math.Min(ErrorsCount, Mutators.Min(m => m.ErrorsCount));

                    var survivals = new List<AiBrain>();

                    foreach (var mutator in Mutators)
                    {
                        if (mutator.Survivals.Any())
                        {
                            survivals.AddRange(mutator.Survivals);
                        }
                    }

                    if (survivals.Any())
                    {
                        survivals = survivals
                            .OrderByDescending(m => m.ErrorsCount)
                            .ToList();

                        // save generation
                        survivals.ForEach(m => m.Generation = Generation);
                        survivals.SaveAsJson($"{Config.EvolutionDir}\\Gen{Generation:000}.E{ErrorsCount:0000}.json");

                        // check goal
                        if (ErrorsCount <= Config.ErrorsGoal)
                        {
                            Running = false;
                        }
                        else if (Running)
                        {
                            // prepare parents for next generation
                            PopulateParents(ref parents, survivals, Config.ThreadsCount);
                        }
                    }
                    else
                    {
                        Running = false;
                    }
                }

                if (StopRequested)
                {
                    Running = false;
                }

                // ---------------------------------------------------------------------------
                // --- local methods ---------------------------------------------------------
                // ---------------------------------------------------------------------------

                void StopGeneration()
                {
                    Mutators.ForEach(m => m.Stop());
                    Mutators.ForEach(m => m.Task?.Wait());
                }
            }, TaskCreationOptions.LongRunning);
        }
    }

    private static void PopulateParents(ref List<AiBrain> parents, List<AiBrain> mutants, int limit)
    {
        parents.Clear();

        if (mutants.Count <= limit)
        {
            for (int i = 0; i < limit; i++)
            {
                parents.Add(mutants[i % mutants.Count]);
            }
        }
        else
        {
            int minProcessed = -1;

            while (parents.Count < limit)
            {
                int needToAdd = limit - parents.Count;
                int minErrorsCount = mutants.Where(m => m.ErrorsCount > minProcessed).Min(m => m.ErrorsCount);
                var selectedParents = mutants.Where(m => m.ErrorsCount == minErrorsCount).ToList();

                if (selectedParents.Count > needToAdd)
                {
                    // select randoms from selectedParents
                    while (parents.Count < limit)
                    {
                        var p = selectedParents.RandomItem();
                        if (!parents.Contains(p))
                        {
                            parents.Add(p);
                        }
                    }

                    break;
                }
                else
                {
                    parents.AddRange(selectedParents);
                    minProcessed = minErrorsCount;
                }
            }
        }
    }

    public void Stop()
    {
        Running = false;
        Task?.Wait();
    }
}
