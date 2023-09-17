using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bamboozi.Dev.MinefieldGame.StateRepository.Models
{
    public class UserState
    {
        public UserLocation Location { get; set; }
        public int LivesRemaining { get; set; }
        public int MovesTaken { get; set; }
    };
}
