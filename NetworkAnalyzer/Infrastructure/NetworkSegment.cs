using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkAnalyzer.Infrastructure
{
    public class NetworkSegment:Dictionary<string,Node>
    {
        public override string ToString()
        {
            var str = new StringBuilder();
            str.AppendLine("Segments contains:");
            str.AppendLine();
            foreach (var node in this.Keys)
            {
                str.AppendFormat("{0} ", node);
            }
            return str.ToString();
        }
    }
}
