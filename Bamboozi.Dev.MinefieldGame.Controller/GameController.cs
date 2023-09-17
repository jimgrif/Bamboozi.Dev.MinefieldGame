using Bamboozi.Dev.MinefieldGame.Service;
using Bamboozi.Dev.MinefieldGame.Service.Dto;
using Bamboozi.Dev.MinefieldGame.StateRepository.Models;

namespace Bamboozi.Dev.MinefieldGame.Controller
{
    public class GameController : IGameController
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
        }

        public MoveResponse StartGame()
        {
            // TODO: get from config
            var initialUserState = new UserState
            {
                Location = new UserLocation { X = 0, Y = 0},
                LivesRemaining = 3,
                MovesTaken = 0
            };

            // TODO: get from config or generate
            int[,] initialMinesState =
            {
                // A,B,C,D,E,F,G,H
                {0, 0, 1, 0, 0, 0, 1, 0}, // 1
                {1, 0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 1, 0, 1},
                {0, 0, 1, 0, 1, 0, 0, 0},
                {1, 0, 0, 1, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 1, 1, 0},
                {0, 0, 1, 0, 1, 0, 0, 0},
                {0, 1, 0, 0, 0, 0, 1, 0}  // 8
            };

            return _gameService.StartGame(initialUserState, initialMinesState);
        }

        public MoveResponse ProcessMove(MoveType moveType)
        {
            return _gameService.ProcessMove(moveType);
        }
    }
}