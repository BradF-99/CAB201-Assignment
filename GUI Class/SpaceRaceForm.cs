﻿using System;
//  Uncomment  this using statement after you have remove the large Block Comment below 
using System.Drawing;
using System.Windows.Forms;
using Game_Logic_Class;
//  Uncomment  this using statement when you declare any object from Object Classes, eg Board,Square etc.
using Object_Classes;

namespace GUI_Class
{
    public partial class SpaceRaceForm : Form
    {
        // The numbers of rows and columns on the screen.
        const int NUM_OF_ROWS = 7;
        const int NUM_OF_COLUMNS = 8;

        // When we update what's on the screen, we show the movement of a player 
        // by removing them from their old square and adding them to their new square.
        // This enum makes it clear that we need to do both.
        enum TypeOfGuiUpdate { AddPlayer, RemovePlayer };


        public SpaceRaceForm()
        {
            InitializeComponent();

            Board.SetUpBoard();
            ResizeGUIGameBoard();
            SetUpGUIGameBoard();
            SetupPlayersDataGridView();
            btnRoll.Enabled = false;
            btnReset.Enabled = false;
            DetermineNumberOfPlayers();
            SpaceRaceGame.SetUpPlayers();
            //PrepareToPlay();
        }


        /// <summary>
        /// Handle the Exit button being clicked.
        /// Pre:  the Exit button is clicked.
        /// Post: the game is terminated immediately
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Resizes the entire form, so that the individual squares have their correct size, 
        /// as specified by SquareControl.SQUARE_SIZE.  
        /// This method allows us to set the entire form's size to approximately correct value 
        /// when using Visual Studio's Designer, rather than having to get its size correct to the last pixel.
        /// Pre:  none.
        /// Post: the board has the correct size.
        /// </summary>
        private void ResizeGUIGameBoard()
        {
            const int SQUARE_SIZE = SquareControl.SQUARE_SIZE;
            int currentHeight = tableLayoutPanel.Size.Height;
            int currentWidth = tableLayoutPanel.Size.Width;
            int desiredHeight = SQUARE_SIZE * NUM_OF_ROWS;
            int desiredWidth = SQUARE_SIZE * NUM_OF_COLUMNS;
            int increaseInHeight = desiredHeight - currentHeight;
            int increaseInWidth = desiredWidth - currentWidth;
            this.Size += new Size(increaseInWidth, increaseInHeight);
            tableLayoutPanel.Size = new Size(desiredWidth, desiredHeight);

        }// ResizeGUIGameBoard


        /// <summary>
        /// Creates a SquareControl for each square and adds it to the appropriate square of the tableLayoutPanel.
        /// Pre:  none.
        /// Post: the tableLayoutPanel contains all the SquareControl objects for displaying the board.
        /// </summary>
        private void SetUpGUIGameBoard()
        {
            for (int squareNum = Board.START_SQUARE_NUMBER; squareNum <= Board.FINISH_SQUARE_NUMBER; squareNum++)
            {
                Square square = Board.Squares[squareNum];
                SquareControl squareControl = new SquareControl(square, SpaceRaceGame.Players);
                AddControlToTableLayoutPanel(squareControl, squareNum);
            }//endfor

        }// end SetupGameBoard

        private void AddControlToTableLayoutPanel(Control control, int squareNum)
        {
            int screenRow = 0;
            int screenCol = 0;
            MapSquareNumToScreenRowAndColumn(squareNum, out screenRow, out screenCol);
            tableLayoutPanel.Controls.Add(control, screenCol, screenRow);
        }// end Add Control


        /// <summary>
        /// For a given square number, tells you the corresponding row and column number
        /// on the TableLayoutPanel.
        /// Pre:  none.
        /// Post: returns the row and column numbers, via "out" parameters.
        /// </summary>
        /// <param name="squareNumber">The input square number.</param>
        /// <param name="rowNumber">The output row number.</param>
        /// <param name="columnNumber">The output column number.</param>
        private static void MapSquareNumToScreenRowAndColumn(int squareNum, out int screenRow, out int screenCol)
        {
            /*
             * Rows
             * All odd rows are reversed !!
             * 0 is start and num is finish !!
             *
             * 0 - 7 is row 0 (Start @ 0)
             * 8 - 15 is row 1 (R)
             * 16 - 23 is row 2
             * 24 - 31 is row 3 (R)
             * 32 - 39 is row 4
             * 40 - 47 is row 5 (R)
             * 48 - 55 is row 6 (Finish @ 55)
             *
             * Top left is 0,0 - Bottom Right is 6,7
             */

            screenRow = (NUM_OF_ROWS - 1) - (squareNum / NUM_OF_COLUMNS); // subtract rows from square number / columns
            screenCol = squareNum % NUM_OF_COLUMNS; // divide squareNum by columns and use modulo as the column
            if (screenRow % 2 != 0) screenCol = (NUM_OF_COLUMNS - 1) - screenCol; // flip column if the row is odd

        }//end MapSquareNumToScreenRowAndColumn


        private void SetupPlayersDataGridView()
        {
            // Stop the playersDataGridView from using all Player columns.
            playersDataGridView.AutoGenerateColumns = false;
            // Tell the playersDataGridView what its real source of data is.
            playersDataGridView.DataSource = SpaceRaceGame.Players;

        }// end SetUpPlayersDataGridView



        /// <summary>
        /// Obtains the current "selected item" from the ComboBox
        ///  and
        ///  sets the NumberOfPlayers in the SpaceRaceGame class.
        ///  Pre: none
        ///  Post: NumberOfPlayers in SpaceRaceGame class has been updated
        /// </summary>
        private void DetermineNumberOfPlayers()
        {
            int numPlayers;
            // Store the SelectedItem property of the ComboBox in a string
            if (Int32.TryParse(comboNumPlayers.Text, out numPlayers))
            {
                SpaceRaceGame.NumberOfPlayers = numPlayers;
            }
            else
            {
                MessageBox.Show("Please enter a valid player count between 2 and 6.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Parse string to a number

            // Set the NumberOfPlayers in the SpaceRaceGame class to that number

        }//end DetermineNumberOfPlayers

        /// <summary>
        /// The players' tokens are placed on the Start square
        /// </summary>
        private void PrepareToPlay()
        {
            // More code will be needed here to deal with restarting 
            // a game after the Reset button has been clicked. 
            //
            // Leave this method with the single statement 
            // until you can play a game through to the finish square
            // and you want to implement the Reset button event handler.
            //

            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);

        }//end PrepareToPlay()


        /// <summary>
        /// Tells you which SquareControl object is associated with a given square number.
        /// Pre:  a valid squareNumber is specified; and
        ///       the tableLayoutPanel is properly constructed.
        /// Post: the SquareControl object associated with the square number is returned.
        /// </summary>
        /// <param name="squareNumber">The square number.</param>
        /// <returns>Returns the SquareControl object associated with the square number.</returns>
        private SquareControl SquareControlAt(int squareNum)
        {
            int screenRow;
            int screenCol;

            // Uncomment the following lines once you've added the tableLayoutPanel to your form. 
            //     and delete the "return null;" 
            //
            MapSquareNumToScreenRowAndColumn(squareNum, out screenRow, out screenCol);
            return (SquareControl)tableLayoutPanel.GetControlFromPosition(screenCol, screenRow);
        }


        /// <summary>
        /// Tells you the current square number of a given player.
        /// Pre:  a valid playerNumber is specified.
        /// Post: the square number of the player is returned.
        /// </summary>
        /// <param name="playerNumber">The player number.</param>
        /// <returns>Returns the square number of the player.</returns>
        private int GetSquareNumberOfPlayer(int playerNumber)
        {
            // Code needs to be added here.
            return SpaceRaceGame.Players[playerNumber].Position;
        }//end GetSquareNumberOfPlayer


        /// <summary>
        /// When the SquareControl objects are updated (when players move to a new square),
        /// the board's TableLayoutPanel is not updated immediately.  
        /// Each time that players move, this method must be called so that the board's TableLayoutPanel 
        /// is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the board's TableLayoutPanel shows the latest information 
        ///       from the collection of SquareControl objects in the TableLayoutPanel.
        /// </summary>
        private void RefreshBoardTablePanelLayout()
        {
            // Uncomment the following line once you've added the tableLayoutPanel to your form.
            tableLayoutPanel.Invalidate(true);
        }

        /// <summary>
        /// When the Player objects are updated (location, etc),
        /// the players DataGridView is not updated immediately.  
        /// Each time that those player objects are updated, this method must be called 
        /// so that the players DataGridView is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the players DataGridView shows the latest information 
        ///       from the collection of Player objects in the SpaceRaceGame.
        /// </summary>
        private void UpdatesPlayersDataGridView()
        {
            SpaceRaceGame.Players.ResetBindings();
        }

        /// <summary>
        /// At several places in the program's code, it is necessary to update the GUI board,
        /// so that player's tokens are removed from their old squares
        /// or added to their new squares. E.g. at the end of a round of play or 
        /// when the Reset button has been clicked.
        /// 
        /// Moving all players from their old to their new squares requires this method to be called twice: 
        /// once with the parameter typeOfGuiUpdate set to RemovePlayer, and once with it set to AddPlayer.
        /// In between those two calls, the players locations must be changed. 
        /// Otherwise, you won't see any change on the screen.
        /// 
        /// Pre:  the Players objects in the SpaceRaceGame have each players' current locations
        /// Post: the GUI board is updated to match 
        /// </summary>
        private void UpdatePlayersGuiLocations(TypeOfGuiUpdate typeOfGuiUpdate)
        {
            // Code needs to be added here which does the following:
            //
            //   for each player
            //       determine the square number of the player
            //       retrieve the SquareControl object with that square number
            //       using the typeOfGuiUpdate, update the appropriate element of 
            //          the ContainsPlayers array of the SquareControl object.
            //          
            for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++) // loop through each player
            {
                int squareNum = GetSquareNumberOfPlayer(i);
                SquareControl plySquare = SquareControlAt(squareNum);
                if (typeOfGuiUpdate == TypeOfGuiUpdate.AddPlayer) plySquare.ContainsPlayers[i] = true;
                if (typeOfGuiUpdate == TypeOfGuiUpdate.RemovePlayer) plySquare.ContainsPlayers[i] = false;
            }

            RefreshBoardTablePanelLayout();//must be the last line in this method. Do not put inside above loop.
        } //end UpdatePlayersGuiLocations

        private void resetGUI()
        {
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            comboNumPlayers.Enabled = true;
            playersDataGridView.Enabled = true;
            btnReset.Enabled = false;
            btnRoll.Enabled = false;
            grpboxStep.Enabled = true;
            SpaceRaceGame.SetUpPlayers();
            UpdatesPlayersDataGridView();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnRoll_Click(object sender, EventArgs e)
        {
            if (grpboxStep.Enabled) // lazy way of telling if it's our first roll
            {
                DetermineNumberOfPlayers();
                SpaceRaceGame.SetUpPlayers();
                PrepareToPlay();
                playersDataGridView.Enabled = false;
                grpboxStep.Enabled = false;
            }

            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer); // remove all the tokens before the round so we can paint them later
            btnRoll.Enabled = false;
            comboNumPlayers.Enabled = false;
            btnReset.Enabled = false;

            if (radioSingleStepTrue.Checked)
            {
                if (!SpaceRaceGame.Game_ended) SpaceRaceGame.PlayOneRound();
            }
            else if (radioSingleStepFalse.Checked)
            {
                while (!SpaceRaceGame.Game_ended) SpaceRaceGame.PlayOneRound(); // wao amazing
            }
            else // u gotta pick an option !
            {
                UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
                UpdatesPlayersDataGridView();
                MessageBox.Show("Please select a step option.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
            UpdatesPlayersDataGridView();
            btnReset.Enabled = true;
            btnRoll.Enabled = true;

            if (SpaceRaceGame.Game_ended)
            {

                string plyEnd = "";
                for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
                {
                    if (SpaceRaceGame.CheckGameEnd(i))
                    {
                        plyEnd = "\n" + plyEnd + SpaceRaceGame.Players[i].Name + " \n";
                    };

                }
                MessageBox.Show("The following players finished the game: \n" + plyEnd, "You're winner!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnRoll.Enabled = false;
            }


        }

        private void radioSingleStepTrue_CheckedChanged(object sender, EventArgs e)
        {
            btnRoll.Enabled = true;
            grpboxStep.Enabled = false;
        }

        private void radioSingleStepFalse_CheckedChanged(object sender, EventArgs e)
        {
            btnRoll.Enabled = true;
            grpboxStep.Enabled = false;
        }

        private void comboNumPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            DetermineNumberOfPlayers();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetGUI();
        }
    }// end class
}
