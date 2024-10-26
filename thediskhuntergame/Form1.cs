using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace thediskhuntergame
{
    public partial class Form1 : Form
    {
        // Class-level variables to store top scores, game settings, and game state
        private List<(string playerName, int score, int R, int T, DateTime timestamp)> topScores;  // List to store top scores with player info
        private Random random;  // Random object for generating random positions and disk sizes
        private int score;  // Player's current score
        private int timeRemaining;  // Remaining time for the game
        private int currentR;  // Disk radius for the current game
        private int currentT;  // Disk time (speed) for the current game
        private Panel gamePanel;  // Panel to display the game
        private List<Rectangle> disks;  // List to store the positions of the disks
        private bool gameOver;  // Boolean to check if the game is over
        private Color diskColor = Color.White;  // Default disk color
        private string filePath = Path.Combine(Application.StartupPath, "topscores.txt");  // Path to the top scores file
        private int lastPointsForHit = 0;  // To store the last points from a disk hit

        public Form1()
        {
            InitializeComponent();  // Initialize the form and components
            disks = new List<Rectangle>();  // Initialize the list to store disk positions
            timer1.Enabled = false;  // Disable the first timer
            timer.Enabled = false;  // Disable the second timer
            random = new Random();  // Create a new random generator
            score = 0;  // Initialize the score
            timeRemaining = 60;  // Set the initial game time to 60 seconds
            topScores = LoadTopScoresFromFile("topscores.txt");  // Load top scores from file
            DisplayTopScores(topScores);  // Display top scores on the form
            InitializeGame();  // Initialize the game settings
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load and display top scores when the form is loaded
            topScores = LoadTopScoresFromFile("topscores.txt");
            DisplayTopScores(topScores);
            this.DoubleBuffered = true; // Enable double buffering

        }

        // Initialize the game panel and timers
        private void InitializeGame()
        {
      
            gameOver = false;  // Set game over to false to start the game
            UpdateScore(0);  // Reset score to 0 at the start of a new game
            timeRemaining = 60;  // Reset time to 60 seconds


            var difficulty = ShowDifficultyDialog();  // Show the difficulty dialog to choose difficulty settings
            if (!difficulty.HasValue)
            {
                // If difficulty is not set, show an error message
                MessageBox.Show("Please choose a difficulty level. Click NewGame", "Difficulty Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;  // Stop further execution and return
            }
            if (difficulty.HasValue)
            {
                currentT = difficulty.Value.T;  // Get the T value from the dialog (disk time)
                currentR = difficulty.Value.R;  // Get the R value from the dialog (disk radius)
                StartNewGame(currentT, currentR);  // Start a new game with the chosen difficulty
            }
        }

        private (int T, int R)? ShowDifficultyDialog()
        {
            // Create a dialog to get T and R from the user for difficulty settings
            using (Form dialog = new Form())
            {
                dialog.Text = "Set Difficulty";
                dialog.Size = new Size(400, 200);

                Label labelT = new Label() { Text = "Disk Time (T):", Location = new Point(10, 20) };
                NumericUpDown inputT = new NumericUpDown() { Location = new Point(150, 20), Minimum = 1, Maximum = 10, Value = 1 };  // Default to 1 for easy mode

                Label labelR = new Label() { Text = "Disk Radius (R):", Location = new Point(10, 60) };
                NumericUpDown inputR = new NumericUpDown() { Location = new Point(150, 60), Minimum = 10, Maximum = 100, Value = 10, Increment = 10 };  // Default to 10 for easy mode

                Button okButton = new Button() { Text = "OK", Location = new Point(130, 100) };
                okButton.Size = new Size(45, 35);
                okButton.Click += (s, args) => { dialog.DialogResult = DialogResult.OK; };

                dialog.Controls.Add(labelT);
                dialog.Controls.Add(inputT);
                dialog.Controls.Add(labelR);
                dialog.Controls.Add(inputR);
                dialog.Controls.Add(okButton);
                dialog.BackColor = Color.Black;
                dialog.ForeColor = Color.White;
                dialog.StartPosition = FormStartPosition.CenterScreen;
                dialog.MaximumSize = new Size(400, 200);
                dialog.MaximizeBox = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    int T = (int)inputT.Value;  // Get T value (disk time)
                    int R = (int)inputR.Value;  // Get R value (disk radius)
                    return (T, R);  // Return the chosen parameters
                }
                else
                {
                    return null;  // If canceled, return null
                }
            }
        }

        // Timer tick event for randomly generating disks
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 200 * currentT;
            if (gameOver) return;  // Skip if the game is over

            // Remove previous disk if it exists
            if (disks.Count > 0)
            {
                disks.RemoveAt(0); // Remove the previous disk
            }

            // Randomly determine disk size and position
            int diskDiameter = currentR * 2;  // Use the current radius to calculate the diameter
            int x = random.Next(0, panel1.Width - diskDiameter);  // Random x position for the disk
            int y = random.Next(0, panel1.Height - diskDiameter);  // Random y position for the disk

            // Create a disk and add it to the list
            Rectangle disk = new Rectangle(x, y, diskDiameter, diskDiameter);
            disks.Add(disk);
     
            // Redraw the panel to show the disk
            panel1.Refresh();
        }


        // Draw the disks on the panel
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
                using (Brush diskBrush = new SolidBrush(diskColor)) // Create a new SolidBrush with diskColor
                foreach (var disk in disks)
                {
                    e.Graphics.FillEllipse(diskBrush, disk);  // Draw disks as white circles
                }

                  if (gameOver) return;  // Stop drawing disks after the game is over
        }

        // Handle mouse click to hit a disk
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            bool hitDisk = false;
            int pointsForHit = 0;

            for (int i = disks.Count - 1; i >= 0; i--)
            {
                if (disks[i].Contains(e.Location)) // Check if the clicked location is inside the disk
                {
                    hitDisk = true;
                    pointsForHit = CalculateScoreForDisk(disks[i].Width / 2, currentT);  // Calculate points for a hit
                    lastPointsForHit = pointsForHit;  // Store points from the hit for future use
                    
                    disks.RemoveAt(i);  // Remove the disk immediately
                    panel1.Invalidate();  // Redraw the panel after removal

                    break;  // Exit the loop after removing the clicked disk
                           
                }
            }
            // If a disk was hit, increase the score by pointsForHit; otherwise, subtract the same points for a miss
            if (hitDisk)
            {
                UpdateScore(pointsForHit);  // Add the points for a hit
            }
            else if (hitDisk == false && lastPointsForHit > 0)  // Only subtract if points were gained before
            {

                UpdateScore(-lastPointsForHit);  // Subtract the same points for a miss
            }
        }

        // Calculate score based on disk radius and game speed (T)
        private int CalculateScoreForDisk(int radius, int gameSpeedT)
        {
            int R = 100 / radius; // to adjust scoring for higher radius
            int score = (20 / currentT) * R;
            // the formula for score
            return Math.Max(score, 1);  // Ensure score is at least 1
        }

        // Update the score and display it on the label
        private void UpdateScore(int points)
        {
            score += points;  // Update the score with positive or negative points based on the click
            score = Math.Max(score, 0);  // Ensure score doesn't go below 0
            scorelabel.Text = $"Score: {score}";  // Update the label with the new score
        }

        // Timer tick event for updating remaining time
        private void timer_Tick(object sender, EventArgs e)
        {
            timeRemaining--;  // Decrease the remaining time by 1
            time.Text = $"Time Remaining: {timeRemaining}";  // Update the label showing the remaining time

            if (timeRemaining <= 0)  // If time is up, stop the game
            {
                timer.Stop();  // Stop the main timer
                MessageBox.Show($"Game Over! Final Score: {score}");  // Show final score
                GameOver(score);  // Trigger GameOver method when time runs out
            }
        }
        // Start of the game class
        private void newgame_Click(object sender, EventArgs e)
        {
            // Reset score and timers when starting a new game
            score = 0;
            timer.Enabled = false;  // Stop the main timer
            timer1.Enabled = false;  // Stop the disk generation timer
            timeRemaining = 60;  // Reset the time to 60 seconds

            // Show the difficulty dialog to allow the user to set game difficulty
            var difficulty = ShowDifficultyDialog();  // Show the dialog and get difficulty settings

            // If a difficulty is chosen, start a new game with the selected parameters
            if (difficulty.HasValue)
            {
                currentT = difficulty.Value.T;  // Get the T value (disk time)
                currentR = difficulty.Value.R;  // Get the R value (disk radius)
                StartNewGame(currentT, currentR);  // Start the game with chosen parameters
            }

        }

        // Start a new game and reset relevant parameters
        private void StartNewGame(int T, int R)
        {
            // Reset score, time, and game state
            UpdateScore(0);  // Reset score to 0
            timeRemaining = 60;  // Reset time to 60 seconds
            timer1.Start();
            timer.Start(); 
            currentR = R;  // Set the disk size based on difficulty
            currentT = T;
            gameOver = false;  // Set the game over flag to false
            disks.Clear();  // Clear any existing disks
            Refresh();
            time.Text = "Time Remaining: 60";  // Reset time display


        }

        // Save the top scores to a file
        private void SaveTopScoresToFile(string filePath, List<(string playerName, int score, int R, int T, DateTime timestamp)> topScores)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write each top score entry to the file
                foreach (var scoreEntry in topScores)
                {
                    // Format the entry with player name, score, disk radius (R), disk time (T), and timestamp
                    string line = $"{scoreEntry.playerName}:{scoreEntry.score}:{scoreEntry.R}:{scoreEntry.T}:{scoreEntry.timestamp:yyyy-MM-dd HH:mm:ss}";
                    writer.WriteLine(line);  // Write the line to the file
                }
            }
        }

        // Load the top scores from a file
        private List<(string playerName, int score, int R, int T, DateTime timestamp)> LoadTopScoresFromFile(string filePath)
        {
            List<(string playerName, int score, int R, int T, DateTime timestamp)> topScores = new List<(string playerName, int score, int R, int T, DateTime timestamp)>();

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Error loading top scores: File not found at {filePath}", "Error");
                return topScores;  // Return empty list if file doesn't exist
            }

            var lines = File.ReadAllLines(filePath);  // Read all lines from the file
            foreach (var line in lines)
            {
                // Split the line by colon (':') and ensure we have the correct number of parts
                var parts = line.Split(new char[] { ':' }, 5);

                // Ensure we have exactly 5 parts (player, score, R, T, and timestamp)
                if (parts.Length == 5 &&
                    int.TryParse(parts[1], out int score) &&
                    int.TryParse(parts[2], out int R) &&
                    int.TryParse(parts[3], out int T) &&
                    DateTime.TryParse(parts[4], out DateTime timestamp))
                {
                    string playerName = parts[0];  // Extract player name
                    topScores.Add((playerName, score, R, T, timestamp));  // Add valid entry to the list
                }
                else
                {
                    // Show an error message if the line format is invalid
                    MessageBox.Show($"Error loading top scores: Invalid line format or data (line: {line})", "Error");
                }
            }

            return topScores;  // Return the list of top scores
        }

        // Display the top scores in the ListBox
        private void DisplayTopScores(List<(string playerName, int score, int R, int T, DateTime timestamp)> topScores)
        {
            Top20.Items.Clear();  // Clear previous items in the ListBox
            foreach (var scoreEntry in topScores)
            {
                // Add each top score to the ListBox with the format: Player Name : Score
                Top20.Items.Add($"{scoreEntry.playerName} : {scoreEntry.score}");
            }
        }

        // Handle the end of the game and update top scores if necessary
        private void GameOver(int playerScore)
        {
            gameOver = true;  // Set game over flag to true

            // Check if the player's score qualifies for the top 20 list
            if (topScores.Count < 20 || playerScore > topScores.Min(s => s.score))
            {
                // Prompt the user to enter their name
                using (Form nameDialog = new Form())
                {
                    nameDialog.Text = "Enter Your Name";
                    nameDialog.Size = new Size(450, 150);

                    Label nameLabel = new Label()
                    {
                        Text = "Your Name:",
                        Location = new Point(10, 20),
                        AutoSize = true
                    };

                    // Increase the width of the TextBox and adjust its location
                    TextBox nameInput = new TextBox()
                    {
                        Location = new Point(100, 20),
                        Width = 200,
                        MaxLength = 15,  // Limit the name length to 15 characters
                        Padding = new Padding(5)  // Add padding inside the TextBox
                    };

                    // Button to submit the name
                    Button okButton = new Button() { Text = "OK", Location = new Point(100, 60), Width = 45, Height = 35 };
                    okButton.Click += (s, e) => { nameDialog.DialogResult = DialogResult.OK; };

                    // Add controls to the dialog form
                    nameDialog.Controls.Add(nameLabel);
                    nameDialog.Controls.Add(nameInput);
                    nameDialog.Controls.Add(okButton);

                    // Customize appearance
                    nameDialog.BackColor = Color.Black;
                    nameDialog.ForeColor = Color.White;
                    nameInput.BackColor = Color.White;
                    nameInput.ForeColor = Color.Black;
                    nameDialog.StartPosition = FormStartPosition.CenterScreen;

                    // Show the dialog and process the result
                    if (nameDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(nameInput.Text))
                    {
                        string playerName = nameInput.Text;  // Get player name from input
                        UpdateTopScores(playerName, playerScore, currentR, currentT);  // Update the top scores list
                    }
                }
            }
        }

        // Update the top scores list and save it to the file
        private void UpdateTopScores(string playerName, int playerScore, int R, int T)
        {
            DateTime timestamp = DateTime.Now;  // Get the current timestamp
            topScores.Add((playerName, playerScore, R, T, timestamp));  // Add new score entry

            // Sort the top scores in descending order and keep only the top 20 scores
            topScores = topScores.OrderByDescending(s => s.score).Take(20).ToList();

            // Save the updated list to the file
            SaveTopScoresToFile("topscores.txt", topScores);

            // Refresh the ListBox display
            DisplayTopScores(topScores);
        }

        // Handle the double-click event on the top scores ListBox
        private void Top20_DoubleClick(object sender, EventArgs e)
        {
            if (Top20.SelectedItem != null)  // Check if an item is selected
            {
                var selectedScore = topScores[Top20.SelectedIndex];  // Get the selected score entry
                string message = $"Score: {selectedScore.score}\n" +
                                 $"Date/Time: {selectedScore.timestamp}\n" +
                                 $"Disk Radius (R): {selectedScore.R}\n" +
                                 $"Disk Time (T): {selectedScore.T}";

                // Show a message box with details of the selected score
                MessageBox.Show(message, "Score Details");
            }
        }

        // Handle the properties button click to allow customization
        private void properties_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;  // Stop the main timer
            timer1.Enabled = false;  // Stop the disk generation timer
            gameOver = true;  // Set game over flag

            // Show the properties form for color customization
            using (Form2 propertiesForm = new Form2())
            {
                if (propertiesForm.ShowDialog() == DialogResult.OK)
                {
                    // Get the colors from Form2
                    this.BackColor = propertiesForm.BackgroundColor;  // Set form background color
                    panel1.BackColor = propertiesForm.BoardColor;  // Set panel background color
                    diskColor = propertiesForm.DiskColor;  // Set disk color

                    // Redraw the panel to reflect the new disk color
                    panel1.Invalidate();
                }
            }

            // Restart the game if time remaining is still positive
            if (timeRemaining > 0)
            {
                gameOver = false;
                timer.Enabled = true;
                timer1.Enabled = true;
            }
        }
    }
}