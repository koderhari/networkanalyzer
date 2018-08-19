using NetworkAnalyzer.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkAnalyzer.Infrastructure
{
    public class Network
    {
        public Network()
        {
            Nodes = new Dictionary<string, Node>();
            NetworkSegments = new List<NetworkSegment>();
        }

        public Dictionary<string, Node> Nodes { get; private set; }

        public List<NetworkSegment> NetworkSegments { get; private set; }

        public bool IsEmpty => Nodes.Count == 0;

        public override string ToString()
        {
            var str = new StringBuilder();
            str.AppendFormat("NetWork has {0} segments.", NetworkSegments.Count);
            var numSegment = 1;
            foreach (var segment in NetworkSegments)
            {
                str.AppendLine();
                str.AppendFormat("Segment {0} has next nodes:", numSegment);
                str.AppendLine();
                foreach (var segmentNode in segment.Keys)
                {
                    str.AppendFormat("{0} ", segmentNode);
                }

                numSegment++;
            }
            return str.ToString();
        }
    }
}
