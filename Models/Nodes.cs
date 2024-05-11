using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAG_ConsoleApp1.Models;

public class Nodes
{
    public required string NodeName { get; set; }

    // Create dictionary to save the distance (edge weight) between current node and its (accessible) neighbour
    public Dictionary<Nodes, int> NodeNeighbor {  get; set; } = new Dictionary<Nodes, int>();

}
