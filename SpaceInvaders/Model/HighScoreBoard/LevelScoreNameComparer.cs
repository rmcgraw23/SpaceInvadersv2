using System;
using System.Collections.Generic;

namespace SpaceInvaders.Model.HighScoreBoard
{
    /// <summary>
    ///     Creates a new comparer interface for sort by name first.
    /// </summary>
    /// <seealso cref="HighScore" />
    public class LevelScoreNameComparer : IComparer<HighScore>
    {
        #region Methods

        int IComparer<HighScore>.Compare(HighScore score1, HighScore score2)
        {
            if (score1 == null || score2 == null)
            {
                throw new ArgumentNullException();
            }

            if (score1.Level > score2.Level)
            {
                return -1;
            }

            if (score1.Level < score2.Level)
            {
                return 1;
            }

            if (score1.Score > score2.Score)
            {
                return -1;
            }

            if (score1.Score < score2.Score)
            {
                return 1;
            }

            switch (string.Compare(score1.Name, score2.Name, StringComparison.Ordinal))
            {
                case -1:
                    return -1;
                case 1:
                    return 1;
                default:
                    return 0;
            }
        }

        #endregion
    }
}