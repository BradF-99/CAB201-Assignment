using System;
//DO NOT DELETE the two following using statements *********************************
using Game_Logic_Class;
using Object_Classes;


namespace Space_Race
{
    class Console_Class
    {
        /// <summary>
        /// Algorithm below currently plays only one game
        /// 
        /// when have this working correctly, add the abilty for the user to 
        /// play more than 1 game if they choose to do so.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Board.SetUpBoard();
            DisplayIntroductionMessage();
            bool moreplays = true;
            while (moreplays)
            {
                SetupGame();
                Play();
                moreplays = AnotherGame();
            }

            /*                    
             Set up the board in Board class (Board.SetUpBoard)
             Determine number of players - initally play with 2 for testing purposes 
             Create the required players in Game Logic class
              and initialize players for start of a game             
             loop  until game is finished           
                call PlayGame in Game Logic class to play one round
                Output each player's details at end of round
             end loop
             Determine if anyone has won
             Output each player's details at end of the game
           */

            PressEnter();

        }//end Main

   
        /// <summary>
        /// Display a welcome message to the console
        /// Pre:    none.
        /// Post:   A welcome message is displayed to the console.
        /// </summary>
        static void DisplayIntroductionMessage()
        {
            Console.WriteLine(" ");
            Console.WriteLine("\tWelcome to Space Race.\n");
        } //end DisplayIntroductionMessage

        /// <summary>
        /// Displays the game rules and prompts user for input
        /// Pre:    none.
        /// Post:   Game rule message is displayed to console, input is checked and game is set up .
        /// </summary>
        static void SetupGame()
        {
            Console.WriteLine("\tThis game is for 2 to 6 players.");
            Console.Write("\tHow many players (2-6): ");
            string input = Console.ReadLine();
            int players = Check_Input(input);
            SpaceRaceGame.NumberOfPlayers = players;
            SpaceRaceGame.SetUpPlayers();
        }

        /// <summary>
        /// Prompts user to play another game.
        /// Checks their input and returns the appropriate result
        /// </summary>
        static bool AnotherGame()
        {
            Console.Write("\n\n\n\n\nPlay Again? (Y or N): ");
            string input = Console.ReadLine();
            string result = Check_String_Input(input);
            if (result == "N")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// Checks the users input and returns the input if it meets the requirements
        /// </summary>
        /// <param name="input"></param>
        static string Check_String_Input(string input)
        {
            if (input == "y" || input == "Y" || input == "yes")
            {
                return "Y";
            }else if(input == "n" || input == "N" || input == "no")
            {
                return "N";
            }
            else
            {
                Console.WriteLine("Invalid Input, please answer Y or N");
                Console.Write("Play Again? (Y or N): ");
                string input2 = Console.ReadLine();
                input = Check_String_Input(input2);
            }
            return input;
        }
        /// <summary
        /// Displays a prompt and waits for a keypress.
        /// Pre:  none
        /// Post: a key has been pressed.
        /// </summary>
        /// 
        static void PressEnter()
        {
            Console.WriteLine("\nThank you for playing Space Race");
            Console.WriteLine("\nPress Enter to terminate program ...");
            Console.ReadLine();
        } // end PressAny

        /// <summary>
        /// 
        /// Checks the users input and returns the input if it meets the requirements
        /// </summary>
        /// <param name="input"></param>
        public static int Check_Input(string input)
        {

            bool TryInput = int.TryParse(input, out int int_input);
            if ((int_input < 2 || int_input > 6) || !TryInput)
            {
                Console.Write("Invalid amount of players entered, please enter a number between 2 and 6: ");
                string input2 = Console.ReadLine();
                int_input = Check_Input(input2);
            }
            return int_input;

        }

        /// <summary>
        /// Pre:  none
        /// Post: Loops through PlayOneRound until game _ended is true.
        /// </summary>
        static void Play()
        {
            while (!SpaceRaceGame.Game_ended)
            {
                SpaceRaceGame.PlayOneRound();
                if (SpaceRaceGame.PlayersOutofFuel)
                {
                    break;
                }
                if (!SpaceRaceGame.Game_ended)
                {
                    Console.WriteLine("\nPress Enter key to play another round...");
                    Console.ReadLine();
                }
            }
        }

    }//end Console class
}
