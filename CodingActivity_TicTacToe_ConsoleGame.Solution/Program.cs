using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingActivity_TicTacToe_ConsoleGame;

namespace CodingActivity_TicTacToe_ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {

            GameController gameController = new GameController();

            //Validate the coordinates entered by the user.
            //int[] userCoords = { 0, 0, 0 };
            //do {
            //    Console.Clear();
            //    userCoords = SplitCoords(GetCoordinates());
            //    if ((userCoords[0] == 0 && userCoords[1] == 0 && userCoords[2] == 0) == true)
            //    {
            //        Console.ReadKey();
            //    }
            //} while (userCoords[0] == 0 && userCoords[1] == 0 && userCoords[2] == 0 );

            //Console.WriteLine("");
            //Console.WriteLine("Your Coordinates Are:");
            //Console.WriteLine("");
            //Console.WriteLine("X - " + userCoords[0]);
            //Console.WriteLine("Y - " + userCoords[1]);
            //Console.WriteLine("Z - " + userCoords[2]);

            //Console.ReadKey();
        }

        /// <summary>
        /// Tests the input provided to see if it is a valid int value.
        /// </summary>
        /// <returns>bool value: Bool value returned indicates whether or not the input is a valid int value, integer value: A coordinate that is a valid int value</returns>
        public static bool IsIntegerValid(string userInput, int minimumValue, int maximumValue, out int CoordinateChoice)
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
                            Console.WriteLine($"You must enter an integer value between {minimumValue} and {maximumValue}. Please try again.");
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
                    Console.WriteLine($"You must enter a valid integer. Please try again.");
                }
            //}

            return validResponse;
        }

        /// <summary>
        /// Gets the x, y, and z coordinates from the user in an ordered group (x, y, z).
        /// </summary>
        /// <returns>string value: The string value returned is a ordered group of coordinates for a 3D space.</returns>
        public static string GetCoordinates()
        {
            //Variable Declarations.
            string coordinates = "";

            //Get the coordinates from the user.
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
        public static int[] SplitCoords(string userinput)
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
                Console.WriteLine("Your entry was invalid, please try again!!");
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
    }
}
