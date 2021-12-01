using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Model
{
    public class HighScoreBoard
    {

        #region Properties

        public IList<HighScore> HighScores { get; set; }

        #endregion

        #region Constructors

        public HighScoreBoard()
        {
            this.HighScores = new List<HighScore>();
        }

        #endregion

        #region Methods

        public void AddHighScore(HighScore score)
        {
            this.HighScores.Add(score);

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
            }

        }

        #endregion
    }
}
