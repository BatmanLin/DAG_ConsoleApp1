using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAG_ConsoleApp1.Models;

public class ShortestPathData
{
    public required List<string> NodeNames { get; set; }
    public int Distance { get; set; }
}
