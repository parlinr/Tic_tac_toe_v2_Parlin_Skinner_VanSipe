using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingActivity_TicTacToe_ConsoleGame
{
    public class GameController
    {
        #region FIELDS
        //
        // track game and round status
        //
        private bool _playingGame;
        private bool _playingRound;

        private int _roundNumber;

        //
        // track the results of multiple rounds
        //
        private int _playerXNumberOfWins;
        private int _playerONumberOfWins;
        private int _numberOfCatsGames;

        //
        // instantiate  a Gameboard object
        // instantiate a GameView object and give it access to the Gameboard object
        //
        private static Gameboard _gameboard = new Gameboard();
        private static ConsoleView _gameView = new ConsoleView(_gameboard);

        private static Gameboard.GameboardState _currentPlayerTurn;

        #endregion

        #region PROPERTIES



        #endregion

        #region CONSTRUCTORS

        public GameController()
        {
            
            InitializeGame();
            PlayGame();
        }
        
        #endregion

        #region METHODS

        /// <summary>
        /// Initialize the multi-round game.
        /// </summary>
        public void InitializeGame()
        {
            //
            // Initialize game variables
            //
            _playingGame = true;
            _playingRound = true;
            _roundNumber = 0;
            _playerONumberOfWins = 0;
            _playerXNumberOfWins = 0;
            _numberOfCatsGames = 0;

            //
            // Initialize game board status
            //
            _gameboard.InitializeGameboard();
        }
        
        /// <summary>
        /// Game Loop
        /// </summary>
        public void PlayGame()
        {
            try
            {
                _gameView.DisplaySplashScreen();
                _gameView.DisplayWelcomeScreen();
            }
            catch (InvalidKeystrokeException ivk)
            {
                Console.Clear();
                global::System.Console.WriteLine(ivk.Message + "\n\n Press any key to exit the application.");
                Console.ReadKey();
                _playingGame = false;
            }

            while (_playingGame)
            {
                //
                // Round loop
                //
                while (_playingRound)
                {
                    //
                    // Perform the task associated with the current game and round state
                    //
                    ManageGameStateTasks();
                    
                    if (_gameView.CurrentViewState == ConsoleView.ViewState.ViewCurrentStats)
                    {
                        _gameView.DisplayCurrentGameStatus(_roundNumber, _playerXNumberOfWins, _playerONumberOfWins, _numberOfCatsGames);
                        _gameView.DisplayContinuePrompt();
                    }
                    
                    //
                    // Evaluate and update the current game board state
                    //
                    _gameboard.UpdateGameboardState();

                    
                    if (_gameView.CurrentViewState == ConsoleView.ViewState.ResetCurrentRound)
                    {
                        //Confirm that the user wants to reset the current round.
                        if (_gameView.DisplayResetRoundPrompt() == true)
                        {
                            //If the user wants to reset the round...

                            //End the round.
                            _roundNumber--;
                            _playingRound = false;
                        }
                        else
                        {
                            //If the user does not want to reset the round...

                            //Re-draw the game board.
                            _gameView.CurrentViewState =  ConsoleView.ViewState.Active;
                            _gameboard.CurrentRoundState = _currentPlayerTurn;
                        }
                    }
                }

                //
                // Round Complete: Display the results
                //
                if (_gameView.CurrentViewState != ConsoleView.ViewState.ResetCurrentRound)
                {
                    _gameView.DisplayCurrentGameStatus(_roundNumber, _playerXNumberOfWins, _playerONumberOfWins, _numberOfCatsGames);
                }

                //
                // Confirm no major user errors
                //
                if (_gameView.CurrentViewState != ConsoleView.ViewState.PlayerUsedMaxAttempts ||
                    _gameView.CurrentViewState != ConsoleView.ViewState.PlayerTimedOut)
                {
                    
                    //Check to see if the current round is being reset.
                    if (_gameView.CurrentViewState != ConsoleView.ViewState.ResetCurrentRound)
                    {
                        //If the round is not being reset...

                        //
                        // Prompt user to play another round
                        //
                        if (_gameView.DisplayNewRoundPrompt())
                        {
                            _gameboard.InitializeGameboard();
                            _gameView.InitializeView();
                            _playingRound = true;
                        }
                        else
                        {
                            _playingGame = false;
                        }
                    }
                    else
                    {
                        //If the round is being reset...

                        //Start a new round.
                        _gameboard.InitializeGameboard();
                        _gameView.InitializeView();
                        _playingRound = true;
                    }



                }
                //
                // Major user error recorded, end game
                //
                else
                {
                    _playingGame = false;
                }
            }

            _gameView.DisplayClosingScreen();
        }

        /// <summary>
        /// manage each new task based on the current game state
        /// </summary>
        private void ManageGameStateTasks()
        {
            switch (_gameView.CurrentViewState)
            {
                case ConsoleView.ViewState.Active:
                    _gameView.DisplayGameArea();
                    
                    switch (_gameboard.CurrentRoundState)
                    {
                        case Gameboard.GameboardState.NewRound:

                            _roundNumber++;

                            //Generate a random number to decide what player goes first.
                            Random coinFlip = new Random();
                            if (coinFlip.Next(0, 2) == 0)
                            {
                                //If the random number is 0...

                                //Set Player O as the first player to take a turn.
                                _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerOTurn;
                                
                                _gameView.DisplayMessageBox("Player O will start first this round.  Press any key to start playing.");
                                Console.ReadKey();
                            }
                            else
                            {
                                //If the random number is 1...

                                //Set Player X as the first player to take a turn.
                                _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerXTurn;
                                
                                _gameView.DisplayMessageBox("Player X will start first this round.  Press any key to start playing.");
                                Console.ReadKey();
                            }
                            
                            break;

                        case Gameboard.GameboardState.PlayerXTurn:
                            _currentPlayerTurn = Gameboard.GameboardState.PlayerXTurn;
                            ManagePlayerTurn(Gameboard.PlayerPiece.X);
                            break;

                        case Gameboard.GameboardState.PlayerOTurn:
                            _currentPlayerTurn = Gameboard.GameboardState.PlayerOTurn;
                            ManagePlayerTurn(Gameboard.PlayerPiece.O);
                            break;

                        case Gameboard.GameboardState.PlayerXWin:
                            _playerXNumberOfWins++;
                            _playingRound = false;
                            break;

                        case Gameboard.GameboardState.PlayerOWin:
                            _playerONumberOfWins++;
                            _playingRound = false;
                            break;

                        case Gameboard.GameboardState.CatsGame:
                            _numberOfCatsGames++;
                            _playingRound = false;
                            break;

                        default:
                            break;
                    }
                    
                    break;
                case ConsoleView.ViewState.PlayerTimedOut:
                    _gameView.DisplayTimedOutScreen();
                    _playingRound = false;
                    break;
                case ConsoleView.ViewState.PlayerUsedMaxAttempts:
                    _gameView.DisplayMaxAttemptsReachedScreen();
                    _playingRound = false;
                    _playingGame = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Attempt to get a valid player move. 
        /// If the player chooses a location that is taken, the CurrentRoundState remains unchanged,
        /// the player is given a message indicating so, and the game loop is cycled to allow the player
        /// to make a new choice.
        /// </summary>
        /// <param name="currentPlayerPiece">identify as either the X or O player</param>
        private void ManagePlayerTurn(Gameboard.PlayerPiece currentPlayerPiece)
        {
            GameboardPosition gameboardPosition = _gameView.GetPlayerPositionChoice();
            
            if (_gameView.CurrentViewState == ConsoleView.ViewState.ViewCurrentStats)
            {
                _gameView.DisplayCurrentGameStatus(_roundNumber, _playerXNumberOfWins, _playerONumberOfWins, _numberOfCatsGames);
                _gameView.CurrentViewState = ConsoleView.ViewState.Active;
                _gameView.DisplayGameArea();
                gameboardPosition = _gameView.GetPlayerPositionChoice();
            }
            
            if (_gameView.CurrentViewState != ConsoleView.ViewState.ResetCurrentRound)
            {
                //
                //Proceed with turn as normal.
                //
                if (_gameView.CurrentViewState != ConsoleView.ViewState.PlayerUsedMaxAttempts)
                {
                    //
                    // player chose an open position on the game board, add it to the game board
                    //
                    if (_gameboard.GameboardPositionAvailable(gameboardPosition))
                    {
                        _gameboard.SetPlayerPiece(gameboardPosition, currentPlayerPiece);
                    }
                    //
                    // player chose a taken position on the game board
                    //
                    else
                    {
                        _gameView.DisplayGamePositionChoiceNotAvailableScreen();
                    }
                }
            }
        }
        
        #endregion
    }
}
