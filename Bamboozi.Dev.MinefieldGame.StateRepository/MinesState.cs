using Bamboozi.Dev.MinefieldGame.StateRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bamboozi.Dev.MinefieldGame.StateRepository
{
    public class MinesState : IMinesState
    {
        int[,]? CurrentState { get; set; }

        public void Initialise(int[,] state)
        {
            CurrentState = state;
        }

        public bool IsMined(UserLocation location)
        {
            // invert the matrix so we can visualise it in the code
            return CurrentState![location.Y, location.X] == 1;
        }
    }
}
