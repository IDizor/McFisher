using System.IO;
using McFisher.AI;
using McFisher.Misc;
using Newtonsoft.Json;

namespace McFisher;

public static class Extensions
{
    private static void CreateFilePath(string fileName)
    {
        var dir = Path.GetDirectoryName(fileName);
        if (dir?.Length > 0)
        {
            Directory.CreateDirectory(dir);
        }
    }

    public static bool IsEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    public static void Restart(this System.Windows.Forms.Timer timer)
    {
        timer.Stop();
        timer.Start();
    }

    public static int IntValue(this NumericUpDown upDown)
    {
        return (int)upDown.Value;
    }

    public static T RandomItem<T>(this IList<T> list)
    {
        return list[AiTools.RandomInt(0, list.Count - 1)];
    }

    public static string ReadAsTextFile(this string fileName)
    {
        if (File.Exists(fileName))
        {
            return File.ReadAllText(fileName);
        }

        return string.Empty;
    }

    public static void SaveToFile(this string data, string fileName)
    {
        CreateFilePath(fileName);
        File.WriteAllText(fileName, data);
    }

    public static T ParseJsonFile<T>(this string fileName)
    {
        return JsonConvert.DeserializeObject<T>(fileName.ReadAsTextFile());
    }

    public static void SaveAsJson(this object obj, string fileName)
    {
        CreateFilePath(fileName);
        JsonConvert.SerializeObject(obj, Formatting.None, JsonConverters.All).SaveToFile(fileName);
    }

    public static void SaveAsJsonPretty(this object obj, string fileName)
    {
        CreateFilePath(fileName);
        JsonConvert.SerializeObject(obj, Formatting.Indented, JsonConverters.All).SaveToFile(fileName);
    }

    public static T JsonClone<T>(this T obj)
    {
        return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
    }

    public static int GetRandomIndex(this IEnumerable<float> array)
    {
        var chances = array.ToArray();

        if (chances.Length == 1)
        {
            return 0;
        }

        var zeros = new List<int>();

        for (int i = 1; i < chances.Length; i++)
        {
            if (chances[i] == 0f)
            {
                chances[i] = 0.000001f;
                zeros.Add(i);
            }

            chances[i] += chances[i - 1];
        }

        var index = 0;

        do
        {
            var random = AiTools.Random(chances[^1]);
            var rolledValue = chances.Where(v => v >= random).Min();
            index = Array.IndexOf(chances, rolledValue);
        } while (zeros.Contains(index));

        return index;
    }
}
