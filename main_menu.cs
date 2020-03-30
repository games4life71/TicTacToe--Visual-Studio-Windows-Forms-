using System;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class main_menu : Form
    {
        //1 - PVP , 2 -P v PC , 3 -v PC2
        public static int gameMode;

        public main_menu()
        {
            InitializeComponent();
        }

        private void btnPVP_Click(object sender, EventArgs e)
        {
            gameMode = 1;
            var form = new Form1();
            form.Show();
            var tis = new main_menu();
            tis.Hide();

        }

        private void btnPVPC1_Click(object sender, EventArgs e)
        {
            gameMode = 2;
            var form = new Form1();
            form.Show();
            var tis = new main_menu();
            tis.Hide();
        }

        private void btnPVPC2_Click(object sender, EventArgs e)
        {
            gameMode = 3;
            var form = new Form1();
            form.Show();
            var tis = new main_menu();

        }
    }
}
