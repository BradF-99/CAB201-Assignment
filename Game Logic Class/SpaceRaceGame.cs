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

        //indicates if all players run out of fuel.
        private static bool playersOutofFuel = false;
        public static bool PlayersOutofFuel
        {
            get
            {
                return playersOutofFuel;
            }
            set
            {
                playersOutofFuel = value;
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
            players.Clear();

            // for number of players
            //      create a new player object
            //      initialize player's instance variables for start of a game
            //      add player to the binding list
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Player player = new Player(names[i]) // simplified object initialisation
                {
                    RocketFuel = Player.INITIAL_FUEL_AMOUNT,
                    Location = Board.StartSquare,
                    Position = 0,
                    AtFinish = false,
                    HasPower = true
                };
                players.Add(player);
            }
            current_round = 1;
            game_ended = false;
            PlayersOutofFuel = false;
            

        }

            /// <summary>
            ///  Plays one round of a game
            /// </summary>
            public static void PlayOneRound(){
            bool end_message_displayed = false; // checks if the winning message has already been displayed to not repeat
            System.Console.WriteLine("\tRound " + current_round + "\n");
            int players_no_fuel = 0;
            for (int i = 0; i < numberOfPlayers; i++) // loop through each player
            {
                Players[i].Play(die1, die2);
                bool player_ended = CheckGameEnd(i);
                players_no_fuel += CheckOutofFuel(i);

                if (players_no_fuel == numberOfPlayers) //if all players have fun out of fuel
                {
                    System.Console.WriteLine("\tAll players have run out of fuel \n");
                    playersOutofFuel = true;
                }
                else  //at least 1 player has fuel
                {
                    if (Players[i].HasPower) //Prints current position of player if they have fuel
                    {
                        System.Console.WriteLine("\t" + Players[i].Name + " on square " + Players[i].Position + " with " + Players[i].RocketFuel + " yottawatt of power remaining");
                    }

                    if (!Game_ended) // if the game is still in play
                    {
                        if(!Players[i].HasPower) //if player is out of power
                        {
                            System.Console.WriteLine("\t" + Players[i].Name + " has run out of fuel!\n");
                        }
                    }
                    else //if at least one player has finished
                    {
                        if (end_message_displayed == false) //if the end game message has not been displayed prior
                        {
                            System.Console.WriteLine("\t\nThe following player(s) have finished the game");
                            end_message_displayed = true;
                        }
                        if (player_ended) //displays the player(s) that have won
                        {
                            System.Console.WriteLine("\t" + Players[i].Name + "\n");
                        }
                    }
                }
            }
                //List all players at the end of the game and which tile they were on and how much fuel left
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

        /// <summary>
        ///  Checks if players[i] is at the finish tile and returns true
        /// <param name="i">player number to be checked</param>
        /// </summary>
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

        /// <summary>
        ///  Checks if players[i] is out of fuel and returns 1
        /// <param name="i">player number to be checked</param>
        /// </summary>
        public static int CheckOutofFuel(int i)
        {
            if (Players[i].HasPower)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

    }//end SnakesAndLadders
}