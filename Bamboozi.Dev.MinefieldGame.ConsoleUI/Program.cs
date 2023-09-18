using Microsoft.Extensions.DependencyInjection;
using Bamboozi.Dev.MinefieldGame.Bootstrap;
using Bamboozi.Dev.MinefieldGame.Controller;
using Bamboozi.Dev.MinefieldGame.Service.Dto;
using System.Text;

namespace Bamboozi.Dev.MinefieldGame.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            IServiceProvider serviceProvider = services
                .AddGameService()
                .BuildServiceProvider();

            IGameController gameController = serviceProvider.GetService<IGameController>()!;
            var initial = gameController.StartGame();

            Console.WriteLine($"You are at {initial.UserState.Location} and have {initial.UserState.LivesRemaining} lives remaining");

            while (true)
            {
                var key = Console.ReadKey(true);

                var move = key.KeyChar switch
                {
                    'u' => new UserMove { MoveType = MoveType.Up },
                    'd' => new UserMove { MoveType = MoveType.Down },
                    'l' => new UserMove { MoveType = MoveType.Left },
                    'r' => new UserMove { MoveType = MoveType.Right },
                    'x' => new UserMove { IsExit = true },
                    _ => new UserMove { IsValid = false }
                };

                if (move.IsExit)
                {
                    Console.WriteLine("GAME EXITED");
                    break;
                }

                if (move.IsValid)
                {
                    var response = gameController.ProcessMove(move.MoveType);
                    Console.WriteLine();

                    var outcomeMessage = response.MoveOutcome switch
                    {
                        MoveOutcome.OutOfBounds => $"A move {response.MoveType} is not possible!",
                        MoveOutcome.Mine => $"BOOM!!",
                        MoveOutcome.Lose => $"BOOM!! GAME OVER: {response.MoveOutcome}",
                        MoveOutcome.Win => $"GAME OVER: {response.MoveOutcome}",
                        _ => null // OK
                    };

                    if (outcomeMessage != null)
                        Console.WriteLine(outcomeMessage);

                    Console.WriteLine($"Current position: {response.UserState.Location}. You have made {response.UserState.MovesTaken} moves and have {response.UserState.LivesRemaining} lives remaining");

                    if (response.MoveOutcome == MoveOutcome.Lose || response.MoveOutcome == MoveOutcome.Win)
                        break;
                }
            }
        }
    }
}