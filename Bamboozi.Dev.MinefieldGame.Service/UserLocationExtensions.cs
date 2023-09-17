using Bamboozi.Dev.MinefieldGame.StateRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bamboozi.Dev.MinefieldGame.Service
{
    public static class UserLocationExtensions
    {
        public static bool IsValid(this UserLocation userLocation)
        {
            return userLocation.X >= 0 && userLocation.Y >= 0 &&
                userLocation.X < Constants.BOARD_SIZE && userLocation.Y < Constants.BOARD_SIZE;
        }
    }
}
