using Bamboozi.Dev.MinefieldGame.Service.Dto;
using Bamboozi.Dev.MinefieldGame.StateRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bamboozi.Dev.MinefieldGame.Controller
{
    public interface IGameController
    {
        MoveResponse StartGame();

        MoveResponse ProcessMove(MoveType moveType);
    }
}
