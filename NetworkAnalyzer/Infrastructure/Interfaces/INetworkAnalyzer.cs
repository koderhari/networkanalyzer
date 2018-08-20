using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkAnalyzer.Infrastructure.Interfaces
{
    public interface INetworkAnalyzer
    {
        Network Analyze(string filePath);

        bool IsHasManySegments(string filePath);
    }
}
