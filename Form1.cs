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
    public partial class PacManGame : Form
    {
        // Boolean flags to track the player's movement direction
        bool goup, godown, goleft, goright, isGameOver;

        // Game variables
        int score, playerSpeed, redGhostSpeed, yellowGhostSpeed, pinkGhostX, pinkGhostY;

        // Create an instance of the SoundManager to manage game sounds
        private SoundManager soundManager;

        // Constructor
        public PacManGame()
        {
            InitializeComponent();
            soundManager = new SoundManager(); // Initialize the SoundManager
            resetGame(); // Reset the game to its initial state
        }

        // Method to handle key release (when the player stops pressing a key)
        private void keyisup(object sender, KeyEventArgs e)
        {
            // Stop the movement if the key is released
            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            // Reset the game if 'Enter' key is pressed after the game is over
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                resetGame();
            }
        }

        // Method to handle key press (when a key is pressed down)
        private void keyisdown(object sender, KeyEventArgs e)
        {
            // Set the movement direction based on the key pressed
            if (e.KeyCode == Keys.Up)
            {
                goup = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
        }

        // Main game timer, executes on every game tick
        private void mainGameTimer(object sender, EventArgs e)
        {
            // Play the main game sound and hide win/loss indicators
            soundManager.PlaySound();
            pictureBoxwin.Visible = false;
            pictureBoxloss.Visible = false;

            // Update the score display
            txtScore.Text = "Score: " + score;

            // Move PacMan based on key input
            if (goleft == true)
            {
                pacman.Left -= playerSpeed;
                pacman.Image = Properties.Resources.left;
            }
            if (goright == true)
            {
                pacman.Left += playerSpeed;
                pacman.Image = Properties.Resources.right;
            }
            if (godown == true)
            {
                pacman.Top += playerSpeed;
                pacman.Image = Properties.Resources.down;
            }
            if (goup == true)
            {
                pacman.Top -= playerSpeed;
                pacman.Image = Properties.Resources.Up;
            }

            // Implement screen wrapping (PacMan reappears on the opposite side)
            if (pacman.Left < -10)
            {
                pacman.Left = 680;
            }
            if (pacman.Left > 680)
            {
                pacman.Left = -10;
            }

            if (pacman.Top < -10)
            {
                pacman.Top = 550;
            }
            if (pacman.Top > 550)
            {
                pacman.Top = 0;
            }

            // Check for collisions with coins, walls, or ghosts
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    // Check for collision with coins
                    if ((string)x.Tag == "coin" && x.Visible == true)
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            score += 1; // Increase score when coin is collected
                            soundManager.PlayCoinSound(); // Play coin sound
                            x.Visible = false; // Hide the collected coin
                        }
                    }

                    // Check for collision with walls
                    if ((string)x.Tag == "wall")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameOver("You Lose!"); // End game if PacMan hits a wall
                            pictureBoxloss.Visible = true; // Show the loss screen
                        }

                        // Check if pink ghost hits a wall and reverse its direction
                        if (pinkGhost.Bounds.IntersectsWith(x.Bounds))
                        {
                            pinkGhostX = -pinkGhostX;
                        }
                    }

                    // Check for collision with ghosts
                    if ((string)x.Tag == "ghost")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameOver("You Lose!"); // End game if PacMan hits a ghost
                            pictureBoxloss.Visible = true; // Show the loss screen
                        }
                    }
                }
            }

            // Move ghosts
            redGhost.Left += redGhostSpeed;
            // Reverse direction if red ghost hits the boundaries
            if (redGhost.Bounds.IntersectsWith(pictureBox1.Bounds) || redGhost.Bounds.IntersectsWith(pictureBox2.Bounds))
            {
                redGhostSpeed = -redGhostSpeed;
            }

            yellowGhost.Left -= yellowGhostSpeed;
            // Reverse direction if yellow ghost hits the boundaries
            if (yellowGhost.Bounds.IntersectsWith(pictureBox3.Bounds) || yellowGhost.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                yellowGhostSpeed = -yellowGhostSpeed;
            }

            // Move pink ghost and reverse its direction if it hits the boundaries
            pinkGhost.Left -= pinkGhostX;
            pinkGhost.Top -= pinkGhostY;

            if (pinkGhost.Top < 0 || pinkGhost.Top > 520)
            {
                pinkGhostY = -pinkGhostY;
            }

            if (pinkGhost.Left < 0 || pinkGhost.Left > 620)
            {
                pinkGhostX = -pinkGhostX;
            }

            // Check if the game is won (if all coins are collected)
            if (score == 46)
            {
                gameOver("You Win!"); // End the game if the player wins
                pictureBoxwin.Visible = true; // Display win message
            }
        }

        // Method to reset the game to its initial state
        private void resetGame()
        {
            txtScore.Text = "Score: 0"; // Reset score display
            score = 0; // Reset score

            // Reset the ghost speeds and player speed
            redGhostSpeed = 5;
            yellowGhostSpeed = 5;
            pinkGhostX = 5;
            pinkGhostY = 5;
            playerSpeed = 8;

            isGameOver = false; // Game is not over initially

            // Reset positions of PacMan and the ghosts
            pacman.Left = 31;
            pacman.Top = 46;

            redGhost.Left = 208;
            redGhost.Top = 55;

            yellowGhost.Left = 448;
            yellowGhost.Top = 445;

            pinkGhost.Left = 525;
            pinkGhost.Top = 235;

            // Make all the game elements visible
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = true;
                }
            }

            gameTimer.Start(); // Start the game timer
        }

        // Method to handle the game over state
        private void gameOver(string message)
        {
            isGameOver = true; // Set game over flag

            gameTimer.Stop(); // Stop the game timer

            txtScore.Text = "Score: " + score + Environment.NewLine + message; // Display final score and game over message
            
        }
    }
}
