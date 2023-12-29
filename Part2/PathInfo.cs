using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2
{
    internal class PathInfo
    {
        public Dictionary<string, int> VisitedNodes { get; set; } = new Dictionary<string, int>();
        public int CycleLength { get; set; }
        public int CycleStart { get; set; }
        public List<int> Solutions { get; set; } = new List<int>();
        public string Start { get; set; }
        public bool Complete { get; set; }
    }
}
