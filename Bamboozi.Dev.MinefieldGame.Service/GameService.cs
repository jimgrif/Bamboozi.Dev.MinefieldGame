using Bamboozi.Dev.MinefieldGame.Service.Dto;
using Bamboozi.Dev.MinefieldGame.StateRepository;
using Bamboozi.Dev.MinefieldGame.StateRepository.Models;
using System.Runtime.CompilerServices;

namespace Bamboozi.Dev.MinefieldGame.Service
{
    public class GameService : IGameService
    {
        private readonly IGameState _gameState;
        private readonly IMinesState _minesState;

        public GameService(IGameState gameState, IMinesState minesState)
        {
            _gameState = gameState ?? throw new ArgumentNullException(nameof(gameState));
            _minesState = minesState ?? throw new ArgumentNullException(nameof(minesState));
        }

        public MoveResponse StartGame(UserState initialUserState, int[,] initialMinesState)
        {
            if (initialUserState.Location.X != 0 || initialUserState.Location.Y != 0 || initialUserState.MovesTaken !=0 || initialUserState.LivesRemaining <= 0)
                throw new InvalidInitialisationException();

            if (initialMinesState.GetLength(0) != Constants.BOARD_SIZE || initialMinesState.GetLength(1) != Constants.BOARD_SIZE)
                throw new InvalidInitialisationException();

            _gameState.SetCurrent(initialUserState);
            _minesState.Initialise(initialMinesState);
            return new MoveResponse(null, null, initialUserState);
        }

        public MoveResponse ProcessMove(MoveType moveType)
        {
            try
            {
                var currentState = _gameState.GetCurrent() ?? throw new NotInitialsedException();

                if (currentState.LivesRemaining <= 0 || !currentState.Location.IsValid())
                    throw new InvalidUserStateException();

                var nextLocation = GetNextLocation(currentState.Location, moveType);

                if (!nextLocation.IsValid())
                {
                    return new MoveResponse(moveType, MoveOutcome.OutOfBounds, currentState);
                }

                var isMined = _minesState.IsMined(nextLocation);

                var updatedState = new UserState
                {
                    Location = nextLocation,
                    MovesTaken = currentState.MovesTaken + 1,
                    LivesRemaining = currentState.LivesRemaining - (isMined ? 1 : 0),
                };

                _gameState.SetCurrent(updatedState);

                var moveOutcome = updatedState.LivesRemaining <= 0 ?
                    MoveOutcome.Lose :
                    updatedState.Location.X == Constants.BOARD_SIZE - 1 ?
                        MoveOutcome.Win :
                        isMined ?
                            MoveOutcome.Mine :
                            MoveOutcome.OK;

                return new MoveResponse(moveType, moveOutcome, updatedState);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private UserLocation GetNextLocation(UserLocation currentLocation, MoveType moveType)
        {
            return moveType switch
            {
                MoveType.Up => new UserLocation { X = currentLocation.X, Y = currentLocation.Y - 1 },
                MoveType.Down => new UserLocation { X = currentLocation.X, Y = currentLocation.Y + 1 },
                MoveType.Left => new UserLocation { X = currentLocation.X - 1, Y = currentLocation.Y },
                MoveType.Right => new UserLocation { X = currentLocation.X + 1, Y = currentLocation.Y },
                _ => currentLocation
            };
        }
    }
}