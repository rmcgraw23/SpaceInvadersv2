using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    /// Manages the PowerUp
    /// </summary>
    public class PowerUp : GameObject
    {
        #region DataMembers
        
        private const int SpeedXDirection = 0;
        private const int SpeedYDirection = 20;

        public int HitCount = 1;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerUp"/> class.
        /// </summary>
        public PowerUp()
        {
            Sprite = new PowerUpSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }

        #endregion

    }
}
