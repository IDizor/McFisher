using McFisher.AI;
using Newtonsoft.Json;
using ScottPlot.Palettes;

namespace McFisher.Training;

public class TrainingSet
{
    private float[] _chancesSource;
    private List<int> _chancesZeros = new();
    private object _chansesLock = new object();

    [JsonProperty]
    public int Count { get; set; }

    [JsonProperty]
    public int BlockSize { get; set; }

    [JsonProperty]
    public int TotalTimeMs { get; set; }

    [JsonProperty]
    public List<TrainingBlock> Blocks { get; set; }

    [JsonIgnore]
    public DateTime? StartDateTime = null;

    public TrainingSet()
    {
        TotalTimeMs = 0;
        Blocks = new List<TrainingBlock>();
    }

    public TrainingBlock? AddBlock(float[] values, bool isGoal, TrainingSet? referenceTraining)
    {
        if (StartDateTime != null || values.Any(v => v > 0f))
        {
            if (StartDateTime == null)
            {
                StartDateTime = DateTime.Now;
            }

            var referenceGoal = false;
            var trainingBlock = new TrainingBlock(values);
            trainingBlock.TimeMs = (int)(DateTime.Now - StartDateTime.Value).TotalMilliseconds;
            if (referenceTraining != null)
            {
                var referenceBlock = referenceTraining.Blocks
                    .FirstOrDefault(b => b.TimeMs > trainingBlock.TimeMs - 10 && b.TimeMs < trainingBlock.TimeMs + 500);
                referenceGoal = referenceBlock?.IsGoal == true;
            }
            trainingBlock.IsGoal = isGoal || referenceGoal;

            Blocks.Add(trainingBlock);
            return trainingBlock;
        }

        return null;
    }

    public void Complete()
    {
        // trim end
        for (int i = Blocks.Count - 1; i >= 0; i--)
        {
            if (Blocks[i].Values.All(v => v == 0))
            {
                Blocks.RemoveAt(i);
            }
        }

        // update props
        Count = Blocks.Count;
        if (Blocks.Count > 0)
        {
            BlockSize = Blocks[0].Values.Length;
            TotalTimeMs = Blocks[^1].TimeMs;
        }
    }

    public int GetRandomSynapseIndex(bool isDoubleBlockSize)
    {
        lock (_chansesLock)
        {
            if (_chancesSource == null)
            {
                _chancesSource = isDoubleBlockSize
                    ? new float[BlockSize * 2]
                    : new float[BlockSize];

                // select maximums
                foreach (var block in Blocks)
                {
                    for (int i = 0; i < BlockSize; i++)
                    {
                        _chancesSource[i] = Math.Max(_chancesSource[i], block.Values[i]);
                        if (isDoubleBlockSize)
                        {
                            _chancesSource[BlockSize + i] = _chancesSource[i];
                        }
                    }
                }

                // sum sequence
                for (int i = 1; i < _chancesSource.Length; i++)
                {
                    if (_chancesSource[i] == 0f)
                    {
                        _chancesSource[i] = 0.000001f;
                        _chancesZeros.Add(i);
                    }

                    _chancesSource[i] += _chancesSource[i - 1];
                }
            }

            var index = 0;

            do
            {
                var random = AiTools.Random(_chancesSource[^1]);
                var rolledValue = _chancesSource.Where(v => v >= random).Min();
                index = Array.IndexOf(_chancesSource, rolledValue);
            } while (_chancesZeros.Contains(index));

            return index;
        }
    }
}
