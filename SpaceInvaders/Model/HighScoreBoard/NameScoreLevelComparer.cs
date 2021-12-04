using System;
using System.Collections.Generic;

namespace SpaceInvaders.Model.HighScoreBoard
{
    /// <summary>
    /// Creates a new comparer interface to compare highcores by level first.
    /// </summary>
    /// <seealso cref="HighScore" />
    public class NameScoreLevelComparer: IComparer<HighScore>
    {
        int IComparer<HighScore>.Compare(HighScore score1, HighScore score2)
        {
            if (score1 == null || score2 == null)
            {
                throw new ArgumentNullException();
            }
            if (String.Compare(score1.Name, score2.Name, StringComparison.Ordinal) == -1)
            {
                return -1;
            }
            else if (String.Compare(score1.Name, score2.Name, StringComparison.Ordinal) == 1)
            {
                return 1;
            }
            else
            {
                if (score1.Score > score2.Score)
                {
                    return -1;
                }
                else if (score1.Score < score2.Score)
                {
                    return 1;
                }
                else
                {
                    if (score1.Level > score2.Level)
                    {
                        return -1;
                    }
                    else if (score1.Level < score2.Level)
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

    }
}
