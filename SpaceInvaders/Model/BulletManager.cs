using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.Model.Player;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    /// Manages the bullets.
    /// </summary>
    internal class BulletManager
    {
        #region DataMembers

        private readonly Canvas gameBackground;

        private const int MaxLives = 3;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the player bullets.
        /// </summary>
        /// <value>
        /// The player bullets.
        /// </value>
        public IList<GameObject> PlayerBullets { get; set; }

        /// <summary>
        /// Gets or sets the enemy bullets.
        /// </summary>
        /// <value>
        /// The enemy bullets.
        /// </value>
        public IList<ShipBullet> EnemyBullets { get; set; }

        /// <summary>
        /// Gets or sets the power ups.
        /// </summary>
        /// <value>
        /// The power ups.
        /// </value>
        public IList<GameObject> PowerUps { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletManager"/> class.
        /// Precondition: none
        /// Post-condition: PlayerBullets.Count() == 0 and EnemyBullets.Count() == 0 and PowerUps.Count() == 0
        /// </summary>
        /// <param name="gameBackground">The game background.</param>
        public BulletManager(Canvas gameBackground)
        {
            this.PlayerBullets = new List<GameObject>();
            this.EnemyBullets = new List<ShipBullet>();
            this.PowerUps = new List<GameObject>();

            this.gameBackground = gameBackground;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the enemy bullet.
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        /// <returns></returns>
        public IList<ShipBullet> AddEnemyBullet(ShipBullet bullet)
        {
            this.EnemyBullets.Add(bullet);
            return this.EnemyBullets;
        }

        /// <summary>
        /// Adds the player bullet.
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        /// <returns></returns>
        public IList<GameObject> AddPlayerBullet(ShipBullet bullet)
        {
            this.PlayerBullets.Add(bullet);
            
            return this.PlayerBullets;
        }

        /// <summary>
        /// Removes the enemy bullet.
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        /// <returns></returns>
        public IList<ShipBullet> RemoveEnemyBullet(ShipBullet bullet)
        {
            this.EnemyBullets.Remove(bullet);
            return this.EnemyBullets;
        }

        /// <summary>
        /// Removes the player bullet.
        /// </summary>
        /// <param name="bullet">The bullet.</param>
        /// <returns></returns>
        public IList<GameObject> RemovePlayerBullet(ShipBullet bullet)
        {
            this.PlayerBullets.Remove(bullet);
            return this.PlayerBullets;
        }

        /// <summary>
        /// Removes the power up.
        /// </summary>
        /// <param name="powerUp">The power up.</param>
        /// <returns></returns>
        public IList<GameObject> RemovePowerUp(PowerUp powerUp)
        {
            this.PowerUps.Remove(powerUp);
            return this.PowerUps;
        }

        /// <summary>
        /// Adds the enemy bullets fired to the canvas and list of bullets
        /// Precondition: none
        /// Post-condition: the bullets fired should be added to the background
        /// </summary>
        public void GetEnemyBulletsFired(IList<GameObject> firingEnemies, double playerX)
        {
            var random = new Random();
            foreach (var ship in firingEnemies)
            {
                this.createAndPlaceEnemyBullets(random, ship, playerX);
            }
        }

        private void createAndPlaceEnemyBullets(Random random, GameObject ship, double playerX)
        {
            var value = random.Next(0, 10);
            if (value == 0)
            {
                var bullet = new ShipBullet();

                switch (ship.Sprite)
                {
                    case Level4EnemySprite _:
                        bullet.EndingX = playerX;
                        bullet.StartingX = ship.X;
                        bullet.IsLevel4Enemy = true;
                        break;
                    case BonusEnemySprite _:
                        bullet.EndingX = playerX;
                        bullet.StartingX = ship.X;
                        bullet.IsBonusEnemy = true;
                        break;
                }

                this.placeBulletsBellowEnemies(ship, bullet);

                this.gameBackground.Children.Add(bullet.Sprite);
                this.AddEnemyBullet(bullet);
                SoundPlayer.PlaySound("laser.wav");
            }
        }

        private void placeBulletsBellowEnemies(GameObject ship, GameObject bullet)
        {
            bullet.X = ship.X + (ship.Width / 2 - 1);
            bullet.Y = ship.Y + 18;
        }

        /// <summary>
        /// Creates the and place player ship bullet.
        /// </summary>
        /// <param name="playerShip">The player ship.</param>
        /// <returns></returns>
        public IList<GameObject> CreateAndPlacePlayerShipBullet(PlayerShip playerShip)
        {
            var bullet = new ShipBullet();

            if (this.PlayerBullets.Count < MaxLives)
            {
                this.AddPlayerBullet(bullet);
                this.gameBackground.Children.Add(bullet.Sprite);
                this.placePlayerBullet(bullet, playerShip);
                SoundPlayer.PlaySound("cannonFire.wav");
                
            }

            return this.PlayerBullets;
        }

        private void placePlayerBullet(GameObject bullet, GameObject playerShip)
        {
            bullet.X = playerShip.X + (playerShip.Width / 2 - 1);
            bullet.Y = playerShip.Y - 15;
        }

        /// <summary>
        /// Creates the and place power up.
        /// </summary>
        /// <param name="playerShip">The player ship.</param>
        public void CreateAndPlacePowerUp(PlayerShip playerShip)
        {
            var powerUp = new PowerUp();
            this.gameBackground.Children.Add(new PowerUp().Sprite);
            this.PowerUps.Add(powerUp);
            this.placePowerUp(powerUp, playerShip);

            SoundPlayer.PlaySound("cannonFire.wav");

        }

        private void placePowerUp(GameObject powerUp, GameObject playerShip)
        {
            powerUp.X = playerShip.X + playerShip.Width;
            powerUp.Y = playerShip.Y - 15;
        }

        /// <summary>
        /// Moves the player bullet up or remove if off screen
        /// Precondition: none
        /// Post-condition: The player bullet has moved up or has been removed
        /// </summary>
        public void MoveBulletUp()
        {
            GameObject shipBullet = null;
            foreach (var bullet in this.PlayerBullets)
            {
                if (bullet.Y + bullet.SpeedY < 0)
                {
                    this.gameBackground.Children.Remove(bullet.Sprite);
                    shipBullet = bullet;
                }

                bullet.MoveUp();
            }

            this.RemovePlayerBullet((ShipBullet)shipBullet);
        }

        /// <summary>
        /// Moves the power up.
        /// </summary>
        public void MovePowerUp()
        {
            GameObject powerUp = null;
            foreach (var curPowerUp in this.PowerUps)
            {
                if (curPowerUp.Y + curPowerUp.SpeedY < 0)
                {
                    this.gameBackground.Children.Remove(curPowerUp.Sprite);
                    powerUp = curPowerUp;
                }

                curPowerUp.MoveUp();
            }

            this.PowerUps.Remove(powerUp);
        }

        /// <summary>
        /// Moves each player bullet in the list down
        /// Precondition: none
        /// Post-condition: each bullet should be moved down
        /// </summary>
        public void MoveBulletDown()
        {
            IList<ShipBullet> bulletsToRemove = new List<ShipBullet>();
            foreach (var currentBullet in this.EnemyBullets)
            {
                if (currentBullet.Y + currentBullet.SpeedY > this.gameBackground.Height)
                {
                    bulletsToRemove.Add(currentBullet);
                    this.gameBackground.Children.Remove(currentBullet.Sprite);
                }

                else if (currentBullet.IsLevel4Enemy || currentBullet.IsBonusEnemy)
                {
                    this.moveBulletTowardsPlayer(currentBullet);
                }

                else if (this.EnemyBullets.Count > 0)
                {
                    currentBullet.MoveDown();
                }
            }

            foreach (var currentBullet in bulletsToRemove)
            {
                this.EnemyBullets.Remove(currentBullet);
            }

        }

        private void moveBulletTowardsPlayer(ShipBullet bullet)
        {
            var bulletXMovement = bullet.StartingX - bullet.EndingX;

            if (bulletXMovement < -10 || bulletXMovement > 10)
            {
                if (bulletXMovement > 0)
                {
                    bullet.MoveLeft();
                    bullet.StartingX -= 10;
                    
                }
                else if (bulletXMovement < 0)
                {
                    bullet.MoveRight();
                }
            }
            bullet.MoveDown();
        }

        #endregion
    }
}
