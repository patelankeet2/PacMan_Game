using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan_Game
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        // Event handler for when the "Start" button (button1) is clicked
        private void button1_Click(object sender, EventArgs e)
        {
            PacManGame pacmangame = new PacManGame();  // Create a new instance of the PacManGame form (the actual game)
            pacmangame.Show();   // Show the PacManGame form
            this.Hide();    // Hide this form.
        }
    }
}
