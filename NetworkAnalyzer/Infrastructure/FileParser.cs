using NetworkAnalyzer.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetworkAnalyzer.Infrastructure
{
    public class FileParser : IFileParser
    {
        private static readonly string separator = ";";
        public Network Parse(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            var result = new Network();
            using (var fs = File.OpenRead(filePath))
            {
                using (var streamReader = new StreamReader(fs))
                {
                    var row = 0;
                    while(!streamReader.EndOfStream)
                    {
                        var line = streamReader.ReadLine();
                        row++;
                        if (row == 1)
                            continue;
                        var parts = line.Split(";",StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length != 2)
                            throw new InvalidOperationException("Wrong file format");
                        var node1 = GetOrAddNode(result ,parts[0]);
                        var node2 = GetOrAddNode(result, parts[1]);
                        AddRelation(node1, node2);
                    }
                }
            }
         
            return result;
        }

        private void AddRelation(Node node1, Node node2)
        {
            node1.Neighbors[node2.Name] = node2;
            node2.Neighbors[node1.Name] = node1;
        }

        private Node GetOrAddNode(Network network, string nodeName)
        {
            if (!network.Nodes.TryGetValue(nodeName, out var node))
            {
                node = new Node(nodeName);
                network.Nodes[nodeName] = node;
            }

            return node;
        }
    }
}
