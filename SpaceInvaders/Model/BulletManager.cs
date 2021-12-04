using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    /// Manages the bullets.
    /// </summary>
    class BulletManager
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
        /// Post-condition: PlayerBullets.Count() == 0 && EnemyBullets.Count() == 0 && PowerUps.Count() == 0
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

        public IList<ShipBullet> AddEnemyBullet(ShipBullet bullet)
        {
            this.EnemyBullets.Add(bullet);
            return this.EnemyBullets;
        }

        public IList<GameObject> AddPlayerBullet(ShipBullet bullet)
        {
            this.PlayerBullets.Add(bullet);
            
            return this.PlayerBullets;
        }

        public IList<ShipBullet> RemoveEnemyBullet(ShipBullet bullet)
        {
            this.EnemyBullets.Remove(bullet);
            return this.EnemyBullets;
        }

        public IList<GameObject> RemovePlayerBullet(ShipBullet bullet)
        {
            this.PlayerBullets.Remove(bullet);
            return this.PlayerBullets;
        }

        public IList<GameObject> RemovePowerUp(PowerUp powerup)
        {
            this.PowerUps.Remove(powerup);
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

        private IList<ShipBullet> createAndPlaceEnemyBullets(Random random, GameObject ship, double playerX)
        {
            var value = random.Next(0, 10);
            if (value == 0)
            {
                ShipBullet bullet = new ShipBullet();

                if (ship.Sprite is Level4EnemySprite)
                {
                    bullet.EndingX = playerX;
                    bullet.StartingX = ship.X;
                    bullet.IsLevel4Enemy = true;
                }
                else if (ship.Sprite is BonusEnemySprite)
                {
                    bullet.EndingX = playerX;
                    bullet.StartingX = ship.X;
                    bullet.IsBonusEnemy = true;
                }

                this.placeBulletsBellowEnemies(ship, bullet);

               this.gameBackground.Children.Add(bullet.Sprite);
                this.AddEnemyBullet(bullet);
                SoundPlayer.PlaySound("laser.wav");

                /*if (this.EnemyBullets.Count == 0)
                {
                    this.EnemyFired = false;
                }
                else
                {
                    this.EnemyFired = true;
                }*/
            }

            return this.EnemyBullets;
        }

        private void placeBulletsBellowEnemies(GameObject ship, GameObject bullet)
        {
            bullet.X = ship.X + (ship.Width / 2 - 1);
            bullet.Y = ship.Y + 18;
        }

        public IList<GameObject> CreateAndPlacePlayerShipBullet(Canvas gameBackground, PlayerShip playerShip)
        {
            ShipBullet bullet = new ShipBullet();

            if (this.PlayerBullets.Count < MaxLives)
            {
                this.AddPlayerBullet(bullet);
                gameBackground.Children.Add(bullet.Sprite);
                this.placePlayerBullet(bullet, playerShip);
                //this.BulletFired = true;
                SoundPlayer.PlaySound("cannonFire.wav");
                
            }

            return this.PlayerBullets;
        }

        private void placePlayerBullet(ShipBullet bullet, PlayerShip playerShip)
        {
            bullet.X = playerShip.X + (playerShip.Width / 2 - 1);
            bullet.Y = playerShip.Y - 15;
        }

        public void CreateandPlacePowerUp(PlayerShip playerShip)
        {
            PowerUp powerUp = new PowerUp();
            this.gameBackground.Children.Add(new PowerUp().Sprite);
            this.PowerUps.Add(powerUp);
            this.placePowerUp(powerUp, playerShip);

            SoundPlayer.PlaySound("cannonFire.wav");

        }

        private void placePowerUp(PowerUp powerUp, PlayerShip playerShip)
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
            //this.playerShipManager.PlayerBullets = this.playerShipManager.PlayerBullets;
        }

        public void MovePowerUp()
        {
            GameObject PowerUp = null;
            foreach (var powerUp in this.PowerUps)
            {
                if (powerUp.Y + powerUp.SpeedY < 0)
                {
                    this.gameBackground.Children.Remove(powerUp.Sprite);
                    PowerUp = powerUp;
                }

                powerUp.MoveUp();
            }

            this.PowerUps.Remove(PowerUp);
            //this.playerShipManager.PlayerBullets = this.playerShipManager.PlayerBullets;
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
            double bulletXMovement = bullet.StartingX - bullet.EndingX;

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
