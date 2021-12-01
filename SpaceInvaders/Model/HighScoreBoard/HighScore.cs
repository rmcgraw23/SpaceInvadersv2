using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Model
{
    public class HighScore : IComparable
    {

        #region Properties

        public string name { get; set; }

        public int score { get; set; }

        public int level { get; set; }

        #endregion

        #region Constructors

        public HighScore(string name, int score, int level)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(this.name));
            this.score = score;
            this.level = level;
        }

        #endregion

        #region Methods

        public int CompareTo(object obj)
        {
            HighScore score2 = obj as HighScore;
            if (this.score > score2.score)
            {
                return -1;
            }
            else if (this.score < score2.score)
            {
                return 1;
            }
            else
            {
                if (this.name.CompareTo(score2.name) < 0)
                {
                    return -1;
                }
                else if (this.name.CompareTo(score2.name) > 0)
                {
                    return 1;
                }
                else
                {
                    if (this.level > score2.level)
                    {
                        return -1;
                    }
                    else if (this.level < score2.level)
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

        #endregion
    }
}
