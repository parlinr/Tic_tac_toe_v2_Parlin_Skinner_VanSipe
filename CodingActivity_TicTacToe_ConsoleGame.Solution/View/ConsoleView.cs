using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using System.Windows;

namespace CodingActivity_TicTacToe_ConsoleGame
{
    public class ConsoleView
    {
        #region ENUMS

        public enum ViewState
        {
            Active,
            PlayerTimedOut, // TODO Track player time on task
            PlayerUsedMaxAttempts,
            ResetCurrentRound,
            ViewCurrentStats
        }

        public enum SidebarMenuOptions
        {
            ViewTheRules,
            ResetCurrentRound,
            ViewGameStats,
            //SaveGameStats,
            ExitApplication,
            None 
        }

        #endregion

        #region FIELDS

        private const int GAMEBOARD_VERTICAL_LOCATION = 4;

        private const int POSITIONPROMPT_VERTICAL_LOCATION = 27;  //12;
        private const int POSITIONPROMPT_HORIZONTAL_LOCATION = 3;

        private const int MESSAGEBOX_VERTICAL_LOCATION = 30;  //15;

        private const int TOP_LEFT_ROW = 3;
        private const int TOP_LEFT_COLUMN = 6;

        private Gameboard _gameboard;
        private ViewState _currentViewStat;

        private Dictionary<string, SidebarMenuOptions> SBMenuOptions = new Dictionary<string, SidebarMenuOptions>();

        #endregion

        #region PROPERTIES
        public ViewState CurrentViewState
        {
            get { return _currentViewStat; }
            set { _currentViewStat = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public ConsoleView(Gameboard gameboard)
        {
            _gameboard = gameboard;

            InitializeView();

        }

        #endregion

        #region METHODS

        /// <summary>
        /// Initialize the console view
        /// </summary>
        public void InitializeView()
        {
            _currentViewStat = ViewState.Active;

            //Setup the menu options for the sidebar menu display.
            if (SBMenuOptions.Count == 0)
            {
                SBMenuOptions.Add("V", SidebarMenuOptions.ViewTheRules);
                SBMenuOptions.Add("R", SidebarMenuOptions.ResetCurrentRound);
                SBMenuOptions.Add("S", SidebarMenuOptions.ViewGameStats);
                SBMenuOptions.Add("E", SidebarMenuOptions.ExitApplication);
            }

            InitializeConsole();
        }

        /// <summary>
        /// configure the console window
        /// </summary>
        public void InitializeConsole()
        {
            ConsoleUtil.WindowWidth = ConsoleConfig.windowWidth;
            ConsoleUtil.WindowHeight = ConsoleConfig.windowHeight;

            Console.BackgroundColor = ConsoleConfig.bodyBackgroundColor;
            Console.ForegroundColor = ConsoleConfig.bodyBackgroundColor;

            ConsoleUtil.WindowTitle = "The Tic-tac-toe Game";
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the Exit prompt on a clean screen
        /// </summary>
        public void DisplayExitPrompt()
        {
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            ConsoleUtil.DisplayMessage("Thank you for play the game. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display the session timed out screen
        /// </summary>
        public void DisplayTimedOutScreen()
        {
            ConsoleUtil.HeaderText = "Session Timed Out!";
            ConsoleUtil.DisplayReset();

            DisplayMessageBox("It appears your session has timed out.");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the maximum attempts reached screen
        /// </summary>
        public void DisplayMaxAttemptsReachedScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "Maximum Attempts Reached!";
            ConsoleUtil.DisplayReset();

            sb.Append(" It appears that you are having difficulty entering your");
            sb.Append(" choice. Please refer to the instructions and play again.");

            DisplayMessageBox(sb.ToString());

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Inform the player that their position choice is not available
        /// </summary>
        public void DisplayGamePositionChoiceNotAvailableScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "Position Choice Unavailable";
            ConsoleUtil.DisplayReset();

            sb.Append(" It appears that you have chosen a position that is all ready");
            sb.Append(" taken. Please try again.");

            DisplayMessageBox(sb.ToString());

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Displays the opening screen for the game.
        /// </summary>
        public void DisplaySplashScreen()
        {
            //Setup the console.
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 10);

            //Display the opening title.
            string tabSpace = new String(' ', 23);
            Console.WriteLine(tabSpace + @"  _______          ______               ______              _____ ____ ");
            Console.WriteLine(tabSpace + @" /_  __(_)____    /_  __/___ ______    /_  __/___  ___     |__  // __ \");
            Console.WriteLine(tabSpace + @"  / / / / ___/_____/ / / __ `/ ___/_____/ / / __ \/ _ \     /_ </ / / /");
            Console.WriteLine(tabSpace + @" / / / / /__/_____/ / / /_/ / /__/_____/ / / /_/ /  __/   ___/ / /_/ / ");
            Console.WriteLine(tabSpace + @"/_/ /_/\___/     /_/  \__,_/\___/     /_/  \____/\___/   /____/_____/  ");
            Console.WriteLine(tabSpace + @"                                                                       ");

            //I had to set the window size in order for this to work on my machine. -- R.P.
            Console.SetWindowSize(100, 25);

            //Hold the execution of the application until the user presses a key.
            Console.SetCursorPosition(80, 25);
            DisplayContinuePrompt();
        }
        
        /// <summary>
        /// display the welcome message on screen
        /// </summary>
        public void DisplayWelcomeScreen()
        {
            //Variable Declarations.
            ConsoleKeyInfo keyPressed;

            //Reset the console display.
            ConsoleUtil.HeaderText = "The Tic-tac-toe Game";
            ConsoleUtil.DisplayReset();
            
            //Display the welcome message.
            Console.WriteLine("Tic-Tac-Toe 3D");
            Console.WriteLine("");
            Console.WriteLine("Welcome to Tic-tac-Toe in 3 dimensions!!");
            Console.WriteLine("");
            Console.WriteLine("Your mission, should you choose to accept it, is take the challenge of playing a classic game in a whole");
            Console.WriteLine("new way.  With Tic-Tac-Toe 3D Bird Brain International has taken the original, classic game of Tic-");
            Console.WriteLine("Tac-Toe and added, well, a whole new dimension. ");
            Console.WriteLine("");
            Console.WriteLine("Press Enter to play or Escape to exit the game.");
            
            //Get the user's response.
            keyPressed = Console.ReadKey();
            
            //Check for the Enter or Escape key .
            if (keyPressed.Key == ConsoleKey.Escape || keyPressed.Key == ConsoleKey.Enter)
            {
                //If the Enter or Escape keys are pressed...

                //Take action based on the key pressed.
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    //If the user pressed the Escape key...

                    //Exit the application.
                    Environment.Exit(0);
                }
            }
            else
            {
                //If the Enter or Escape keys are pressed...

                //Throw an error for an invalid keystroke.
                throw new InvalidKeystrokeException("Use either the Enter or Escape key.");
            }
        }

        /// <summary>
        /// Display a closing screen when the user quits the application
        /// </summary>
        public void DisplayClosingScreen()
        {
            ConsoleUtil.HeaderText = "The Tic-tac-toe Game";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Thank you for playimg Tic-tac-toe 3D by Bird Brain International.");
            ConsoleUtil.DisplayMessage("Visit www.birdbrain.com for upcoming games.\n\n");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Displays the game board, sidebar menu, and game status in the console.
        /// </summary>
        public void DisplayGameArea()
        {
            //Reset the console.
            ConsoleUtil.HeaderText = "Current Game Board";
            ConsoleUtil.DisplayReset();
            
            //Display the menu
            DisplaySidebarMenu();

            //Display the game board.
            DisplayGameboard();

            //Display the current status of the game at the bottom of the screen.
            DisplayGameStatus();
        }

        /// <summary>
        /// Display the instructions for the game on the screen.
        /// </summary>
        public void DisplayGameInstructions()
        {
            Console.WriteLine("How to play ");
            Console.WriteLine("");
            Console.WriteLine("The object of the game, as with traditional Tic-tac-Toe, is for one player to get 3 game pieces in a");
            Console.WriteLine("row (across the X or Y axis, or forward & back on the Z axis).   ");
            Console.WriteLine("");
            Console.WriteLine("To place a game piece (X or O) on the gameboard enter the coordinates in an X, Y, Z grouping for the ");
            Console.WriteLine("empty location on the gameboard you wish to place a game piece (coordinates of 2, 2, 2 for example ");
            Console.WriteLine("places a piece in the centermost location of the gameboard).  ");
            Console.WriteLine("");
            Console.WriteLine("At any point in the game an option from the sidebar menu can be entered to do other tasks such as view");
            Console.WriteLine("gameplay statistics, reset the current round of play, and exit the application.");
        }
        
        /// <summary>
        /// Displays the menu on the right-hand side of the screen.
        /// </summary>
        public void DisplaySidebarMenu()
        {
            //Variable Declarations.
            int menuColumn = 88;
            int menuRow = GAMEBOARD_VERTICAL_LOCATION;

            //Display the menu sidbar header.
            Console.SetCursorPosition(menuColumn, menuRow);
            Console.WriteLine("     Main Menu      ");
            menuRow++;
            Console.SetCursorPosition(menuColumn, menuRow);
            Console.WriteLine("                    ");
            menuRow++;

            //Display the menu options.
            foreach (KeyValuePair<string, SidebarMenuOptions> menuOption in SBMenuOptions)
            {
                Console.SetCursorPosition(menuColumn, menuRow);
                Console.WriteLine($" {menuOption.Key} - {ConsoleUtil.ToLabelFormat(menuOption.Value.ToString())}");
                menuRow++;
            }
        }
        
        public void DisplayCurrentGameStatus(int roundsPlayed, int playerXWins, int playerOWins, int catsGames)
        {
            ConsoleUtil.HeaderText = "Current Game Status";
            ConsoleUtil.DisplayReset();

            double playerXPercentageWins = (double)playerXWins / roundsPlayed;
            double playerOPercentageWins = (double)playerOWins / roundsPlayed;
            double percentageOfCatsGames = (double)catsGames / roundsPlayed;

            ConsoleUtil.DisplayMessage("Rounds Played: " + roundsPlayed);
            ConsoleUtil.DisplayMessage("Rounds for Player X: " + playerXWins + " - " + String.Format("{0:P2}", playerXPercentageWins));
            ConsoleUtil.DisplayMessage("Rounds for Player O: " + playerOWins + " - " + String.Format("{0:P2}", playerOPercentageWins));
            ConsoleUtil.DisplayMessage("Cat's Games: " + catsGames + " - " + String.Format("{0:P2}", percentageOfCatsGames));

            DisplayContinuePrompt();
        }

        public bool DisplayNewRoundPrompt()
        {
            ConsoleUtil.HeaderText = "Continue or Quit";
            ConsoleUtil.DisplayReset();

            return DisplayGetYesNoPrompt("Would you like to play another round?");
        }
        
        /// <summary>
        /// Prompts the user to either reset  the current round or not to reset the current round.
        /// </summary>
        /// <returns></returns>
        public bool DisplayResetRoundPrompt()
        {
            ConsoleUtil.HeaderText = "Reset Current Round";
            ConsoleUtil.DisplayReset();

            return DisplayGetYesNoPrompt("Would you like to reset the current round?");
        }
        
        /// <summary>
        /// Display the current game status at the bottom of the screen.
        /// </summary>
        public void DisplayGameStatus()
        {
            StringBuilder sb = new StringBuilder();

            switch (_gameboard.CurrentRoundState)
            {
                case Gameboard.GameboardState.NewRound:
                    //
                    // The new game status should not be an necessary option here
                    //
                    break;
                case Gameboard.GameboardState.PlayerXTurn:
                    DisplayMessageBox("It is currently Player X's turn.");
                    break;
                case Gameboard.GameboardState.PlayerOTurn:
                    DisplayMessageBox("It is currently Player O's turn.");
                    break;
                case Gameboard.GameboardState.PlayerXWin:
                    DisplayMessageBox("Player X Wins! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                case Gameboard.GameboardState.PlayerOWin:
                    DisplayMessageBox("Player O Wins! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                case Gameboard.GameboardState.CatsGame:
                    DisplayMessageBox("Cat's Game! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Display a message to the user at the bottom of the screen.
        /// </summary>
        /// <param name="message"></param>
        public void DisplayMessageBox(string message)
        {
            string leftMargin = new String(' ', ConsoleConfig.displayHorizontalMargin);
            string topBottom = new String('*', ConsoleConfig.windowWidth - 2 * ConsoleConfig.displayHorizontalMargin);

            StringBuilder sb = new StringBuilder();

            Console.SetCursorPosition(0, MESSAGEBOX_VERTICAL_LOCATION);
            Console.WriteLine(leftMargin + topBottom);

            Console.WriteLine(ConsoleUtil.Center("Game Status"));

            ConsoleUtil.DisplayMessage(message);

            Console.WriteLine(Environment.NewLine + leftMargin + topBottom);
        }

        /// <summary>
        /// Displays the tic-tac-toe 3D game board.
        /// </summary>
        private void DisplayGameboard()
        {
            //Display the game board planes.
            DisplayGameboardPlane(0, 0, 0);
            DisplayGameboardPlane(30, 7, 1);  
            DisplayGameboardPlane(50, 15, 2);

            //Display Game board Labels.
            DisplayGameboardLabels();
        }

        /// <summary>
        /// Displays the a single plane of the 3D cube of the tic-tac-toe board.
        /// </summary>
        /// <param name="planeName"></param>
        /// <param name="column"></param>
        /// <param name="verticalPositionOffset"></param>
        /// <param name="zAxis"></param>
        private void DisplayGameboardPlane(int column, int verticalPositionOffset, int zAxis)
        {
            //
            // move cursor below header
            //
            Console.SetCursorPosition(column, GAMEBOARD_VERTICAL_LOCATION + verticalPositionOffset);
            
            //Display the top of the plane.
            Console.Write("\t\t        |---+---+---|");

            //
            for (int i = 0; i < 3; i++)
            {
                //Fill in the vertical lines to seperate the cells.
                verticalPositionOffset++;
                Console.SetCursorPosition(column, GAMEBOARD_VERTICAL_LOCATION + verticalPositionOffset);
                Console.Write("\t\t        | ");

                for (int j = 0; j < 3; j++)
                {
                    if (_gameboard.PositionState[i, j, zAxis] == Gameboard.PlayerPiece.None)
                    {
                        Console.Write(" " + " | ");
                    }
                    else
                    {
                        Console.Write(_gameboard.PositionState[i, j, zAxis] + " | ");
                    }
                }
                
                //Display the row divider.
                verticalPositionOffset++;
                Console.SetCursorPosition(column, GAMEBOARD_VERTICAL_LOCATION + verticalPositionOffset);
                Console.Write("\t\t        |---+---+---|");
            }
        }

        /// <summary>
        /// Displays the X, Y, and Z axis labels on the game board.
        /// </summary>
        private void DisplayGameboardLabels()
        {
            //Variable Declarations.
            int column = 24;

            //Display the labels for the game board.
            Console.SetCursorPosition(21, 7);   //X Axis
            Console.WriteLine("X");
            Console.SetCursorPosition(30, 12);   //Y Axis
            Console.WriteLine("Y");

            //Z Axis
            for (int row = 14; row < 26; row++)
            {
                switch (column)
                {
                    case 39:
                        Console.SetCursorPosition(column, row);
                        Console.WriteLine(@"");
                        break;
                    case 42:
                        Console.SetCursorPosition(column, row);
                        Console.WriteLine(@"Z");
                        break;
                    case 45:
                        Console.SetCursorPosition(column, row);
                        Console.WriteLine(@"");
                        break;
                    default:
                        Console.SetCursorPosition(column, row);
                        Console.WriteLine(@"\");
                        break;
                }

                column += 3;
            }
        }
        
        /// <summary>
        /// display prompt for a player's next move
        /// </summary>
        /// <param name="coordinateType"></param>
        private void DisplayPositionPrompt()  //(string coordinateType)
        {
            Console.SetCursorPosition(POSITIONPROMPT_HORIZONTAL_LOCATION, POSITIONPROMPT_VERTICAL_LOCATION);
            Console.Write(new String(' ', ConsoleConfig.windowWidth));
            //
            // Write new prompt
            //
            Console.SetCursorPosition(POSITIONPROMPT_HORIZONTAL_LOCATION, POSITIONPROMPT_VERTICAL_LOCATION);
            //Console.Write("Enter " + coordinateType + " number: ");
        }

        /// <summary>
        /// Display a Yes or No prompt with a message
        /// </summary>
        /// <param name="promptMessage">prompt message</param>
        /// <returns>bool where true = yes</returns>
        private bool DisplayGetYesNoPrompt(string promptMessage)
        {
            bool yesNoChoice = false;
            bool validResponse = false;
            string userResponse;

            while (!validResponse)
            {
                //ConsoleUtil.DisplayReset();

                ConsoleUtil.DisplayPromptMessage(promptMessage + "(yes/no)");
                userResponse = Console.ReadLine();

                if (userResponse.ToUpper() == "YES")
                {
                    validResponse = true;
                    yesNoChoice = true;
                }
                else if (userResponse.ToUpper() == "NO")
                {
                    validResponse = true;
                    yesNoChoice = false;
                }
                else
                {
                    ConsoleUtil.DisplayMessage(
                        "It appears that you have entered an incorrect response." +
                        " Please enter either \"yes\" or \"no\"."
                        );
                    DisplayContinuePrompt();
                }
            }

            return yesNoChoice;
        }

        /// <summary>
        /// Get a player's position choice within the correct range of the array
        /// Note: The ConsoleView is allowed access to the GameboardPosition struct.
        /// </summary>
        /// <returns>GameboardPosition</returns>
        public GameboardPosition GetPlayerPositionChoice()
        {
            //Variable Declaratoins.
            GameboardPosition gameboardPosition = new GameboardPosition(-1, -1, -1);
            string userInput = "";
            SidebarMenuOptions menuOptionChoice = SidebarMenuOptions.None;
            
            //Validate the coordinates entered by the user.
            int[] userCoords = { 0, 0, 0 };

            do
            {
                userInput = GetCoordinates();
                if (SBMenuOptions.ContainsKey(userInput.ToUpper()) == true)
                {
                    //If a menu option was entered...

                    //Display the appropriate screen based on the menu option entered by the user.
                    menuOptionChoice = SBMenuOptions[userInput.ToUpper()];
                    switch (menuOptionChoice)
                    {
                        case SidebarMenuOptions.ViewTheRules:
                            //Display the game instructions.
                            Console.Clear();
                            DisplayGameInstructions();
                            Console.WriteLine("");
                            Console.WriteLine("");

                            //Hold the execution of the application until the user presses a key.
                            DisplayContinuePrompt();

                            //Redraw the game board.
                            DisplayGameArea();
                            break;
                        case SidebarMenuOptions.ResetCurrentRound:
                            _gameboard.CurrentRoundState = Gameboard.GameboardState.NewRound;
                            _currentViewStat = ViewState.ResetCurrentRound;
                            return gameboardPosition;
                        case SidebarMenuOptions.ViewGameStats:
                            _currentViewStat = ViewState.ViewCurrentStats;
                            return gameboardPosition;
                            //break;
                        case SidebarMenuOptions.ExitApplication:
                            //Ask the user if they want to leave the application.
                            Console.Clear();
                            if (DisplayGetYesNoPrompt("Do you wish to exit the application?") == true)
                            {
                                //If the user enters yes...

                                //Display the closing screen.
                                DisplayClosingScreen();

                                //Exit.
                                Environment.Exit(0);
                            }
                            else
                            {
                                //If the user enters yes...

                                //Redraw the game board.
                                DisplayGameArea();
                            }
                            break;
                        default:
                            break;
                    }

                    userCoords[0] = 0;
                    userCoords[1] = 0;
                    userCoords[2] = 0;
                    continue;
                }
                else
                {
                    //If a menu option was not entered...

                    //Split up and validate the coordinates entered by the user.
                    userCoords = SplitCoords(userInput);
                    if ((userCoords[0] == 0 && userCoords[1] == 0 && userCoords[2] == 0) == true)
                    {
                        Console.ReadKey();
                    }
                }
            } while (userCoords[0] == 0 && userCoords[1] == 0 && userCoords[2] == 0);

            //Save the user's position choice.
            gameboardPosition.XAxis = userCoords[0];
            gameboardPosition.YAxis = userCoords[1];
            gameboardPosition.ZAxis = userCoords[2];
            
            //Return the user's positon choice.
            return gameboardPosition;
        }

        /// <summary>
        /// Tests the input provided to see if it is a valid int value.
        /// </summary>
        /// <returns>bool value: Bool value returned indicates whether or not the input is a valid int value, integer value: A coordinate that is a valid int value</returns>
        public bool IsIntegerValid(string userInput, int minimumValue, int maximumValue, out int CoordinateChoice)
        {
            //Variable Declarations.
            CoordinateChoice = 0;
            bool validateRange = false;
            bool validResponse = false;

            //If the min and max values are not zero, validate the range.
            //validateRange = (minimumValue != 0 && maximumValue != 0);
            if (minimumValue == 0 && maximumValue == 0)
            {
                //Validate Range...
                validateRange = false;
            }
            else
            {
                validateRange = true;
            }

            //Validate the user's response.
            //while (!validResponse)
            //{
            if (int.TryParse(userInput, out CoordinateChoice))
            {
                //The value entered is a valid integer...

                if (validateRange == true)
                {
                    //Check to make sure the integer entered is within the specified range...

                    if (CoordinateChoice >= minimumValue && CoordinateChoice <= maximumValue)
                    {
                        //The integer entered is within the specified range...

                        validResponse = true;
                    }
                    else
                    {
                        //The integer entered is not within the specified range...

                        //Display an error message in the Input Box area of the screen.
                        DisplayPositionPrompt();
                        Console.Write($"You must enter an integer value between {minimumValue} and {maximumValue}. Press Enter to try again.");
                    }
                }
                else
                {
                    //Do not check to make sure the integer entered is within the specified range...

                    validResponse = true;
                }
            }
            else
            {
                //Not a valid integer...

                //Display an error message in the Input Box area of the screen.
                DisplayPositionPrompt();
                Console.Write($"You must enter a valid integer. Press Enter to try again.");
            }
            //}

            return validResponse;
        }

        /// <summary>
        /// Gets the x, y, and z coordinates from the user in an ordered group (x, y, z).
        /// </summary>
        /// <returns>string value: The string value returned is a ordered group of coordinates for a 3D space.</returns>
        public string GetCoordinates()
        {
            //Variable Declarations.
            string coordinates = "";

            //Get the coordinates from the user.
            DisplayPositionPrompt();
            Console.Write("Enter the coordinates for your space (X, Y, Z): ");
            coordinates = Console.ReadLine();
            
            //Return the coordinates.
            return coordinates;
        }

        /// <summary>
        /// Splits the coordinates provided in the userInput parameter value and validates each coordinate.
        /// </summary>
        /// <param name="userinput"></param>
        /// <returns>int array: The int array contains the valid coordinates provided by the user.  If all elements in the array are 0, one or more coordinates were not valid.</returns>
        public int[] SplitCoords(string userinput)
        {
            //Variable Declarations.
            int coord = 0;
            string[] splitCoords;
            int[] userCoords = { 0, 0, 0 };

            //Check for comma seperation of the coordinates.
            if (userinput.IndexOf(',') != userinput.LastIndexOf(','))
            {
                //If there are commas seperating the coordinates...

                //Split the coordinates entered into an array.
                splitCoords = userinput.Split(',');
            }
            else
            {
                //If there are no commas seperating the coordinates...

                //Display an error message and return to the calling method.
                DisplayPositionPrompt();
                Console.Write("Your entry was invalid, press Enter to try again!!");
                return userCoords;
            }


            //Validate the coordinates entered by the user.
            for (int i = 0; i < splitCoords.Length; i++)
            {
                if (IsIntegerValid(splitCoords[i].Trim(), 1, 3, out coord) == true)
                {
                    //If the number is valid...

                    //Set the coordinate in the next index in the user coodinates array.
                    userCoords[i] = coord;
                }
                else
                {
                    //If the number is not valid...

                    //Reset the user coodinates array and break out of the loop.
                    userCoords[0] = 0;
                    userCoords[1] = 0;
                    userCoords[2] = 0;
                    break;
                }
            }

            //Return the coordinates.
            return userCoords;
        }
        
        /// <summary>
        /// Validate the player's coordinate response for integer and range
        /// </summary>
        /// <param name="coordinateType">an integer value within proper range or -1</param>
        /// <returns></returns>
        private int PlayerCoordinateChoice(string coordinateType)
        {
            int tempCoordinate = -1;
            int numOfPlayerAttempts = 1;
            int maxNumOfPlayerAttempts = 4;

            while ((numOfPlayerAttempts <= maxNumOfPlayerAttempts))
            {
                if (int.TryParse(Console.ReadLine(), out tempCoordinate))
                {
                    //
                    // Player response within range
                    //
                    if (tempCoordinate >= 1 && tempCoordinate <= _gameboard.MaxNumOfRowsColumns)
                    {
                        return tempCoordinate;
                    }
                    //
                    // Player response out of range
                    //
                    else
                    {
                        DisplayMessageBox(coordinateType + " numbers are limited to (1,2,3)");
                    }
                }
                //
                // Player response cannot be parsed as integer
                //
                else
                {
                    DisplayMessageBox(coordinateType + " numbers are limited to (1,2,3)");
                }

                //
                // Increment the number of player attempts
                //
                numOfPlayerAttempts++;
            }

            //
            // Player used maximum number of attempts, set view state and return
            //
            CurrentViewState = ViewState.PlayerUsedMaxAttempts;
            return tempCoordinate;
        }

        #endregion
    }
}
