using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model
{
    class BulletsManager
    {
        #region DataMembers

        private double backgroundHeight;
        private double backgroundWidth;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the player bullet.
        /// </summary>
        /// <value>
        ///     The player bullet.
        /// </value>
        public ShipBullet PlayerBullet { get; set; }

        /// <summary>
        ///     Gets or sets the enemy bullets.
        /// </summary>
        /// <value>
        ///     The enemy bullets.
        /// </value>
        public IList<GameObject> EnemyBullets { get; set; }

        /// <summary>
        ///     Gets or sets whether a bullet was fired.
        /// </summary>
        /// <value>
        ///     Weather a bullet was fired.
        /// </value>
        public bool BulletFired { get; set; }

        /// <summary>
        ///     Gets or sets whether the enemy fired.
        /// </summary>
        /// <value>
        ///     Whether an enemy fired..
        /// </value>
        public bool EnemyFired { get; set; }

        #endregion

        #region Constructor

        public BulletsManager(double backgroundWidth, double backgroundHeight)
        {
            if (backgroundHeight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundHeight));
            }

            if (backgroundWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundWidth));
            }

            this.backgroundHeight = backgroundHeight;
            this.backgroundWidth = backgroundWidth;
        }

        #endregion

        #region Methods

        public void IntializeBullets()
        {

        }

        /// <summary>
        /// Creates and places a bullet as long as there isn't another on the screen.
        /// Precondition: background != null
        /// post-condition: bullet has been placed on the canvas or bullet already exists.
        /// </summary>
        /// <param name="background"></param>
        public void CreateAndPlacePlayerShipBullet(Canvas background)
        {
            if (!background.Children.Contains(this.PlayerBullet.Sprite))
            {
                background.Children.Add(this.PlayerBullet.Sprite);
                this.placePlayerBullet();
                this.BulletFired = true;
            }
        }

        private void placePlayerBullet()
        {
            //this.PlayerBullet.X = this.PlayerShip.X + 1; //(this.playerShip.Width / 2);
            //this.PlayerBullet.Y = this.PlayerShip.Y - 15;
        }

        #endregion
    }
}
