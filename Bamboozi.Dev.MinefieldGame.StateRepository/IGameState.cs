using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bamboozi.Dev.MinefieldGame.StateRepository.Models;

namespace Bamboozi.Dev.MinefieldGame.StateRepository
{
    public interface IGameState
    {
        UserState? GetCurrent();

        void SetCurrent(UserState state);
    }
}
