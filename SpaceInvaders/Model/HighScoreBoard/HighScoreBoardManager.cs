using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.Model.HighScoreBoard;

namespace SpaceInvaders.Model
{
    public class HighScoreBoardManager
    {
        #region DataMembers


        #endregion
        #region Properties

        public List<HighScore> HighScores { get; set; }

        #endregion

        #region Constructors

        public HighScoreBoardManager()
        {
            this.HighScores = new List<HighScore>();
            this.SetHighScoreBoard();
            
        }

        #endregion

        #region Methods

        public event EventHandler HighScoreUpdated;

        public void OnHighScoreUpdated()
        {
            this.HighScoreUpdated?.Invoke(this, EventArgs.Empty);
        }
        public void AddHighScore(HighScore score)
        {
            this.HighScores.Add(score);
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
                    else if (lowestScore.score > currentScore.score)
                    {
                        lowestScore = currentScore;
                    }
                }

                this.HighScores.Remove(lowestScore);

                string[] output = new []{""};
                foreach (var highScore in HighScores)
                {
                    output.Append(highScore.name + highScore.score + highScore.level);
                }
                File.WriteAllLines("scores.txt", output);
            }

        }

        public bool WithinTopTen(int score)
        {
            foreach (var highScore in this.HighScores)
            {
                if (score > highScore.score)
                {
                    return true;
                    this.OnHighScoreUpdated();
                }
            } return false;
        }

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

        #endregion
    }
}
