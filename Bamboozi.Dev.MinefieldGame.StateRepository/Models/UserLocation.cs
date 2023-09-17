using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bamboozi.Dev.MinefieldGame.StateRepository.Models
{
    public class UserLocation
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            string x = X switch
            {
                0 => "A",
                1 => "B",
                2 => "C",
                3 => "D",
                4 => "E",
                5 => "F",
                6 => "G",
                7 => "H",
                _ => "X"
            };
            return $"{x}{Y + 1}";
        }
    }
}
