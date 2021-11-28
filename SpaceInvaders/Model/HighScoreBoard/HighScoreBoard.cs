using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Model
{
    class HighScoreBoard
    {

        #region Properties

        public IList<HighScore> HighScores;

        #endregion

        #region Constructors

        public HighScoreBoard()
        {
            this.HighScores = new List<HighScore>();
        }

        #endregion
    }
}
