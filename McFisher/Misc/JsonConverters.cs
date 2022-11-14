using Newtonsoft.Json;
using System.Globalization;

namespace McFisher.Misc;

public static class JsonConverters
{
    public static readonly FloatJsonConverter Float = new();
    public static readonly DoubleJsonConverter Double = new();

    public static readonly JsonConverter[] All = new JsonConverter[]
    {
        Float, Double
    };
}

public class FloatJsonConverter : JsonConverter<float>
{
    public override bool CanRead => false;

    public override void WriteJson(JsonWriter writer, float value, JsonSerializer serializer)
    {
        var s = value.ToString("0.00000000", CultureInfo.InvariantCulture);
        writer.WriteRawValue(s);
    }

    public override float ReadJson(JsonReader reader, Type objectType, float existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        throw new NotSupportedException();
    }
}

public class DoubleJsonConverter : JsonConverter<double>
{
    public override bool CanRead => false;

    public override void WriteJson(JsonWriter writer, double value, JsonSerializer serializer)
    {
        var s = value.ToString("0.00000000000000", CultureInfo.InvariantCulture);
        writer.WriteRawValue(s);
    }

    public override double ReadJson(JsonReader reader, Type objectType, double existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        throw new NotSupportedException();
    }
}
