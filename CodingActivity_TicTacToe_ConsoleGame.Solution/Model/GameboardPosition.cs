using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingActivity_TicTacToe_ConsoleGame
{
    /// <summary>
    /// struct to store the game board location of a player's piece
    /// </summary>

    public struct GameboardPosition
    {
        public int XAxis { get; set; }

        public int YAxis { get; set; }

        public int ZAxis { get; set; }

        public GameboardPosition(int x, int y, int z)
        {
            XAxis = x;
            YAxis = y;
            ZAxis = z; 
        }
    }
}
