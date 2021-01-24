using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SE.MineField.Interfaces;

namespace SE.MineField
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddTransient<IGameBoard, GameBoardService>()
                .AddTransient<IRenderer, ConsoleScreenRenderer>()
                .AddTransient<IConsoleWrapper, ConsoleWrapper>()
                .AddTransient<IGameEngine, GameEngine>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //do the actual work here
            var gameEngine = serviceProvider.GetService<IGameEngine>();

            gameEngine.Start();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<IGameBoard, GameBoardService>()
                        .AddTransient<IRenderer, ConsoleScreenRenderer>());
    }
}