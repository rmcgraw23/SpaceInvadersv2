using System;

namespace SpaceInvaders.Model.HighScoreBoard
{
    /// <summary>
    /// Creates a high score
    /// </summary>
    /// <seealso cref="System.IComparable" />
    public class HighScore : IComparable
    {

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public int Level { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HighScore"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="score">The score.</param>
        /// <param name="level">The level.</param>
        /// <exception cref="ArgumentNullException">Name</exception>
        public HighScore(string name, int score, int level)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Score = score;
            this.Level = level;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>either 1 or -1 if score is greater or less than the other score.</returns>
        public int CompareTo(object obj)
        {
            var score2 = obj as HighScore;
            if (score2 == null)
            {
                throw new ArgumentNullException();
            }
            if (this.Score > score2.Score)
            {
                return -1;
            }
            else if (this.Score < score2.Score)
            {
                return 1;
            }
            else
            {
                if (string.Compare(this.Name, score2.Name, StringComparison.Ordinal) < 0)
                {
                    return -1;
                }
                else if (string.Compare(this.Name, score2.Name, StringComparison.Ordinal) > 0)
                {
                    return 1;
                }
                else
                {
                    if (this.Level > score2.Level)
                    {
                        return -1;
                    }
                    else if (this.Level < score2.Level)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Name + " " + this.Score + " " + this.Level;
        }

        #endregion
    }
}
