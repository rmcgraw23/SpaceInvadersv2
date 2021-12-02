using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    internal class Shield : GameObject
    {
        private const int SpeedXDirection = 0;
        private const int SpeedYDirection = 0;

        public int HitsRemaining { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Shield" /> class.
        /// </summary>
        public Shield()
        {
            Sprite = new ShieldSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
            this.HitsRemaining = 2;
        }
    }
}
