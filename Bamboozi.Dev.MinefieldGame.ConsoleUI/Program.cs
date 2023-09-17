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
            // NOTE - the Build Action and Copy to Output Directory properties of the JSON file must be set to Content and Copy if newer (or Copy always) respectively
            ServiceCollection services = new ServiceCollection();
            IServiceProvider serviceProvider = services
                .AddGameService()
                .BuildServiceProvider();

            // GetService<>() requires Microsoft.Extensions.DependencyInjection
            IGameController gameController = serviceProvider.GetService<IGameController>()!;
            var initial = gameController.StartGame();

            Console.WriteLine($"You are at {initial.UserState.Location} and have {initial.UserState.LivesRemaining} lives remaining");

            while (true)
            {
                var key = Console.ReadKey(true);
                var move = GetUserMove(key.KeyChar);

                if (move.IsExit)
                {
                    Console.WriteLine("GAME EXITED");
                    break;
                }

                if (move.IsValid)
                {
                    var response = gameController.ProcessMove(move.MoveType);
                    Console.WriteLine();
                    if (response.MoveOutcome == MoveOutcome.OutOfBounds)
                    {
                        Console.WriteLine($"A move {response.MoveType} is not possible!");
                    }
                    else
                    {
                        Console.WriteLine($"You moved {response.MoveType} to {response.UserState.Location}");
                        if (response.MoveOutcome == MoveOutcome.Mine)
                        {
                            Console.WriteLine($"BOOM");
                        }
                        if (response.MoveOutcome == MoveOutcome.Lose || response.MoveOutcome == MoveOutcome.Win)
                        {
                            Console.WriteLine($"GAME OVER: {response.MoveOutcome}");
                            Console.WriteLine($"You have made {response.UserState.MovesTaken} moves and have {response.UserState.LivesRemaining} lives remaining");
                            break;
                        }
                    }
                    Console.WriteLine($"You have made {response.UserState.MovesTaken} moves and have {response.UserState.LivesRemaining} lives remaining");
                }
            }
        }

        static UserMove GetUserMove(char key)
        {
            return key switch
            {
                'u' => new UserMove { MoveType = MoveType.Up},
                'd' => new UserMove { MoveType = MoveType.Down },
                'l' => new UserMove { MoveType = MoveType.Left },
                'r' => new UserMove { MoveType = MoveType.Right },
                'x' => new UserMove { IsExit = true },
                _ => new UserMove { IsValid = false }
            };
        }
    }
}