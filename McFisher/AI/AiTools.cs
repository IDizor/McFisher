using System.IO;

namespace McFisher.AI;

/// <summary>
/// Helper class.
/// </summary>
public static class AiTools
{
    public static readonly Random R = new();

    public static int RandomSign => R.NextSingle() > 0.5f ? 1 : -1;

    public static float RandomFloat => R.NextSingle();

    public static float RandomPower => (float)Math.Pow(R.NextSingle(), 0.5) * RandomSign;
    
    public static int RandomInt(int minValue, int maxValue, bool randomSign = false)
    {
        var val = R.Next(minValue, maxValue + 1);

        if (randomSign)
        {
            val *= RandomSign;
        }

        return val;
    }

    public static float Random(float limit)
    {
        return limit == 0f ? 0f : R.NextSingle() * limit;
    }

    public static double Random(double limit)
    {
        return limit == 0d ? 0d : R.NextDouble() * limit;
    }

    public static int RandomMutationsCount(int synapseCount, int errorsCount)
    {
        // Excel formula: =2+ROUND((($B3^0.3)/4)*(C$2/800 + 1)+(C$2/800);0)
        //float sc = synapseCount / 800f;
        //int count = 2 + (int)Math.Round(Math.Pow(errorsCount, 0.3) / 4 * (sc + 1) + sc);
        //float sc = synapseCount / 500f;
        //int count = 1 + (int)Math.Round(Math.Pow(errorsCount, 0.5) / 10 * (sc + 1) + sc);

        int count = (int)(Math.Pow(errorsCount, 0.18) + Math.Pow(synapseCount, 0.25));

        // randomize
        int deviation = (int)Math.Max(count / 7f, 1f);
        int result = RandomInt(Math.Max(count - deviation, 1), count + deviation);

        return result;
    }

    // Excel formula: =MAX(1;ROUND((5000-InitialErrorsCount)/1000;0))
    private static readonly int[] LimitsPerErrorCounts = Enumerable.Range(0, 3500)
        .Select(v => Math.Max(1, (int)Math.Round((5000f - v) / 1000f)))
        .ToArray();

    public static int SurvivalsLimitPerErrorsCount(int errorCount)
    {
        if (errorCount >= LimitsPerErrorCounts.Length)
        {
            return 1;
        }

        return LimitsPerErrorCounts[errorCount];
    }

    public static bool DirExists(string dir)
    {
        var path = Path.GetFullPath(dir);
        var dirs = Directory.GetDirectories(Path.GetDirectoryName(path));
        if (dirs.Any())
        {
            dirs = dirs.Select(d => Path.GetFileName(d)).ToArray();
            dir = Path.GetFileName(dir);
            var result = dirs.Any(d => d.StartsWith(dir, StringComparison.InvariantCultureIgnoreCase));

            return result;
        }

        return false;
    }

    public static bool FileExists(string file)
    {
        var path = Path.GetFullPath(file);
        var files = Directory.GetFiles(Path.GetDirectoryName(path));
        if (files.Any())
        {
            files = files.Select(d => Path.GetFileName(d)).ToArray();
            file = Path.GetFileNameWithoutExtension(file) + ".";
            var result = files.Any(d => d.StartsWith(file, StringComparison.InvariantCultureIgnoreCase));

            return result;
        }

        return false;
    }
}
