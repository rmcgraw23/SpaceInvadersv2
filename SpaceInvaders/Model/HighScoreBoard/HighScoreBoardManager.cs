using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpaceInvaders.Model.HighScoreBoard
{
    /// <summary>
    /// Manages the high score board.
    /// </summary>
    public class HighScoreBoardManager
    {
        #region DataMembers

        /// <summary>
        /// The score
        /// </summary>
        private int score;

        /// <summary>
        /// The level
        /// </summary>
        private int level;

        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets the high scores.
        /// </summary>
        /// <value>
        /// The high scores.
        /// </value>
        public List<HighScore> HighScores { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HighScoreBoardManager"/> class.
        /// </summary>
        public HighScoreBoardManager()
        {
            this.HighScores = new List<HighScore>();
            this.score = 0;
            this.level = 0;
            this.SetHighScoreBoard();
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Occurs when [high score updated].
        /// </summary>
        public event EventHandler HighScoreUpdated;

        /// <summary>
        /// Called when [high score updated].
        /// </summary>
        public void OnHighScoreUpdated()
        {
            this.HighScoreUpdated?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Adds the high score.
        /// </summary>
        /// <param name="name">The name.</param>
        public void AddHighScore(string name)
        {
            this.HighScores.Add(new HighScore(name, this.score, this.level));
            this.HighScores.Sort();

            if (this.HighScores.Count >= 10)
            {
                HighScore lowestScore = null;
                foreach (var currentScore in this.HighScores)
                {
                    if (lowestScore == null)
                    {
                        lowestScore = currentScore;
                    }
                    else if (lowestScore.Score > currentScore.Score)
                    {
                        lowestScore = currentScore;
                    }
                }

                this.HighScores.Remove(lowestScore);

                string[] output = new []{""};
                foreach (var highScore in this.HighScores)
                {
                    _ = output.Append(highScore.Name + highScore.Score + highScore.Level);
                }
                File.WriteAllLines("scores.txt", output);
            }

        }

        /// <summary>
        /// Determines if score is Within the top ten.
        /// </summary>
        /// <returns></returns>
        public bool WithinTopTen()
        {
            foreach (var highScore in this.HighScores)
            {
                if (this.score > highScore.Score)
                {
                    this.OnHighScoreUpdated();
                    return true;
                }
            } return false;
        }

        /// <summary>
        /// Sets the high score board.
        /// </summary>
        public void SetHighScoreBoard()
        {
            try
            {
                string[] lines = File.ReadAllLines("scores.txt");

                foreach (var line in lines)
                {
                    string [] score = line.Split(" ");
                    this.HighScores.Add(new HighScore(score[0], Int32.Parse(score[1]), Int32.Parse(score[2])));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Gets the score and level.
        /// </summary>
        /// <param name="playerScore">The score.</param>
        /// <param name="playerLevel">The level.</param>
        public void GetScoreAndLevel(int playerScore, int playerLevel)
        {
            this.score = playerScore;
            this.level = playerLevel;
        }

        #endregion
    }
}
