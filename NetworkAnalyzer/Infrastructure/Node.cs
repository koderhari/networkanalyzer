using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkAnalyzer.Infrastructure
{
    public class Node
    {
        public Node(string name)
        {
            Name = name;
            Neighbors = new Dictionary<string, Node>();
        }

        public Dictionary<string,Node> Neighbors { get; set; }

        public string Name { get; private set; }
    }
}
