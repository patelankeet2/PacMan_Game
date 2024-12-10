using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib; // Importing the Windows Media Player library for playing sounds

namespace PacMan_Game
{
    // The SoundManager class is responsible for managing the game's sound effects
    internal class SoundManager
    {
        private WindowsMediaPlayer coinsound; // WindowsMediaPlayer instance for playing coin sound
        private WindowsMediaPlayer playsound; // WindowsMediaPlayer instance for playing game sound

        // Constructor initializes the WindowsMediaPlayer objects for sound
        public SoundManager()
        {
            coinsound = new WindowsMediaPlayer(); // Initialize the coin sound player
            playsound = new WindowsMediaPlayer(); // Initialize the game play sound player
        }

        // This method returns the full path to the sound file in the "Resources" folder
        private string GetResourcePath(string fileName)
        {
            // Combines the current directory (BaseDirectory) with the relative path to the "Resources" folder
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", fileName);
        }

        // Plays the coin sound effect when PacMan collects a coin
        public void PlayCoinSound()
        {
            coinsound.URL = GetResourcePath("coin.mp3"); // Set the URL to the "coin.mp3" sound file
            coinsound.controls.play(); // Play the coin sound
        }

        // Plays the main game sound (e.g., background music or sound effect)
        public void PlaySound()
        {
            playsound.URL = GetResourcePath("play.mp3"); // Set the URL to the "play.mp3" sound file
            playsound.controls.play(); // Play the game sound
        }
    }
}
