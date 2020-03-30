using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{

    public partial class Form1 : Form
    {
        private int gameMode = main_menu.gameMode;

        private bool PlayerTurn = true;
        //the virtual table so we can check if the game is won 
        private char[,] Board = new char[3, 3];
        private char[,] exp =
                    {{ 'X', 'O', 'X' },
                     { 'O', 'O', 'X' },
                     { default(char), default(char), default(char)}};


        //reference to menu form 

        public Form1()
        {

            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            TableLayoutPanelCellPosition pp = new TableLayoutPanelCellPosition(1, 2);
            Console.WriteLine(FindBestMove(exp) + "te de e dd e");


            //create the buttons  
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
                for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)
                {
                    Button button = new Button();
                    button.Dock = DockStyle.Fill;

                    button.FlatStyle = FlatStyle.Popup;
                    button.ForeColor = Color.Red;
                    button.FlatAppearance.BorderSize = 0;
                    button.Margin = new Padding(0);
                    button.Font = new Font(button.Font.FontFamily, 60, FontStyle.Bold);
                    button.ForeColor = Color.Red;
                    button.Tag = new Tuple<int, int>(i, j); //position of the button 
                    //add an event handler  to every button pressed 
                    button.Click += Click_Board;
                    tableLayoutPanel1.Controls.Add(button);

                }
            if (gameMode == 1) MessageBox.Show("Play vs Player ", "Mode ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (gameMode == 2) MessageBox.Show("Play vs dummy Computer", "Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                MessageBox.Show("Play versus VERY smart computer", "Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Find best move for the computer to take 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private TableLayoutPanelCellPosition FindBestMove(char[,] board)
        {
            TableLayoutPanelCellPosition bestmove = new TableLayoutPanelCellPosition(-1, -1);//to return best move 

            //for every available move 
            int bestMoveVal = -100;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {

                    if (board[i, j] == default(char)) // if cell is empty 
                    {

                        //make the move 
                        board[i, j] = 'O';

                        int moveVal = Minmax1(board, false);

                        board[i, j] = default(char);
                        if (moveVal > bestMoveVal)
                        {
                            bestMoveVal = moveVal;

                            bestmove.Row = i;
                            bestmove.Column = j;

                        }
                        Console.WriteLine("val pt celula {0} este {1}", (i, j), moveVal);
                    }

                }

            return bestmove;

        }

        private void Afisare(char[,] board)
        {
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }


        }

        private void Click_Board(object s, EventArgs e)
        {

            //when the user presses a button 
            if (!(s is Button)) return;

            else
            {
                //cast the sender into a button 
                Button button = (Button)s;

                TableLayoutPanelCellPosition pos = new TableLayoutPanelCellPosition();
                TableLayoutPanelCellPosition lastpos = new TableLayoutPanelCellPosition();
                pos = tableLayoutPanel1.GetPositionFromControl(button);

                //it is player;s turn 
                if (PlayerTurn)
                {


                    Board[pos.Row, pos.Column] = 'X';

                    button.Text = "X";
                    button.Font = new Font(button.Font.FontFamily, 60, FontStyle.Bold);
                    button.ForeColor = Color.Red;
                    button.Enabled = false;
                    PlayerTurn = false;

                    //the pc moves 

                    if (!PlayerTurn && gameMode == 2 && !CheckWin(Board, 'X', pos) && !CheckTie(Board))
                    {
                        //dummy computer checks for first empty space 


                        int x_coord = CheckFreeSpace(Board, pos).Item1;
                        int y_coord = CheckFreeSpace(Board, pos).Item2;


                        Board[x_coord, y_coord] = 'O';

                        tableLayoutPanel1.GetControlFromPosition(y_coord, x_coord).Text = 'O'.ToString();
                        tableLayoutPanel1.GetControlFromPosition(y_coord, x_coord).Enabled = false;
                        PlayerTurn = true;

                    }

                    if (!PlayerTurn && gameMode == 3 && !CheckWin(Board, 'X', pos) && !CheckTie(Board))
                    {

                        //Minmax computer plays 


                        TableLayoutPanelCellPosition best = new TableLayoutPanelCellPosition();
                        best = FindBestMove(Board);
                        Console.WriteLine("best move for o is " + best.Row + best.Column);
                        Board[best.Row, best.Column] = 'O';
                        tableLayoutPanel1.GetControlFromPosition(best.Column, best.Row).Text = 'O'.ToString();
                        tableLayoutPanel1.GetControlFromPosition(best.Column, best.Row).Enabled = false;
                        PlayerTurn = true;
                        lastpos = best;

                    }

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
                    else if (CheckWin(Board, 'O', lastpos))
                    {

                        MessageBox.Show("O wins ! ", "WIN", MessageBoxButtons.OK);
                        DialogResult dg = MessageBox.Show("Do you wish to restart ? ", "Restart", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dg == DialogResult.Yes)
                        {
                            RestartGame(Board);
                        }


                    }

                    else if (CheckTie(Board))
                    {
                        MessageBox.Show("Game is a  tie ", "Tie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult dg = MessageBox.Show("Do you wish to restart ? ", "Restart", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dg == DialogResult.Yes)
                        {
                            RestartGame(Board);
                        }
                    }
                    //every time player click 

                }
                else if (!PlayerTurn)
                {
                    //player v player 

                    Board[pos.Row, pos.Column] = 'O';

                    button.Text = "O";
                    button.Enabled = false;
                    PlayerTurn = true;
                    //check if the move is win 

                    if (CheckWin(Board, 'O', pos))
                    {

                        MessageBox.Show("O wins ! ", "WIN", MessageBoxButtons.OK);
                        DialogResult dg = MessageBox.Show("Do you wish to restart ? ", "Restart", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dg == DialogResult.Yes)
                        {
                            RestartGame(Board);
                        }


                    }


                }

            }

        }

        /// <summary>
        /// A function that returns the best move at a given game-state 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="isMAximizing"></param>
        /// <param name="newPos"></param>
        /// <returns></returns>
        private int Minmax(char[,] board, bool isMaximizing, TableLayoutPanelCellPosition CurrentPos)
        {

            //base case if the board is in the terminal level  
            if (CheckTie(board)) return 0; //score 0 draw 


            else if (isMaximizing && CheckWin(board, 'O', CurrentPos)) { Console.WriteLine("+1"); return 1; }  //maximizing player wins 
            else if (!isMaximizing && CheckWin(board, 'X', CurrentPos)) { Console.WriteLine("-1"); return -1; } //minimizing player wins 


            else
            {

                if (isMaximizing)
                {   //it s O's turn to maximize 

                    int bestMove = int.MinValue;
                    //for each empty cell of board 
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (board[i, j] == default(char))
                            { //make the move 

                                board[i, j] = 'O';

                                //recursively check minmax  
                                TableLayoutPanelCellPosition position = new TableLayoutPanelCellPosition(j, i);//position of the move 

                                bestMove = Math.Max(bestMove, Minmax(board, !isMaximizing, position));
                                //undo the move 
                                board[i, j] = default(char);
                            }

                        }

                    }
                    Console.WriteLine("best move {0} at pos {1}", bestMove, CurrentPos);
                    return bestMove;
                }


                else
                {
                    //player's turn

                    int bestMove = int.MaxValue;
                    //for each empty cell of board 
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (board[i, j] == default(char))
                            { //make the move 
                                board[i, j] = 'X';

                                //recursively check minmax  
                                TableLayoutPanelCellPosition position = new TableLayoutPanelCellPosition(j, i);

                                bestMove = Math.Min(bestMove, Minmax(board, !isMaximizing, position));
                                //undo the move 
                                board[i, j] = default(char);
                            }

                        }
                    }

                    return bestMove;

                }

            }

        }
        private int Minmax1(char[,] board, bool isMaximizing)
        {

            //base case if the board is in the terminal level  
            int scoreval = score(board);
            if (scoreval == 10 || scoreval == -10) { return -scoreval; }
            if (CheckTie(board)) { return 0; } //game is a tie 



            if (isMaximizing)
            {   //it s O's turn to maximize 

                int bestMove = int.MinValue;
                //for each empty cell of board 
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == default(char))
                        { //make the move 

                            board[i, j] = 'O';

                            //recursively check minmax  

                            bestMove = Math.Max(bestMove, Minmax1(board, false));
                            //  Console.WriteLine(bestMove + "best");
                            //undo the move 
                            board[i, j] = default(char);
                        }

                    }

                }

                return bestMove;
            }

            else if (!isMaximizing)
            {
                //player's turn

                int bestMove = int.MaxValue;
                //for each empty cell of board 
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == default(char))
                        { //make the move 
                            board[i, j] = 'X';

                            //recursively check minmax  

                            bestMove = Math.Min(bestMove, Minmax1(board, true));
                            //undo the move 
                            board[i, j] = default(char);
                        }

                    }
                }

                return bestMove;
            }
            return -1111;
        }
        public static int score(char[,] b)
        {
            //x wins  - +10 
            //0 wins -10

            char opponent = 'O';
            char player = 'X';
            // Checking for Rows for X or O victory. 
            for (int row = 0; row < 3; row++)
            {
                if (b[row, 0] == b[row, 1] &&
                    b[row, 1] == b[row, 2])
                {
                    if (b[row, 0] == player)
                        return +10;
                    else if (b[row, 0] == opponent)
                        return -10;
                }
            }

            // Checking for Columns for X or O victory. 
            for (int col = 0; col < 3; col++)
            {
                if (b[0, col] == b[1, col] &&
                    b[1, col] == b[2, col])
                {
                    if (b[0, col] == player)
                        return +10;

                    else if (b[0, col] == opponent)
                        return -10;
                }
            }

            // Checking for Diagonals for X or O victory. 
            if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2])
            {
                if (b[0, 0] == player)
                    return +10;
                else if (b[0, 0] == opponent)
                    return -10;
            }

            if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0])
            {
                if (b[0, 2] == player)
                    return +10;
                else if (b[0, 2] == opponent)
                    return -10;
            }

            // Else if none of them have won then return 0 
            return 0;

        }

        private Tuple<int, int> CheckFreeSpace(char[,] board, TableLayoutPanelCellPosition lastMove)
        {

            // check the row , column or diag 

            //column check 
            for (int i = 0; i < 3; i++)

                if (board[lastMove.Row, i] == default(char)) return (lastMove.Row, i).ToTuple();



            for (int i = 0; i < 3; i++)

                if (board[i, lastMove.Column] == default(char)) return (i, lastMove.Column).ToTuple();


            for (int i = 0; i < 3; i++)
                if (board[i, i] == default(char)) return (i, i).ToTuple();

            //anti diagonal 
            for (int i = 0; i < 3; i++)
            {
                int j = 2 - i;
                if (board[i, j] == default(char)) return (i, j).ToTuple();
            }


            return (-1, -1).ToTuple();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// A method that checks the s  tate of the game 1 - p1 wins , 2 -p2 wins  , 3 -Tie 
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
                    if (Board[i, i] != player)
                    {
                        isWinDiag = false;
                        break;
                    }
                }

            }

            if (x_Coord == y_Coord && isWinDiag == true) return true;



            else if (x_Coord + y_Coord + 1 == 3 && isWinANTI == true) return true;



            else if (isWinColumn == true) return true;



            else if (isWinRow == true) return true;





            else return false;




        }

        private void RestartGame(char[,] board)
        {

            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
                for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)
                {
                    Button button = (Button)tableLayoutPanel1.GetControlFromPosition(i, j);
                    //reset every button  of panel 
                    button.Text = " ";
                    button.Enabled = true;
                    Board[i, j] = default(char);

                }
            PlayerTurn = true;
        }
        /// <summary>
        /// returns true if the game is a tie 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private bool CheckTie(char[,] board)
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

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    Console.Write(Board[i, j] + " ");

                }
                Console.WriteLine();
            }
        }
    }

}
