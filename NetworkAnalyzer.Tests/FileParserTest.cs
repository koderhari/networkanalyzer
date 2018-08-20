using NetworkAnalyzer.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using static NetworkAnalyzer.Tests.TestEnvironment;


namespace NetworkAnalyzer.Tests
{

    [TestFixture]
    public class FileParserTest
    {
        [SetUp]
        public void Setup()
        {
        }
        //todo проверить связи
        //некорректный формат
        [Test]
        public void ParseNetworkFileNotFound()
        {
            var fileName = "Network1Segment.txt";
            var fileParser = new FileParser();
            Assert.Throws<FileNotFoundException>(() => fileParser.Parse(fileName));
        }

        [Test]
        public void ParseNetwork1Segment()
        {
            var fileParser = new FileParser();
            var netWork = fileParser.Parse(FILE_1SEGMENT);
            var lengthNeighbours = new Dictionary<string, string[]>
            {
                {"Node1",new string[]{"Node2","Node3"}},
                {"Node2",new string[]{"Node1","Node3"}},
                {"Node3",new string[]{"Node1","Node2"}},
            };
            Assert.AreEqual(netWork.Nodes.Count, 3, string.Format(COUNTS_MUST_BE,3));
            CheckNeigbors(netWork.Nodes, lengthNeighbours);
        }

        [Test]
        public void ParseNetwork2Segment()
        {
            var fileParser = new FileParser();
            var netWork = fileParser.Parse(FILE_2SEGMENT);
            var lengthNeighbours = new Dictionary<string, string[]>
            {
                {"Node1",new string[]{"Node2","Node3"}},
                {"Node2",new string[]{"Node1","Node3"}},
                {"Node3",new string[]{"Node1","Node2"}},
                {"Node4",new string[]{"Node5"}},
                {"Node5",new string[]{"Node4"}},
            };
            Assert.AreEqual(netWork.Nodes.Count, 5, string.Format(COUNTS_MUST_BE, 5));
            CheckNeigbors(netWork.Nodes, lengthNeighbours);
        }

        private void CheckNeigbors(Dictionary<string, Node> nodes, Dictionary<string, string[]> lengthNeighbours)
        {
            foreach (var node in lengthNeighbours)
            {
                if (!nodes.TryGetValue(node.Key, out var parseNode))
                {
                    Assert.Fail($"Can't find node {node.Key} in nodes");
                }

                Assert.AreEqual(parseNode.Neighbors.Count, node.Value.Length, $"Wrong neigbours for node {node.Key}");
                foreach (var neighbour in node.Value)
                {
                    if (!parseNode.Neighbors.ContainsKey(neighbour))
                        Assert.Fail($"Can't find neighbour {neighbour} for {node.Key}");
                }
            }
        }

        [Test]
        public void ParseNetworkNoSegment()
        {
            var fileParser = new FileParser();
            var netWork = fileParser.Parse(FILE_NOSEGMENT);
            Assert.AreEqual(netWork.IsEmpty, true, "Network must be empty");
        }

        [Test]
        public void ParseNetworkWrongFormat()
        {
            var fileParser = new FileParser();
            Assert.Throws<InvalidOperationException>(() => fileParser.Parse(FILE_WRONGFORMAT),"Wrong file format");
        }


    }
}
