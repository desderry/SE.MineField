using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SE.MineField.Enums;
using SE.MineField.Interfaces;
using SE.MineField.Models;

namespace SE.MineField
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddTransient<IGameBoardService, GameBoardServiceService>()
                .AddTransient<IRenderer, ConsoleScreenRenderer>()
                .AddTransient<IConsoleWrapper, ConsoleWrapper>()
                .AddTransient<IGameEngine, GameEngine>()
                .AddTransient<IPlayer, Player>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //do the actual work here
            var gameEngine = serviceProvider.GetService<IGameEngine>();

            gameEngine.Start();

            while (gameEngine.Status == GameStatus.InProgress)
            {
                gameEngine.PlayerMoves(Console.ReadKey().Key);
            }
        }
    }
}