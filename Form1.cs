using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {

        private bool PlayerTurn = true;
        //the virtual table so we can check if the game is won 
        private char[,] Board = new char[3, 3]; 
        public Form1()
        {
            InitializeComponent();
        }

        
         
        private void Form1_Load(object sender, EventArgs e)
        {
            //create the buttons  
            for(int i = 0; i<tableLayoutPanel1.RowCount;i++)
                for(int j =  0; j<tableLayoutPanel1.ColumnCount;j++)
                {
                    Button button = new Button ();
                    button.Dock = DockStyle.Fill;
      
                    button.FlatStyle = FlatStyle.Popup;
                    button.ForeColor = Color.Red;
                    button.FlatAppearance.BorderSize = 0;
                    button.Margin = new Padding(0);
                    button.Tag = new Tuple<int, int>(i, j); //position of the button 
                    //add an event handler  to every button pressed 
                    button.Click += Click_Board;
                    tableLayoutPanel1.Controls.Add(button);

                }

        }

     

        private void Click_Board(object s  , EventArgs e )
        {
            //when the user presses a button 
            if (!(s is Button)) return; 

             else
            {
                //cast the sender into a button 
                Button button = (Button)s;

                if (PlayerTurn)
                {

                    TableLayoutPanelCellPosition pos = new TableLayoutPanelCellPosition();
                    pos = tableLayoutPanel1.GetPositionFromControl(button);

                    Board[pos.Row, pos.Column] = 'X';

                    button.Text = "X";
                    button.Font = new Font(button.Font.FontFamily,60, FontStyle.Bold);
                    button.ForeColor = Color.Red;
                    button.Enabled = false;
                    PlayerTurn = false;

                    //check if the move is win 

                    if (CheckWin(Board, 'X', pos))
                    {
                        MessageBox.Show("X wins ! ", "WIN", MessageBoxButtons.OK);
                        DialogResult dg = MessageBox.Show("Do you wish to restart ? ", "Restart", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dg == DialogResult.Yes)
                        {
                            RestartGame(Board);
                        }
                    }
                  else  if (CheckTie(Board))
                    {
                        MessageBox.Show("Game is tie ", "Tie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult dg = MessageBox.Show("Do you wish to restart ? ", "Restart", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dg == DialogResult.Yes)
                        {
                            RestartGame(Board);
                        }
                    }
                   
                } 
                else
                {
                    //the player2 turn  is 

                    TableLayoutPanelCellPosition pos = new TableLayoutPanelCellPosition();
                    pos = tableLayoutPanel1.GetPositionFromControl(button);

                    
                    Board[pos.Row, pos.Column] = 'O';
                    button.Text = "O";
                    button.ForeColor = Color.DarkBlue;
                    button.Font = new Font(button.Font.FontFamily, 60,FontStyle.Bold);
                    button.Enabled = false;
                    PlayerTurn = true;
                        

                    if (CheckWin(Board, 'O', pos))
                    {
                        MessageBox.Show("O  wins ! ", "WIN", MessageBoxButtons.OK);
                        DialogResult dg = MessageBox.Show("Do you wish to restart ? ", "Restart", MessageBoxButtons.YesNo , MessageBoxIcon.Question);
                        if (dg == DialogResult.Yes)
                        {
                            RestartGame(Board);
                        }
                    }
                   else  if (CheckTie(Board))
                    {
                        MessageBox.Show("Game is tie ", "Tie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult dg = MessageBox.Show("Do you wish to restart ? ", "Restart", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dg == DialogResult.Yes)
                        {
                            RestartGame(Board);
                        }
                    }

                

                }



            }

        }

           
        private void button1_Click_1(object sender, EventArgs e)
        {
            for(int i =  0; i<3; i++)

            {
                for (int j = 0; j < 3; j++)
                {

                    Console.Write(Board[i, j] + " ");

                }
                Console.WriteLine();
            }
        }



        /// <summary>
        /// A method that checks the state of the game 1 - p1 wins , 2 -p2 wins  , 3 -Tie 
        /// </summary>
        /// <param The virtual table ="Board"></param>
        /// <returns></returns>
        private bool CheckWin(char[,] Board, char player, TableLayoutPanelCellPosition pos)
        {



            //only ckeck the column , row or diagonal
            int x_Coord = pos.Row;
            int y_Coord = pos.Column;

            bool isWinDiag = true;
            bool isWinRow = true;
            bool isWinColumn = true;
            bool isWinANTI = true;

            //check the row 
            for (int i = 0; i < 3; i++)
            {
                if (Board[x_Coord, i] != player) { isWinRow = false; break; }
            }

            //check win in column 
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, y_Coord] != player) { isWinColumn = false; break; }
            }

            //check win in anti-diagonal 
            if (x_Coord + y_Coord + 1 == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    int j = 2 - i;
                    if (Board[i, j] != player)
                    {
                        isWinANTI = false;
                        break;
                    }

                }
            }
             if (x_Coord == y_Coord)
            {
                    
                for (int i = 0; i < 3; i++)
                {
                    if(Board[i,i] != player)
                    {
                        isWinDiag = false;
                        break;
                    }
                }

            }

            if (x_Coord == y_Coord && isWinDiag == true)  return true;
           
              
           
            else if (x_Coord + y_Coord + 1 == 3 && isWinANTI == true) return true;
           
                
            
            else if (isWinColumn == true)    return true;
           
             
            
           else if (isWinRow == true)     return true;
            
           


            
            else return false; 
           
         

            
        }
      
        
        private void RestartGame(char [,] board )
        {

            for(int i = 0; i<tableLayoutPanel1.RowCount;i++)
             for(int j = 0; j<tableLayoutPanel1.ColumnCount;j++)
                {
                    Button button = (Button)tableLayoutPanel1.GetControlFromPosition(i, j);
                    //reset every button  of panel 
                    button.Text = " ";
                    button.Enabled = true; 
                    Board[i, j] = default(char);
                    
                }
            PlayerTurn = true; 
        }

        private bool CheckTie(char[,] board )
        {


            for (int i = 0; i < 3; i++)

            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == default(char)) return false; 

                }

            }

            return true;
        }

        private void lblRestart_Click(object sender, EventArgs e)
        {
            RestartGame(Board);
        }
    }



    


}
