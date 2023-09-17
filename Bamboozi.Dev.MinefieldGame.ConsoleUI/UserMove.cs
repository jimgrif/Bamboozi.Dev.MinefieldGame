using Bamboozi.Dev.MinefieldGame.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bamboozi.Dev.MinefieldGame.ConsoleUI
{
    internal class UserMove
    {
        public MoveType MoveType { get; set; }
        public bool IsValid { get; set; } = true;
        public bool IsExit { get; set; } = false;
    }
}
