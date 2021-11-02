using SpaceInvaders.View.Sprites;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model
{
    /// <summary>
    /// Manages the ships for the game.
    /// </summary>
    class ShipsManager
    {
        #region DataMembers
        private const double PlayerShipBottomOffset = 30;

        private int enemyShipsPerRow = 8;

        private readonly double backgroundHeight;
        private readonly double backgroundWidth;

        private Canvas gameBackground;

        private const int MaxLives = 3;
        private const int OutOfLives = 0;
        private const int OneLifeLeft = 1;

        private readonly BulletsManager bulletsManager;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the player ship.
        /// </summary>
        /// <value>
        ///     The player ship.
        /// </value>
        public PlayerShip PlayerShip { get; set; }

        /// <summary>
        ///     Gets or sets the enemy ships.
        /// </summary>
        /// <value>
        ///     The enemy ships.
        /// </value>
        public IList<EnemyShip> EnemyShips { get; set; }

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

        /// <summary>
        ///     Gets or sets the lives.
        /// </summary>
        /// <value>
        ///     The lives.
        /// </value>
        public int Lives { get; set; }

        #endregion

        #region Constructor

        public ShipsManager(double backgroundHeight, double backgroundWidth)
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
            this.bulletsManager = new BulletsManager(backgroundHeight, backgroundWidth);
        }


        #endregion

        #region Methods

        /// <summary>
        /// Initializes the ships.
        /// Precondition: none
        /// Post-condition: none
        /// </summary>
        /// <exception cref="ArgumentNullException">background equals null</exception>
        public void InitializeShips(Canvas background)
        {
            this.gameBackground = background;
            this.EnemyFired = false;
            this.BulletFired = false;
            this.EnemyBullets = new List<GameObject>();
            this.PlayerBullet = new List<ShipBullet>();
            this.Lives = 3;
        }

        /// <summary>
        /// Creates places the player ship.
        /// Precondition: none
        /// Post-condition: the player ship has been placed on the background.
        /// </summary>
        public void CreateAndPlacePlayerShip()
        {
            this.PlayerShip = new PlayerShip();
            this.gameBackground.Children.Add(this.PlayerShip.Sprite);

            this.placePlayerShipNearBottomOfBackgroundCentered();
        }

        private void placePlayerShipNearBottomOfBackgroundCentered()
        {
            this.PlayerShip.X = this.backgroundWidth / 2 - this.PlayerShip.Width / 2.0;
            this.PlayerShip.Y = this.backgroundHeight - this.PlayerShip.Height - PlayerShipBottomOffset;
        }

        /// <summary>
        /// Creates and places the enemy ships.
        /// Precondition: none
        /// Post-condition: enemy ships have been placed on the background.
        /// </summary>
        public void CreateAndPlaceEnemyShips()
        {
            var count = 4;
            this.getEnemyShips();
            for (var index = 0; index < this.EnemyShips.Count; index++)
            {
                this.gameBackground.Children.Add(this.EnemyShips[index].Sprite);
                if (index < 2)
                {
                    this.placeEnemyShips(count - 1, this.EnemyShips[index]);
                    //this.enemyShipsPerRow = 2;
                }

                else if (index < 6)
                {

                    //this.enemyShipsPerRow = 4;
                    this.placeEnemyShips(count - 4, this.EnemyShips[index]);


                }

                else if (index < 12)
                {
                    //this.enemyShipsPerRow = 6;
                    this.placeEnemyShips(count - 9, this.EnemyShips[index]);

                }
                else
                {
                    //this.enemyShipsPerRow = 8;
                    this.placeEnemyShips(count - 16, this.EnemyShips[index]);

                }

                count++;

            }
        }

        private void getEnemyShips()
        {
            this.EnemyShips = new List<EnemyShip>();

            for (var ship = 0; ship < 20; ship++)
            {

                if (ship < (int)EnemyAmounts.Level1EnemyCount)
                {
                    this.EnemyShips.Add(new EnemyShip(new Level1EnemySprite()));
                }

                else if (ship < (int)EnemyAmounts.Level2EnemyCount)
                {
                    this.EnemyShips.Add(new EnemyShip(new Level2EnemySprite()));
                }
                else if (ship < (int)EnemyAmounts.Level3EnemyCount)
                {
                    this.EnemyShips.Add(new EnemyShip(new Level3EnemySprite()));
                }
                else
                {
                    this.EnemyShips.Add(new EnemyShip(new Level4EnemySprite()));
                }

            }
        }

        /// <summary>
        /// Creates and places a bullet as long as there isn't another on the screen.
        /// Precondition: none
        /// post-condition: bullet has been placed on the canvas or bullet already exists.
        /// </summary>
        public void CreateAndPlacePlayerShipBullet()
        {
            ShipBullet bullet = new ShipBullet();

            if (this.PlayerBullet.Count < MaxLives)
            {
                this.PlayerBullet.Add(bullet);
                this.gameBackground.Children.Add(bullet.Sprite);
                this.placePlayerBullet(bullet);
                this.BulletFired = true;
            }

            /*this.bulletsManager.CreateAndPlacePlayerShipBullet(this.gameBackground, this.PlayerShip);
            this.PlayerBullet = this.bulletsManager.PlayerBullet;*/
        }

        private void placePlayerBullet(ShipBullet bullet)
        {
            bullet.X = this.PlayerShip.X + 1;
            bullet.Y = this.PlayerShip.Y - 15;
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

        /// <summary>
        /// Adds the enemy bullets fired to the canvas and list of bullets
        /// Precondition: none
        /// Post-condition: the bullets fired should be added to the background
        /// </summary>
        public void GetEnemyBulletsFired()
        {
            var random = new Random();
            foreach (var ship in this.getLevel3Enemies())
            {
                this.createAndPlaceEnemyBullets(random, ship);
            }
        }

        private void createAndPlaceEnemyBullets(Random random, GameObject ship)
        {
            var value = random.Next(0, 10);
            if (value == 0)
            {
                GameObject bullet = new ShipBullet();
                this.placeBulletsBellowEnemies(ship, bullet);

                this.gameBackground.Children.Add(bullet.Sprite);
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

        private IList<GameObject> getLevel3Enemies()
        {
            IList<GameObject> firingEnemies = new List<GameObject>();
            foreach (var ship in this.EnemyShips)
            {
                if (ship.Sprite is Level3EnemySprite || ship.Sprite is Level4EnemySprite)
                {
                    firingEnemies.Add(ship);
                }
            }

            return firingEnemies;
        }

        private void placeBulletsBellowEnemies(GameObject ship, GameObject bullet)
        {
            bullet.X = ship.X;
            bullet.Y = ship.Y + 18;
        }

        /// <summary>
        /// Checks if an enemy ship is hit by a bullet
        /// Precondition: none
        /// Post-condition: the enemy and bullet should be removed if the ship is hit
        /// </summary>
        public EnemyShip EnemyDestroyed()
        {
            EnemyShip destroyedShip = null;
            ShipBullet hitBullet = null;
            foreach (var ship in this.EnemyShips)
            {
                destroyedShip = this.shipDestroyed(ship, destroyedShip, ref hitBullet);
            }

            this.EnemyShips.Remove(destroyedShip);
            this.PlayerBullet.Remove(hitBullet);
            return destroyedShip;
        }

        private EnemyShip shipDestroyed(EnemyShip ship, EnemyShip destroyedShip, ref ShipBullet hitBullet)
        {
            foreach (var bullet in this.PlayerBullet)
            {
                if (this.WithinShipHeight(ship, bullet) && this.WithinShipWidth(ship, bullet))
                {
                    this.gameBackground.Children.Remove(ship.Sprite);
                    this.gameBackground.Children.Remove(bullet.Sprite);
                    destroyedShip = ship;
                    hitBullet = bullet;
                    this.BulletFired = false;
                }
            }

            return destroyedShip;
        }

        /// <summary>
        /// Checks if the player was hit by a bullet.
        /// Precondition: background != null
        /// Post-condition: player ship should be removed if hit
        /// </summary>
        public int PlayerDied()
        {
            GameObject hitBullet = null;
            foreach (var bullet in this.EnemyBullets)
            {
                this.playerDestroyed(bullet, ref hitBullet);
            }

            this.EnemyBullets.Remove(hitBullet);
            return this.Lives;
        }

        private void playerDestroyed(GameObject bullet, ref GameObject hitBullet)
        {
            if (this.WithinShipHeight(this.PlayerShip, bullet) && this.WithinShipWidth(this.PlayerShip, bullet))
            {
                this.gameBackground.Children.Remove(this.PlayerShip.Sprite);
                this.gameBackground.Children.Remove(bullet.Sprite);
                hitBullet = bullet;

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

        private bool WithinShipHeight(GameObject ship, GameObject bullet)
        {
            return bullet.withinObjectHeight(ship, bullet);
            //bullet.Y <= ship.Y + ship.Height && bullet.Y >= ship.Y;
        }

        private bool WithinShipWidth(GameObject ship, GameObject bullet)
        {
            return bullet.withinObjectWidth(ship, bullet);
            //bullet.X <= ship.X + ship.Width / 2 && bullet.X >= ship.X - ship.Width / 2;
        }
        #endregion
    }
}
