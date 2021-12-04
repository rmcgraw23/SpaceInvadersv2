using System;
using System.Collections.Generic;

namespace SpaceInvaders.Model.HighScoreBoard
{
    /// <summary>
    ///     Creates a new comparer interface to compare high scores by level first.
    /// </summary>
    /// <seealso cref="HighScore" />
    public class NameScoreLevelComparer : IComparer<HighScore>
    {
        #region Methods

        int IComparer<HighScore>.Compare(HighScore score1, HighScore score2)
        {
            if (score1 == null || score2 == null)
            {
                throw new ArgumentNullException();
            }

            switch (string.Compare(score1.Name, score2.Name, StringComparison.Ordinal))
            {
                case -1:
                    return -1;
                case 1:
                    return 1;
                default:
                {
                    if (score1.Score > score2.Score)
                    {
                        return -1;
                    }

                    if (score1.Score < score2.Score)
                    {
                        return 1;
                    }

                    if (score1.Level > score2.Level)
                    {
                        return -1;
                    }

                    if (score1.Level < score2.Level)
                    {
                        return 1;
                    }

                    return 0;
                }
            }
        }

        #endregion
    }
}