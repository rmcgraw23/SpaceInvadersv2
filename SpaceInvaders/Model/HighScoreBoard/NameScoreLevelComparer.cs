using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.Model.HighScoreBoard;

namespace SpaceInvaders.Model.HighScoreBoard
{
    public class NameScoreLevelComparer: IComparer<HighScore>
    {
        int IComparer<HighScore>.Compare(HighScore score1, HighScore score2)
        {
            if (score1.name.CompareTo(score2.name) == -1)
            {
                return -1;
            }
            else if (score1.name.CompareTo(score2.name) == 1)
            {
                return 1;
            }
            else
            {
                if (score1.score > score2.score)
                {
                    return -1;
                }
                else if (score1.score < score2.score)
                {
                    return 1;
                }
                else
                {
                    if (score1.level > score2.level)
                    {
                        return -1;
                    }
                    else if (score1.level < score2.level)
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
