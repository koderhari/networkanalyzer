using NetworkAnalyzer.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static NetworkAnalyzer.Tests.TestEnvironment;

namespace NetworkAnalyzer.Tests
{
    [TestFixture]
    public class SimpleDfsAnalyzerTest
    {
       

        [Test]
        public void AnalyzeNetwork1Segment()
        {
            var fileParser = new FileParser();
            var analyzer = new SimpleDfsAnalyzer(fileParser);
            var netWork = analyzer.Analyze(FILE_1SEGMENT);

            Assert.AreEqual(netWork.NetworkSegments.Count, 1, string.Format(COUNTS_MUST_BE,1));
        }

        [Test]
        public void AnalyzeNetwork2Segment()
        {
            var fileParser = new FileParser();
            var analyzer = new SimpleDfsAnalyzer(fileParser);
            var netWork = analyzer.Analyze(FILE_2SEGMENT);
            var segment1 = new string[] { "Node1","Node2","Node3" };
            var segment2 = new string[] { "Node4", "Node5"};
            var contentSegments = new Dictionary<int, string[]>
            {
                { 3, segment1 },
                { 2, segment2 }
            };

            Assert.AreEqual(netWork.NetworkSegments.Count, 2, string.Format(COUNTS_MUST_BE, 2));
            CheckSegmentsWithDifferentLength(netWork.NetworkSegments, contentSegments);
        }

        [Test]
        public void AnalyzeNetwork5Segment()
        {
            var fileParser = new FileParser();
            var analyzer = new SimpleDfsAnalyzer(fileParser);
            var netWork = analyzer.Analyze(FILE_5SEGMENT);
            var segment1 = new string[] { "Node1", "Node2", "Node3" };
            var segment2 = new string[] { "Node4", "Node5" };
            var segment3 = new string[] { "Node6", "Node7", "Node8", "Node9"};
            var segment4 = new string[] { "Node12", "Node11", "Node13", "Node14", "Node15", "Node16", "Node17"};
            var segment5 = new string[] { "Node21", "Node18", "Node22", "Node23", "Node19", "Node20" };
 

            var contentSegments = new Dictionary<int, string[]>
            {
                { segment1.Length, segment1 },
                { segment2.Length, segment2 },
                { segment3.Length, segment3},
                { segment4.Length, segment4},
                { segment5.Length, segment5},
            };

            Assert.AreEqual(netWork.NetworkSegments.Count, 5, string.Format(COUNTS_MUST_BE, 5));
            CheckSegmentsWithDifferentLength(netWork.NetworkSegments, contentSegments);
        }

        private void CheckSegmentsWithDifferentLength(List<NetworkSegment> networkSegments, Dictionary<int,string[]> contentSegments)
        {
            List<(bool valid, string node)> checks = new List<(bool, string)>();
            foreach (var segment in networkSegments)
            {

                if (!contentSegments.TryGetValue(segment.Count, out var contentSegment))
                {
                    Assert.Fail($"Invalid length and contains for segment: {segment}");
                }
                checks.Add(CheckSegmentContains(segment, contentSegment));
            }

            foreach (var (valid, node) in checks)
            {
                Assert.AreEqual(valid, true, $"Node {node} in wrong segment");
            }
        }

        private (bool result, string node) CheckSegmentContains(NetworkSegment segment,string[] segmentNodes)
        {
            foreach (var node in segmentNodes)
            {
                if (!segment.ContainsKey(node))
                {
                    return (false, node);
                }
            }

            return (true, string.Empty);
        }
    }
}
