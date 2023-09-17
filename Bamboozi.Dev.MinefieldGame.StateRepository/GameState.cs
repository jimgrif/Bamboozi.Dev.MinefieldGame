using Bamboozi.Dev.MinefieldGame.StateRepository.Models;

namespace Bamboozi.Dev.MinefieldGame.StateRepository
{
    public class GameState : IGameState
    {

        UserState? CurrentState { get; set; }

        public UserState? GetCurrent()
        {
            return CurrentState;
        }

        public void SetCurrent(UserState state)
        {
            CurrentState = state;
        }
    }
}