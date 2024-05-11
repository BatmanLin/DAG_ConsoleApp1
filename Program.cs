namespace DAG_ConsoleApp1;

using DAG_ConsoleApp1.Models;
using System;
using System.Linq.Expressions;

class Program
{
    static void Main(string[] args) {
        // Construct the provided graph
        var a = new Nodes { NodeName = "A" };
        var b = new Nodes { NodeName = "B" };
        var c = new Nodes { NodeName = "C" };
        var d = new Nodes { NodeName = "D" };
        var e = new Nodes { NodeName = "E" };
        var f = new Nodes { NodeName = "F" };
        var g = new Nodes { NodeName = "G" };
        var h = new Nodes { NodeName = "H" };
        var i = new Nodes { NodeName = "I" };

        a.NodeNeighbor.Add(b, 4);
        a.NodeNeighbor.Add(c, 6);
        b.NodeNeighbor.Add(a, 4);
        b.NodeNeighbor.Add(f, 2);
        c.NodeNeighbor.Add(a, 6);
        c.NodeNeighbor.Add(d, 8);
        d.NodeNeighbor.Add(c, 8);
        d.NodeNeighbor.Add(e, 4);
        d.NodeNeighbor.Add(g, 1);
        e.NodeNeighbor.Add(b, 2);
        e.NodeNeighbor.Add(d, 4);
        e.NodeNeighbor.Add(f, 3);
        e.NodeNeighbor.Add(i, 8);
        f.NodeNeighbor.Add(b, 2);
        f.NodeNeighbor.Add(e, 3);
        f.NodeNeighbor.Add(g, 4);
        f.NodeNeighbor.Add(h, 6);
        g.NodeNeighbor.Add(d, 1);
        g.NodeNeighbor.Add(f, 4);
        g.NodeNeighbor.Add(h, 5);
        g.NodeNeighbor.Add(i, 5);
        h.NodeNeighbor.Add(f, 6);
        h.NodeNeighbor.Add(g, 5);
        i.NodeNeighbor.Add(e, 8);
        i.NodeNeighbor.Add(g, 5);

        // Build the list of graph nodes, and ask users for the from/to Nodes
        var graphNode = new List<Nodes> { a, b, c, d, e, f, g, h, i };
        
        String choice = GetNodeNameInput("Enter any character to start.");
        while (choice != "X") 
        {
            Console.WriteLine("Welcome to play our DAG search console :)");
            string fromNodeName = GetNodeNameInput("Enter the Starting Node (fromNodeName)");
            string toNodeName = GetNodeNameInput("Enter the Ending Node (toNodeName)");

            // Find the shortest path with the Dijkstra's Algorithm
            var shortestPathData = ShortestPathData(fromNodeName, toNodeName, graphNode);

            // Print the result of path and distance to StdOut
            Console.WriteLine($"The Shortest Path: {string.Join(",", shortestPathData.NodeNames)} with the Distance: {shortestPathData.Distance}");
            choice = GetNodeNameInput("Please X to exit, or any other character to restart :P");
        }
        
    }

    public static string GetNodeNameInput(string prompt) {
        Console.WriteLine(prompt);
        string input;
        while (true)
        {
            input = Console.ReadLine()?.Trim(); 
            if(!string.IsNullOrEmpty(input) && input.Length == 1) break;
            Console.WriteLine("Invalid input! Please try again.");
        }
        return input;
    }

    public static ShortestPathData ShortestPathData(string fromNodeName, string toNodeName, List<Nodes> graphNode) 
    {  
        var queue = new List<Nodes>(); // This queue is the nodes in Graph to be process
        var disDic = new Dictionary<Nodes, int>(); // Used to update the distance
        var preDic = new Dictionary<Nodes, Nodes?>(); // Used to update the parent node, parentNode can be Null.
        var shortestPath = new List<string>();

        // Initial the queue, disDic and preDic as presented in the algorithm video
        foreach (var node in graphNode)
        {
            queue.Add(node);
            disDic[node] = int.MaxValue;
            preDic[node] = null;
        }

        // Set up start/end node, update its distance with parent as 0, its parent will remain Null
        var startNode = graphNode.Single(node => node.NodeName == fromNodeName);
        var endNode = graphNode.Single(node => node.NodeName == toNodeName);
        disDic[startNode] = 0;

        while (queue.Any())
        {
            var near = queue.OrderBy(node => disDic[node]).First(); // 1st near will be startNode itself with distance 0
            queue.Remove(near);

            // Traverse near's neighbors to update
            foreach (var neighbor in near.NodeNeighbor.Keys)
            {
                var opt = disDic[near] + near.NodeNeighbor[neighbor];
                if (disDic[neighbor] > opt)
                {
                    disDic[neighbor] = opt;
                    preDic[neighbor] = near;
                }
            }

        }

        shortestPath.Add(endNode.NodeName);
        var distance = disDic[endNode];

        // Reverse back to startNode whose parent is Null
        while (preDic[endNode] != null)
        {
            endNode = preDic[endNode];
            shortestPath.Insert(0, endNode.NodeName);
        }
        return new ShortestPathData { NodeNames = shortestPath, Distance = distance };

    }
}