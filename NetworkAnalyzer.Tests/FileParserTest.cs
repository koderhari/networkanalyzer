using NetworkAnalyzer.Infrastructure;
using NUnit.Framework;
using System;
using System.IO;


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
            var netWork = fileParser.Parse(TestEnvironment.FILE_1SEGMENT);
            Assert.AreEqual(netWork.Nodes.Count, 3, "Count nodes must be 3");
        }

        [Test]
        public void ParseNetwork2Segment()
        {
            var fileParser = new FileParser();
            var netWork = fileParser.Parse(TestEnvironment.FILE_2SEGMENT);
            Assert.AreEqual(netWork.Nodes.Count, 5, "Count nodes must be 5");
        }

        [Test]
        public void ParseNetworkNoSegment()
        {
            var fileParser = new FileParser();
            var netWork = fileParser.Parse(TestEnvironment.FILE_NOSEGMENT);
            Assert.AreEqual(netWork.IsEmpty, true, "Network must be empty");
        }

        [Test]
        public void ParseNetworkWrongFormat()
        {
            var fileParser = new FileParser();
            Assert.Throws<InvalidOperationException>(() => fileParser.Parse(TestEnvironment.FILE_WRONGFORMAT),"Wrong file format");
        }


    }
}
