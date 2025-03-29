using AOC.Common;

namespace AOC2024._23;

public class Day23B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        var graph = new Graph<string>();
        foreach (var row in input)
        {
            var com = row.Split("-");
            
            var left = graph.AddOrGetIfExists(com[0]);
            var right = graph.AddOrGetIfExists(com[1]);
            graph.AddUndirectedEdge(left, right);
        }

        var previousResult = new List<List<string>>();
        for (var c = 2; c < graph.Count; c++)
        {
            PrintElapsedTime(c);
            var result = new List<List<string>>();
            for (var i = 0; i < graph.Count - c; i++)
            {
                result.AddRange(LoopN(graph, i, c - 1, [graph[i].Value]));    
            }

            if (result.Count == 0) break;
            if (result.Select(r => r.Count).Max() < c + 1) break;
            
            previousResult = result;
        }
        
        var max = previousResult.MaxBy(r => r.Count)!;
        max.Sort();
        return string.Join(',', max);
    }

    private List<List<string>> LoopN(Graph<string> graph, int index, int countModifier, List<string> computers)
    {
        var result = new List<List<string>>();
        if (countModifier < 0) return result;
        
        for (var i = index + 1; i < graph.Count - countModifier; i++)
        {
            var node = graph[i];
            if (computers.All(c => node.Neighbors.Any(n => n.Value == c)))
            {
                var r = computers.ToList();
                r.Add(node.Value);
                result.Add(r);

                var temp = LoopN(graph, i, countModifier - 1, r);
                result.AddRange(temp);
            }
        }
        
        return result;
    }
}