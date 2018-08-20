using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetworkAnalyzer.Infrastructure;
using NetworkAnalyzer.Infrastructure.Interfaces;
using System;
using System.IO;

namespace NetworkAnalyzer
{
    class Program
    {
        private static ServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            Configure();
            Proccess();
        }

        private static void Proccess()
        {
            var analyzer = _serviceProvider.GetService<INetworkAnalyzer>();
            while (true)
            {
                Console.WriteLine("Please input filename with nodes and press enter (or q for exit)");
                var line = Console.ReadLine();
                if (string.IsNullOrEmpty(line)) continue;
                if (line.Trim().ToLowerInvariant() == "q")
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Start loading");
                try
                {
                    var network = analyzer.Analyze(line);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(network);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error while analyze file {e}");
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stop loading");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        private static void Configure()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            _serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton<IFileParser, FileParser>()
            .AddSingleton<INetworkAnalyzer, SimpleDfsAnalyzer>()
            .BuildServiceProvider();
            _serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);
        }


    }
}
