using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    class BulletsManager
    {
        #region DataMembers

        private double backgroundHeight;
        private double backgroundWidth;

        private const int MaxLives = 3;

        private int enemyShipsPerRow = 8;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the player bullet.
        /// </summary>
        /// <value>
        ///     The player bullet.
        /// </value>
        public IList<ShipBullet> PlayerBullet { get; set; }

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
        /// <param name="playerShip"></param>
        public void CreateAndPlacePlayerShipBullet(Canvas background, GameObject playerShip)
        {
            ShipBullet bullet = new ShipBullet();

            if (this.PlayerBullet.Count < MaxLives)
            {
                this.PlayerBullet.Add(bullet);
                background.Children.Add(bullet.Sprite);
                this.placePlayerBullet(bullet, playerShip);
                this.BulletFired = true;
            }
        }

        private void placePlayerBullet(GameObject ship, GameObject bullet)
        {
            bullet.X = ship.X + 1;
            bullet.Y = ship.Y - 15;
        }

        private void placeEnemyShips(int count, GameObject ship)
        {
            var offset = (this.backgroundWidth - this.enemyShipsPerRow * ship.Width) / (this.enemyShipsPerRow + 1);
            ship.X = offset * (count + 1) + count * ship.Width;
            if (ship.Sprite is Level1EnemySprite)
            {
                ship.Y = 224;
            }
            else if (ship.Sprite is Level2EnemySprite)
            {
                ship.Y = 166;
            }
            else if (ship.Sprite is Level3EnemySprite)
            {
                ship.Y = 108;
            }
            else if (ship.Sprite is Level4EnemySprite)
            {
                ship.Y = 50;
            }
        }

        private void createAndPlaceEnemyBullets(Random random, GameObject ship, Canvas background)
        {
            var value = random.Next(0, 10);
            if (value == 0)
            {
                GameObject bullet = new ShipBullet();
                this.placeBulletsBellowEnemies(ship, bullet);

                background.Children.Add(bullet.Sprite);
                this.EnemyBullets.Add(bullet);

                if (this.EnemyBullets.Count == 0)
                {
                    this.EnemyFired = false;
                }
                else
                {
                    this.EnemyFired = true;
                }
            }
        }

        private void placeBulletsBellowEnemies(GameObject ship, GameObject bullet)
        {
            bullet.X = ship.X;
            bullet.Y = ship.Y + 18;
        }

        #endregion
    }
}
