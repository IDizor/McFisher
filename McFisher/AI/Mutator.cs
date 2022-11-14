using System.Collections.Concurrent;

namespace McFisher.AI;

public class Mutator
{
    public Task? Task { get; private set; }
    public AiBrain? Prototype { get; private set; }
    public AiConfig? Config { get; set; } = null;
    public int MutatedTotal { get; private set; }
    public int TotalLimit { get; set; }
    public int InitialErrorsCount { get; private set; }
    public int ErrorsCount { get; private set; }
    public bool Running { get; private set; }

    public ConcurrentBag<AiBrain> Survivals = new ConcurrentBag<AiBrain>();
    public AiBrain? BestMutant { get; private set; } = null;

    public static int SurvivalsTruncLimit { get; set; } = 100;
    private static float[] MutationTypeChances = {
        0.32f,   // add neuron
        0.55f,   // add synapse
        0.55f,   // mutate synapse
        0.06f,   // mutate neuron memory power
    };

    public static Mutator StartNew(AiBrain prototype, AiConfig config)
    {
        prototype?.Populate();

        var mutator = new Mutator
        {
            Config = config,
            Prototype = prototype,
            TotalLimit = config.GenerationPopulationLimit / config.ThreadsCount,
            ErrorsCount = int.MaxValue,
        };

        mutator.Mutate();

        return mutator;
    }

    public void Stop()
    {
        Running = false;
    }

    private void Mutate()
    {
        if (Task == null)
        {
            Task = Task.Factory.StartNew(() =>
            {
                var hasPrototype = Prototype != null;
                InitialErrorsCount = hasPrototype ? Prototype.ErrorsCount : int.MaxValue;
                ErrorsCount = InitialErrorsCount;

                var mutationsCount = hasPrototype
                    ? AiTools.RandomMutationsCount(Prototype.SynapsesCount, Prototype.ErrorsCount)
                    : 2;

                Running = true;
                while (Running)
                {
                    var mutant = hasPrototype
                        ? Prototype.Clone()
                        : AiBrain.CreateEmptyBrain(Config.Training.BlockSize * (Config.FrequenciesPrevMax > 0 ? 2 : 1));

                    mutant.NeuronsMemory = Config.NeuronsMemory;

                    for (int i = 0; i < mutationsCount; i++)
                    {
                        if (mutant.SynapsesCount == 0)
                        {
                            mutant.AddRandomSynapse(Config);
                        }
                        else
                        {
                            int mutationType = 1;
                            int hiddenCount = mutant.Hidden.Count();
                            do
                            {
                                mutationType = MutationTypeChances.GetRandomIndex();
                            } while ((mutationType == 3 && hiddenCount == 0)
                                  || (mutationType == 3 && Config.NeuronsMemory == 0f));

                            switch (mutationType)
                            {
                                case 0:
                                    mutant.AddRandomNeuron(Config);
                                    break;
                                case 1:
                                    mutant.AddRandomSynapse(Config);
                                    break;
                                case 2:
                                    mutant.MutateSynapse();
                                    break;
                                default:
                                    mutant.MutateNeuron();
                                    break;
                            }
                        }
                    }

                    mutant.ProcessTrainingData(Config);
                    MutatedTotal++;

                    // check errors and save best mutants
                    if (mutant.ErrorsCount < InitialErrorsCount &&
                        Survivals.Count(m => m.ErrorsCount == mutant.ErrorsCount) < AiTools.SurvivalsLimitPerErrorsCount(mutant.ErrorsCount))
                    {
                        Survivals.Add(mutant);

                        if (ErrorsCount > mutant.ErrorsCount)
                        {
                            BestMutant = mutant;
                        }

                        ErrorsCount = Math.Min(ErrorsCount, mutant.ErrorsCount);

                        // truncate worst survivals
                        if (Survivals.Count > SurvivalsTruncLimit)
                        {
                            var bests = Survivals
                                .OrderBy(s => s.ErrorsCount)
                                .Take(SurvivalsTruncLimit / 2)
                                .ToArray();
                            Survivals = new ConcurrentBag<AiBrain>(bests);
                        }
                    }
                    else if (mutant.ErrorsCount == 0)
                    {
                        // stop mutator when best mutants found
                        Running = false;
                    }

                    if (TotalLimit > 0 && MutatedTotal >= TotalLimit)
                    {
                        Running = false;
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}

