using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

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
        public IList<ShipBullet> PlayerBullets { get; set; }

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
        public IList<PowerUp> PowerUps { get; set; }

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
            this.PlayerBullets = new List<ShipBullet>();
            this.EnemyBullets = new List<ShipBullet>();
            this.PowerUps = new List<PowerUp>();

            this.gameBackground = gameBackground;
        }

        #endregion

        #region Methods

        public IList<ShipBullet> AddEnemyBullet(ShipBullet bullet)
        {
            this.EnemyBullets.Add(bullet);
            return this.EnemyBullets;
        }

        public IList<ShipBullet> AddPlayerBullet(ShipBullet bullet)
        {
            this.PlayerBullets.Add(bullet);
            
            this.placePowerUp();
            return this.PlayerBullets;
        }

        public IList<ShipBullet> RemoveEnemyBullet(ShipBullet bullet)
        {
            this.EnemyBullets.Remove(bullet);
            return this.EnemyBullets;
        }

        public IList<ShipBullet> RemovePlayerBullet(ShipBullet bullet)
        {
            this.PlayerBullets.Remove(bullet);
            return this.PlayerBullets;
        }
        /// <summary>
        /// Adds the enemy bullets fired to the canvas and list of bullets
        /// Precondition: none
        /// Post-condition: the bullets fired should be added to the background
        /// </summary>
        public void GetEnemyBulletsFired(IList<GameObject> firingEnemies)
        {
            var random = new Random();
            foreach (var ship in firingEnemies)
            {
                this.createAndPlaceEnemyBullets(random, ship);
            }
        }

        private IList<ShipBullet> createAndPlaceEnemyBullets(Random random, GameObject ship)
        {
            var value = random.Next(0, 10);
            if (value == 0)
            {
                ShipBullet bullet = new ShipBullet();
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
            bullet.X = ship.X;
            bullet.Y = ship.Y + 18;
        }

        public IList<ShipBullet> CreateAndPlacePlayerShipBullet(Canvas gameBackground, PlayerShip playerShip)
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
            bullet.X = playerShip.X + 1;
            bullet.Y = playerShip.Y - 15;
        }

        /// <summary>
        /// Moves the player bullet up or remove if off screen
        /// Precondition: none
        /// Post-condition: The player bullet has moved up or has been removed
        /// </summary>
        public void MoveBulletUp()
        {
            ShipBullet shipBullet = null;
            foreach (var bullet in this.PlayerBullets)
            {
                if (bullet.Y + bullet.SpeedY < 0)
                {
                    this.gameBackground.Children.Remove(bullet.Sprite);
                    shipBullet = bullet;
                }

                bullet.MoveUp();
            }

            this.RemovePlayerBullet(shipBullet);
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

                if (this.EnemyBullets.Count > 0)
                {
                    currentBullet.MoveDown();
                }
            }

            foreach (var currentBullet in bulletsToRemove)
            {
                this.EnemyBullets.Remove(currentBullet);
            }

        }

        private void placePowerUp()
        {
            this.gameBackground.Children.Add(new PowerUp().Sprite);
            this.PowerUps.Add(new PowerUp());
        }



        #endregion
    }
}
