using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

//using TicTacToe.ConsoleApp.Model;

namespace CodingActivity_TicTacToe_ConsoleGame
{
    public class Gameboard
    {
        #region ENUMS

        public enum PlayerPiece
        {
            X,
            O,
            None
        }

        public enum GameboardState
        {
            NewRound,
            PlayerXTurn,
            PlayerOTurn,
            PlayerXWin,
            PlayerOWin,
            CatsGame
        }

        #endregion

        #region FIELDS

        private const int MAX_NUM_OF_ROWS_COLUMNS = 3;

        //private PlayerPiece[,] _positionState;
        private PlayerPiece[, ,] _positionState;

        private GameboardState _currentRoundState;

        #endregion

        #region PROPERTIES

        public int MaxNumOfRowsColumns
        {
            get { return MAX_NUM_OF_ROWS_COLUMNS; }
        }

        //public PlayerPiece[,] PositionState
        //{
        //    get { return _positionState; }
        //    set { _positionState = value; }
        //}
        public PlayerPiece[,,] PositionState
        {
            get { return _positionState; }
            set { _positionState = value; }
        }

        public GameboardState CurrentRoundState
        {
            get { return _currentRoundState; }
            set { _currentRoundState = value; }
        }
        #endregion

        #region CONSTRUCTORS

        public Gameboard()
        {
            //_positionState = new PlayerPiece[MAX_NUM_OF_ROWS_COLUMNS, MAX_NUM_OF_ROWS_COLUMNS];
            _positionState = new PlayerPiece[MAX_NUM_OF_ROWS_COLUMNS, MAX_NUM_OF_ROWS_COLUMNS, MAX_NUM_OF_ROWS_COLUMNS];

            InitializeGameboard();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// fill the game board array with "None" enum values
        /// </summary>
        public void InitializeGameboard()
        {
            _currentRoundState = GameboardState.NewRound;

            for (int xAxis = 0; xAxis < MAX_NUM_OF_ROWS_COLUMNS; xAxis++)
            {
                for (int yAxis = 0; yAxis < MAX_NUM_OF_ROWS_COLUMNS; yAxis++)
                {
                    for (int zAxis = 0; zAxis < MAX_NUM_OF_ROWS_COLUMNS; zAxis++)
                    {
                        _positionState[xAxis, yAxis, zAxis] = PlayerPiece.None;
                    }
                }
            }
        }

        /// <summary>
        /// Determine if the game board position is taken
        /// </summary>
        /// <param name="gameboardPosition"></param>
        /// <returns>true if position is open</returns>
        public bool GameboardPositionAvailable(GameboardPosition gameboardPosition)
        {
            //
            // Confirm that the board position is empty
            // Note: gameboardPosition converted to array index by subtracting 1
            //
            if (_positionState[gameboardPosition.XAxis - 1, gameboardPosition.YAxis - 1, gameboardPosition.ZAxis - 1] == PlayerPiece.None)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Update the game board state if a player wins or a cat's game happens.
        /// </summary>
        public void UpdateGameboardState()
        {
            if (ThreeInARow(PlayerPiece.X))
            {
                _currentRoundState = GameboardState.PlayerXWin;
            }
            //
            // A player O has won
            //
            else if (ThreeInARow(PlayerPiece.O))
            {
                _currentRoundState = GameboardState.PlayerOWin;
            }
            //
            // All positions filled
            //
            else if (IsCatsGame())
            {
                _currentRoundState = GameboardState.CatsGame;
            }
        }
        
        /// <summary>
        /// Check the game board to see if all spaces are filled and neither player has won.
        /// </summary>
        /// <returns></returns>
        public bool IsCatsGame()
        {
            //
            // All positions on board are filled and no winner
            //
            for (int z = 0; z < 3; z++)
            {
                for (int row = 0; row < 3; row++)
                {
                    for (int column = 0; column < 3; column++)
                    {
                        if (_positionState[row, column, z] == PlayerPiece.None)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Check for any three in a row.
        /// </summary>
        /// <param name="playerPieceToCheck">Player's game piece to check</param>
        /// <returns>true if a player has won</returns>
        private bool OrigThreeInARow(PlayerPiece playerPieceToCheck)
        {
            //
            // Check rows for player win
            //
            for (int row = 0; row < 3; row++)    //X axis.
            {
                if (_positionState[row, 0, 0] == playerPieceToCheck &&
                    _positionState[row, 1, 0] == playerPieceToCheck &&
                    _positionState[row, 2, 0] == playerPieceToCheck)
                {
                    return true;
                }
            }

            //
            // Check columns for player win
            //
            for (int column = 0; column < 3; column++)    //y axis.
            {
                if (_positionState[0, column, 0] == playerPieceToCheck &&
                    _positionState[1, column, 0] == playerPieceToCheck &&
                    _positionState[2, column, 0] == playerPieceToCheck)
                {
                    return true;
                }
            }

            //
            // Check diagonals for player win
            //
            if (
                (_positionState[0, 0, 0] == playerPieceToCheck &&
                _positionState[1, 1, 0] == playerPieceToCheck &&
                _positionState[2, 2, 0] == playerPieceToCheck)
                ||
                (_positionState[0, 2, 0] == playerPieceToCheck &&
                _positionState[1, 1, 0] == playerPieceToCheck &&
                _positionState[2, 0, 0] == playerPieceToCheck)
                )
            {
                return true;
            }

            //
            // No Player Has Won
            //

            return false;
        }
        
        /// <summary>
        /// Check for any three in a row.
        /// </summary>
        /// <param name="playerPieceToCheck">Player's game piece to check</param>
        /// <returns>true if a player has won</returns>
        private bool ThreeInARow(PlayerPiece playerPieceToCheck)
        {
            //
            // Check rows for player win
            //
            for (int zAxis = 0; zAxis < 3; zAxis++)
            {
                for (int xAxis = 0; xAxis < 3; xAxis++)    //X axis.
                {
                    if (_positionState[xAxis, 0, zAxis] == playerPieceToCheck &&
                        _positionState[xAxis, 1, zAxis] == playerPieceToCheck &&
                        _positionState[xAxis, 2, zAxis] == playerPieceToCheck)
                    {
                        return true;
                    }
                }

                //
                // Check columns for player win
                //
                for (int yAxis = 0; yAxis < 3; yAxis++)    //y axis.
                {
                    if (_positionState[0, yAxis, zAxis] == playerPieceToCheck &&
                        _positionState[1, yAxis, zAxis] == playerPieceToCheck &&
                        _positionState[2, yAxis, zAxis] == playerPieceToCheck)
                    {
                        return true;
                    }
                }
            }

            //
            // Check diagonals for player win
            //
            for (int zAxis = 0; zAxis < 3; zAxis++)
            {
                if (
                (_positionState[0, 0, zAxis] == playerPieceToCheck &&
                _positionState[1, 1, zAxis] == playerPieceToCheck &&
                _positionState[2, 2, zAxis] == playerPieceToCheck)
                ||
                (_positionState[0, 2, zAxis] == playerPieceToCheck &&
                _positionState[1, 1, zAxis] == playerPieceToCheck &&
                _positionState[2, 0, zAxis] == playerPieceToCheck)
                )
                {
                    return true;
                }
            }
            
            //
            // Check rows across each plane (z axis) of the game board for a player win.
            //
            for (int a = 0; a < 3; a++)
            {
                for (int xAxis = 0; xAxis < 3; xAxis++)    //X axis.
                {
                    if (_positionState[xAxis, a, 0] == playerPieceToCheck &&
                        _positionState[xAxis, a, 1] == playerPieceToCheck &&
                        _positionState[xAxis, a, 2] == playerPieceToCheck)
                    {
                        return true;
                    }
                }
            }

            //
            // Check columns for player win
            //
            for (int b = 0; b < 3; b++)
            {
                for (int yAxis = 0; yAxis < 3; yAxis++)    //y axis.
                {
                    if (_positionState[b, yAxis, 0] == playerPieceToCheck &&
                        _positionState[b, yAxis, 1] == playerPieceToCheck &&
                        _positionState[b, yAxis, 2] == playerPieceToCheck)
                    {
                        return true;
                    }
                }
            }

            //
            // Check diagnals across each plane (z axis) of the game board for a player win.
            //
            if (_positionState[0, 0, 0] == playerPieceToCheck &&
                _positionState[1, 1, 1] == playerPieceToCheck &&
                _positionState[2, 2, 2] == playerPieceToCheck)
            {
                return true;
            }

            if (_positionState[2, 0, 0] == playerPieceToCheck &&
                _positionState[1, 1, 1] == playerPieceToCheck &&
                _positionState[0, 2, 2] == playerPieceToCheck)
            {
                return true;
            }

            if (_positionState[2, 0, 2] == playerPieceToCheck &&
                _positionState[1, 1, 1] == playerPieceToCheck &&
                _positionState[0, 2, 0] == playerPieceToCheck)
            {
                return true;
            }

            if (_positionState[0, 0, 2] == playerPieceToCheck &&
                _positionState[1, 1, 1] == playerPieceToCheck &&
                _positionState[2, 2, 0] == playerPieceToCheck)
            {
                return true;
            }




            //
            // No Player Has Won
            //

            return false;
        }
        
        /// <summary>
        /// Add player's move to the game board.
        /// </summary>
        /// <param name="gameboardPosition"></param>
        /// <param name="PlayerPiece"></param>
        public void SetPlayerPiece(GameboardPosition gameboardPosition, PlayerPiece PlayerPiece)
        {
            //
            // Row and column value adjusted to match array structure
            // Note: gameboardPosition converted to array index by subtracting 1
            //
            _positionState[gameboardPosition.XAxis - 1, gameboardPosition.YAxis - 1, gameboardPosition.ZAxis - 1] = PlayerPiece;

            //
            // Change game board state to next player
            //
            SetNextPlayer();
        }

        /// <summary>
        /// Switch the game board state to the next player.
        /// </summary>
        private void SetNextPlayer()
        {
            if (_currentRoundState == GameboardState.PlayerXTurn)
            {
                _currentRoundState = GameboardState.PlayerOTurn;
            }
            else
            {
                _currentRoundState = GameboardState.PlayerXTurn;
            }
        }

        #endregion
    }
}

