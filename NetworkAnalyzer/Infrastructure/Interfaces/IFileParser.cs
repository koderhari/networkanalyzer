using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkAnalyzer.Infrastructure.Interfaces
{
    public interface IFileParser
    {
        Network Parse(string filePath);
    }
}
