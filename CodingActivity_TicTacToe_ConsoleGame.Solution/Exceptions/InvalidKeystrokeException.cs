﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingActivity_TicTacToe_ConsoleGame
{ 
    public class InvalidKeystrokeException : Exception
    {
        public InvalidKeystrokeException(string message) : base(message)
        {

        }
    }
}
