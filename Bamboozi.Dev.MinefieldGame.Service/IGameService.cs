using Bamboozi.Dev.MinefieldGame.Service.Dto;
using Bamboozi.Dev.MinefieldGame.StateRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bamboozi.Dev.MinefieldGame.Service
{
    public interface IGameService
    {
        MoveResponse StartGame(UserState initialUserState, int[,] initialMinesState);

        MoveResponse ProcessMove(MoveType moveType);
    }
}
