using Bamboozi.Dev.MinefieldGame.StateRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bamboozi.Dev.MinefieldGame.StateRepository
{
    public interface IMinesState
    {
        void Initialise(int[,] state);

        bool IsMined(UserLocation location);
    }
}
