using NetworkAnalyzer.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NetworkAnalyzer.Infrastructure
{
    public class SimpleDfsAnalyzer : INetworkAnalyzer
    {
        private IFileParser _fileParser;
        private Dictionary<string, Node> visited;
        private Dictionary<string, Node> notVisited;

        public SimpleDfsAnalyzer(IFileParser fileParser)
        {
            _fileParser = fileParser;
        }

        public Network Analyze(string filePath)
        {
            var network = _fileParser.Parse(filePath);
            visited = new Dictionary<string,Node>();
            notVisited = new Dictionary<string, Node>();
            foreach (var item in network.Nodes)
            {
                notVisited[item.Key] = item.Value;
            }

            while (notVisited.Any())
            {
                var node = notVisited.First().Value;
                var netWorkSegment = new NetworkSegment();
                Dfs(node, netWorkSegment);
                network.NetworkSegments.Add(netWorkSegment);
            }

            visited.Clear();
            notVisited.Clear();
            return network;
        }

        private void Dfs(Node node, NetworkSegment netWorkSegment)
        {
            visited[node.Name] = node;
            netWorkSegment[node.Name] = node;
            notVisited.Remove(node.Name);
            foreach (var neighbor in node.Neighbors)
            {
                if (visited.ContainsKey(neighbor.Key)) continue;
                Dfs(neighbor.Value, netWorkSegment);
            }
        }
    }
}
