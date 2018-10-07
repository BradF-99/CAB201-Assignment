using System.Drawing;
using System.ComponentModel;
using Object_Classes;


namespace Game_Logic_Class
{
    public static class SpaceRaceGame
    {
        // Minimum and maximum number of players.
        public const int MIN_PLAYERS = 2;
        public const int MAX_PLAYERS = 6;

        private static int numberOfPlayers = 2;  //default value for test purposes only 
        public static int NumberOfPlayers
        {
            get
            {
                return numberOfPlayers;
            }
            set
            {
                numberOfPlayers = value;
            }
        }

        //indicates if the game is over
        private static bool game_ended = false;
        public static bool Game_ended
        {
            get
            {
                return game_ended;
            }
            set
            {
                game_ended = value;
            }
        }

        //Current round
        private static int current_round = 1;
        public static int Current_round
        {
            get
            {
                return current_round;
            }
            set
            {
                current_round = value;
            }
        }

        public static string[] names = { "One", "Two", "Three", "Four", "Five", "Six" };  // default values
        
        // Only used in Part B - GUI Implementation, the colours of each player's token
        private static Brush[] playerTokenColours = new Brush[MAX_PLAYERS] { Brushes.Yellow, Brushes.Red,
                                                                       Brushes.Orange, Brushes.White,
                                                                      Brushes.Green, Brushes.DarkViolet};
        /// <summary>
        /// A BindingList is like an array which grows as elements are added to it.
        /// </summary>
        private static BindingList<Player> players = new BindingList<Player>();
        public static BindingList<Player> Players
        {
            get
            {
                return players;
            }
        }

        // The pair of die
        private static Die die1 = new Die(), die2 = new Die();
       

        /// <summary>
        /// Set up the conditions for this game as well as
        ///   creating the required number of players, adding each player 
        ///   to the Binding List and initialize the player's instance variables
        ///   except for playerTokenColour and playerTokenImage in Console implementation.
        ///   
        ///     
        /// Pre:  none
        /// Post:  required number of players have been initialsed for start of a game.
        /// </summary>
        public static void SetUpPlayers() 
        {

            // for number of players
            //      create a new player object
            //      initialize player's instance variables for start of a game
            //      add player to the binding list
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Player player = new Player(names[i]);
                player.RocketFuel = Player.INITIAL_FUEL_AMOUNT;
                player.Location = Board.StartSquare;
                player.Position = 0;
                players.Add(player);
            }

        }

            /// <summary>
            ///  Plays one round of a game
            /// </summary>
            public static void PlayOneRound() 
        {
            //checks if the winning message has already been displayed to not repeat
            bool end_message_displayed = false;
            System.Console.WriteLine("\n\n\tRound " + current_round + "\n");
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Players[i].Play(die1, die2);
                bool player_ended = CheckGameEnd(i);
                if (Game_ended)
                {
                    if(end_message_displayed == false)
                    {
                        System.Console.WriteLine("\tThe following player(s) have finished the game");
                        end_message_displayed = true;
                    }
                    if (player_ended)
                    {
                        System.Console.WriteLine("\t" + Players[i].Name + "\n");
                    }
                }
                else
                {
                    System.Console.WriteLine("\t" + Players[i].Name + " on square " + Players[i].Position + " with " + Players[i].RocketFuel + " yottawatt of power remaining \n");
                }
            }

            if (Game_ended)
            {
                System.Console.WriteLine("\tIndividual players finished at the locations specified \n\n");
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    System.Console.WriteLine("\t" + Players[i].Name + " on square " + Players[i].Position + " with " + Players[i].RocketFuel + " yottawatt of power remaining \n");
                }
            }
            current_round++;
        }

        public static bool CheckGameEnd(int i)
        {
            if (Players[i].AtFinish)
            {
                Game_ended = true;
                return true;
            }
            else
            {
                return false;
            }
        }

    }//end SnakesAndLadders
}
