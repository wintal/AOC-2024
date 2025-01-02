using System.Collections.Immutable;
using System.Data;

namespace Day23;


class Program
{
    private const string Sample = "sample.txt";
    private const string Input = "input.txt";
    static void Main(string[] args)
    {
        RunPart1(Sample);
        RunPart1(Input);
        RunPart2(Sample);
        RunPart2(Input);
    }

    class Node
    {
        public string Name;
        public HashSet<Node> Connections = new();
    }
    
    

    static void RunPart1(string inputFile)
    {
        var start = DateTime.Now;

        long result = 0;
        var lines = System.IO.File.ReadAllLines(inputFile);

        Dictionary<string, Node> nodes = new();
        foreach (var line in lines)
        {
            var parts = line.Split("-", StringSplitOptions.RemoveEmptyEntries| StringSplitOptions.TrimEntries);
            var nodeA = nodes.GetValueOrDefault(parts[0], new Node() { Name = parts[0] });
            var nodeB = nodes.GetValueOrDefault(parts[1], new Node() { Name = parts[1] });
            nodeA.Connections.Add(nodeB);
            nodeB.Connections.Add(nodeA);
            nodes[parts[0]] =  nodeA;
            nodes[parts[1]] =  nodeB;
        }

        HashSet<string> connections = new();
        HashSet<Node> covered = new();
        foreach (var node in nodes)
        {
            foreach (var connection in node.Value.Connections)
            {
                foreach (var secondConnection in node.Value.Connections)
                {
                    if (connection == secondConnection || covered.Contains(secondConnection) || covered.Contains(connection))
                    {
                        continue;
                    }

                    if (connection.Connections.Contains(secondConnection))
                    {
                        // found group
                        List<string> connectionStrings = new()
                        {
                            node.Key, connection.Name, secondConnection.Name
                        };
                        connectionStrings.Sort();
                        connections.Add(string.Join(',', connectionStrings));
                    }
                }
            }
            covered.Add(node.Value);
        }


        foreach (var connection in connections)
        {
            var split = connection.Split(',');
            foreach (var com in split)
            {
                if (com.StartsWith('t')) 
                { 
                    result++;
                    break;
                }
            }
        }
        System.Console.WriteLine(
            $"Result {inputFile} is {string.Join(',', result)} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    static void RunPart2(string inputFile)
    {

        var start = DateTime.Now;
        long result = 0;
        var lines = System.IO.File.ReadAllLines(inputFile);

        Dictionary<string, Node> nodes = new();
        foreach (var line in lines)
        {
            var parts = line.Split("-", StringSplitOptions.RemoveEmptyEntries| StringSplitOptions.TrimEntries);
            var nodeA = nodes.GetValueOrDefault(parts[0], new Node() { Name = parts[0] });
            var nodeB = nodes.GetValueOrDefault(parts[1], new Node() { Name = parts[1] });
            nodeA.Connections.Add(nodeB);
            nodeB.Connections.Add(nodeA);
            nodes[parts[0]] =  nodeA;
            nodes[parts[1]] =  nodeB;
        }


        var bestset = FindClique(nodes);

        var names = bestset.Select(n => n.Name).ToList();
        names.Sort();
        var stringResult = string.Join(",", names);
        System.Console.WriteLine(
            $"Result {inputFile} is {string.Join(',', stringResult)} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static HashSet<Node> FindClique(Dictionary<string, Node> nodes)
    {
        List<HashSet<Node>> cliques = new();
        BronKerbosch(new HashSet<Node>(), new HashSet<Node>(nodes.Values), new HashSet<Node>(), cliques );

        HashSet<Node> result = new HashSet<Node>();
        foreach (var clique in cliques)
        {
            if (clique.Count > result.Count)
            {
                result = clique;
            }
        }

        return result;
    }
    
    private static void BronKerbosch(HashSet<Node> candidateSet, HashSet<Node> remaining, HashSet<Node> exclusions,
        List<HashSet<Node>> cliques)
    {
        if (remaining.Count == 0 && exclusions.Count == 0)
        {
            cliques.Add([..candidateSet]);
            return;
        }

        var pivot = remaining.Concat(exclusions).First();
        var nonNeighbors = new HashSet<Node>(remaining.Except(pivot.Connections));

        foreach (var v in nonNeighbors)
        {
            var newR = new HashSet<Node>(candidateSet) { v };
            var newP = new HashSet<Node>(remaining.Intersect(v.Connections));
            var newX = new HashSet<Node>(exclusions.Intersect(v.Connections));
            BronKerbosch(newR, newP, newX, cliques);
            remaining.Remove(v);
            exclusions.Add(v);
        }
    }

}