using System.Text.Json;
using System.Text.Json.Nodes;

namespace AOC2015._12;

public class Day12B : Day
{
    protected override string Run()
    {
        var input = GetInputAsString();
        var jsonObj = JsonNode.Parse(input).AsObject();
        var sum = CalculateSum(jsonObj);
        
        return sum.ToString();
    }

    private static int CalculateSum(JsonNode? jn)
    {
        if (jn is JsonArray)
        {
            return jn.AsArray().Sum(CalculateSum);
        }
        else if (jn is JsonObject)
        {
            var obj = jn.AsObject();
            if (obj.Any(kvp => IsObjectWithRed(kvp.Value)))
            {
                return 0;
            }
            return jn.AsObject().Sum(kvp => CalculateSum(kvp.Value));
        }
        else
        {
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(jn.ToJsonString());
            if (jsonElement.ValueKind == JsonValueKind.Number)
            {
                return jsonElement.GetInt32();
            }
        }
        
        return 0;
    }

    private static bool IsObjectWithRed(JsonNode? jn)
    {
        switch (jn)
        {
            case JsonArray:
                return false;
            case JsonObject:
                return false;
            default:
            {
                var jsonElement = JsonSerializer.Deserialize<JsonElement>(jn.ToJsonString());
                if (jsonElement.ValueKind == JsonValueKind.String)
                {
                    return "red".Equals(jsonElement.GetString());
                }

                break;
            }
        }

        return false;
    }
}