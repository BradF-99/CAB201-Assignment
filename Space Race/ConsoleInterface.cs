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
            Console.WriteLine("Welcome to Space Race.\n");
            Console.WriteLine("This game is for 2 to 6 players.\n");
            Console.Write("How many players (2-6)? : ");
            string input = Console.ReadLine();
            int players = Check_Input(input);
            SpaceRaceGame.NumberOfPlayers = players;
            SpaceRaceGame.SetUpPlayers();
        } //end DisplayIntroductionMessage

        /// <summary>
        /// Displays a prompt and waits for a keypress.
        /// Pre:  none
        /// Post: a key has been pressed.
        /// </summary>
        static void PressEnter()
        {
            Console.Write("\nPress Enter to terminate program ...");
            Console.ReadLine();
        } // end PressAny

        public static int Check_Input(string input)
        {

            bool TryInput = int.TryParse(input, out int int_input);
            if ((int_input < 2 || int_input > 6) || !TryInput)
            {
                Console.Write("Invalid amount of players entered, please enter a number between 2 and 6 : ");
                string input2 = Console.ReadLine();
                int_input = Check_Input(input2);
            }
            return int_input;

        }

    }//end Console class
}
