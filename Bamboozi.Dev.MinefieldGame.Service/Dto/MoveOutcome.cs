using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bamboozi.Dev.MinefieldGame.Service.Dto
{
    public enum MoveOutcome
    {
        OK,
        OutOfBounds,
        Mine,
        Lose,
        Win
    }
}
