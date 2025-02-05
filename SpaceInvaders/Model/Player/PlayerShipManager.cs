﻿using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model.Player
{
    /// <summary>
    ///     Manages the Player Ship.
    /// </summary>
    internal class PlayerShipManager
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the player ship.
        /// </summary>
        /// <value>
        ///     The player ship.
        /// </value>
        public PlayerShip PlayerShip { get; set; }

        /// <summary>
        ///     Gets or sets whether a bullet was fired.
        /// </summary>
        /// <value>
        ///     Weather a bullet was fired.
        /// </value>
        public bool BulletFired { get; set; }

        /// <summary>
        ///     Gets or sets the lives.
        /// </summary>
        /// <value>
        ///     The lives.
        /// </value>
        public int Lives { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerShipManager" /> class.
        ///     Precondition: none
        ///     Post-condition: gameBackground = background
        /// </summary>
        /// <param name="background">The background.</param>
        public PlayerShipManager(Canvas background)
        {
            this.gameBackground = background;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes the ships.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        public void InitializeShips()
        {
            this.BulletFired = false;
            this.Lives = 3;
        }

        /// <summary>
        ///     Creates places the player ship.
        ///     Precondition: none
        ///     Post-condition: the player ship has been placed on the background.
        /// </summary>
        public void CreateAndPlacePlayerShip()
        {
            this.PlayerShip = new PlayerShip();
            this.gameBackground.Children.Add(this.PlayerShip.Sprite);

            this.placePlayerShipNearBottomOfBackgroundCentered();
        }

        private void placePlayerShipNearBottomOfBackgroundCentered()
        {
            this.PlayerShip.X = this.gameBackground.Width / 2 - this.PlayerShip.Width / 2.0;
            this.PlayerShip.Y = this.gameBackground.Height - this.PlayerShip.Height - PlayerShipBottomOffset;
        }

        /// <summary>
        ///     Checks if the player was hit by a bullet.
        ///     Precondition: background != null
        ///     Post-condition: player ship should be removed if hit
        /// </summary>
        public IDictionary<ShipBullet, int> PlayerDied(IList<ShipBullet> enemyBullets)
        {
            IDictionary<ShipBullet, int> result = new Dictionary<ShipBullet, int>();
            ShipBullet hitBullet = null;
            foreach (var bullet in enemyBullets)
            {
                this.playerDestroyed(bullet, ref hitBullet);
            }

            if (hitBullet != null)
            {
                result.Add(hitBullet, this.Lives);
            }

            return result;
        }

        private void playerDestroyed(ShipBullet bullet, ref ShipBullet hitBullet)
        {
            if (CollisionDetector.DetectCollision(this.PlayerShip, bullet))
            {
                this.gameBackground.Children.Remove(this.PlayerShip.Sprite);
                this.gameBackground.Children.Remove(bullet.Sprite);
                hitBullet = bullet;
                SoundPlayer.PlaySound("destroyed.wav");

                if (this.Lives > OneLifeLeft)
                {
                    this.Lives--;
                    this.CreateAndPlacePlayerShip();
                }
                else
                {
                    this.Lives = OutOfLives;
                }
            }
        }

        /// <summary>
        ///     Moves the player ship to the left.
        ///     Precondition: none
        ///     Post-condition: The player ship has moved left.
        /// </summary>
        public void MovePlayerShipLeft()
        {
            if (this.PlayerShip.X - this.PlayerShip.SpeedX > 0)
            {
                this.PlayerShip.MoveLeft();
            }
        }

        /// <summary>
        ///     Moves the player ship to the right.
        ///     Precondition: none
        ///     Post-condition: The player ship has moved right.
        /// </summary>
        public void MovePlayerShipRight()
        {
            if (this.PlayerShip.X + this.PlayerShip.Width + this.PlayerShip.SpeedX < this.gameBackground.Width)
            {
                this.PlayerShip.MoveRight();
            }
        }

        /// <summary>
        ///     Moves the player ship down.
        /// </summary>
        public void MovePlayerShipDown()
        {
            if (this.PlayerShip.Y + this.PlayerShip.Height + this.PlayerShip.SpeedY < this.gameBackground.Height - 30)
            {
                this.PlayerShip.MoveDown();
            }
        }

        /// <summary>
        ///     Moves the player ship up.
        /// </summary>
        public void MovePlayerShipUp()
        {
            if (this.PlayerShip.Y + this.PlayerShip.SpeedY > 300)
            {
                this.PlayerShip.MoveUp();
            }
        }

        /// <summary>
        ///     Players the collided with shield.
        /// </summary>
        public void PlayerCollidedWithShield()
        {
            this.gameBackground.Children.Remove(this.PlayerShip.Sprite);
            SoundPlayer.PlaySound("destroyed.wav");

            if (this.Lives > OneLifeLeft)
            {
                this.Lives--;
                this.CreateAndPlacePlayerShip();
            }
            else
            {
                this.Lives = OutOfLives;
            }
        }

        #endregion

        #region DataMembers

        private const double PlayerShipBottomOffset = 30;
        private const int OutOfLives = 0;
        private const int OneLifeLeft = 1;

        private readonly Canvas gameBackground;

        #endregion
    }
}