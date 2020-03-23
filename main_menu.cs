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
    public partial class main_menu : Form
    {
        public main_menu()
        {
            InitializeComponent();
        }

        private void btnPVP_Click(object sender, EventArgs e)
        {
            var form = new Form1();
            form.Show();
            var tis = new main_menu();
            tis.Hide();  
        
        }
    }
}
