using NetworkAnalyzer.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NetworkAnalyzer.Infrastructure
{
    public class SimpleDfsAnalyzer : INetworkAnalyzer
    {
        private readonly IFileParser _fileParser;
        private Dictionary<string, Node> _visited;
        private Dictionary<string, Node> _notVisited;

        public SimpleDfsAnalyzer(IFileParser fileParser)
        {
            _fileParser = fileParser;
        }

        public Network Analyze(string filePath)
        {
            var network = _fileParser.Parse(filePath);
            _visited = new Dictionary<string,Node>();
            _notVisited = new Dictionary<string, Node>();
            foreach (var item in network.Nodes)
            {
                _notVisited[item.Key] = item.Value;
            }

            while (_notVisited.Any())
            {
                var node = _notVisited.First().Value;
                var netWorkSegment = new NetworkSegment();
                Dfs(node, netWorkSegment);
                network.NetworkSegments.Add(netWorkSegment);
            }

            _visited.Clear();
            _notVisited.Clear();
            return network;
        }

        public bool IsHasManySegments(string filePath)
        {
            return Analyze(filePath).NetworkSegments.Count > 1;
        }

        private void Dfs(Node node, NetworkSegment netWorkSegment)
        {
            _visited[node.Name] = node;
            netWorkSegment[node.Name] = node;
            _notVisited.Remove(node.Name);
            foreach (var neighbor in node.Neighbors)
            {
                if (_visited.ContainsKey(neighbor.Key)) continue;
                Dfs(neighbor.Value, netWorkSegment);
            }
        }
    }
}
