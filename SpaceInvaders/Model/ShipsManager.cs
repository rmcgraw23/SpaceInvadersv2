using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.View.Sprites;

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
        }


        #endregion

        #region Methods

        /// <summary>
        /// Initializes the ships.
        /// Precondition: none
        /// Post-condition: none
        /// </summary>
        /// <exception cref="ArgumentNullException">background equals null</exception>
        public void InitializeShips()
        {
            this.EnemyFired = false;
            this.BulletFired = false;
            this.EnemyBullets = new List<GameObject>();
            this.PlayerBullet = new List<ShipBullet>();
            this.Lives = 3;
        }

        /// <summary>
        /// Creates places the player ship.
        /// Precondition: background != null
        /// Post-condition: the player ship has been placed on the background.
        /// </summary>
        /// <param name="background">The background.</param>
        public void CreateAndPlacePlayerShip(Canvas background)
        {
            this.PlayerShip = new PlayerShip();
            background.Children.Add(this.PlayerShip.Sprite);

            this.placePlayerShipNearBottomOfBackgroundCentered();
        }

        private void placePlayerShipNearBottomOfBackgroundCentered()
        {
            this.PlayerShip.X = this.backgroundWidth / 2 - this.PlayerShip.Width / 2.0;
            this.PlayerShip.Y = this.backgroundHeight - this.PlayerShip.Height - PlayerShipBottomOffset;
        }

        /// <summary>
        /// Creates and places the enemy ships.
        /// Precondition: background != null
        /// Post-condition: enemy ships have been placed on the background.
        /// </summary>
        /// <param name="background">The background.</param>
        public void CreateAndPlaceEnemyShips(Canvas background)
        {
            var count = 4;
            this.getEnemyShips();
            for (var index = 0; index < this.EnemyShips.Count; index++)
            {
                background.Children.Add(this.EnemyShips[index].Sprite);
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

            //int ships = 0;
            for (var ship = 0; ship < 20; ship++)
            {
                /*this.EnemyShips.Add(new EnemyShip(new Level1EnemySprite()));
                this.EnemyShips.Add(new EnemyShip(new Level2EnemySprite()));
                this.EnemyShips.Add(new EnemyShip(new Level3EnemySprite()));*/

                if (ship < 2)
                {
                    this.EnemyShips.Add(new EnemyShip(new Level1EnemySprite()));
                }

                else if (ship < 6)
                {
                    this.EnemyShips.Add(new EnemyShip(new Level2EnemySprite()));
                }
                else if (ship < 12)
                {
                    this.EnemyShips.Add(new EnemyShip(new Level3EnemySprite()));
                }
                else
                {
                    this.EnemyShips.Add(new EnemyShip(new Level4EnemySprite()));
                }

                //enemyShipsPerRow += 2;
                //ships += 1;
            }
        }

        private void addShipsPerRow(int shipsPerRow, EnemyShip enemyShipSprite)
        {
            for (var ship = 0; ship < shipsPerRow; ship++)
            {
                this.EnemyShips.Add(enemyShipSprite);
            }
        }

        /// <summary>
        /// Creates and places a bullet as long as there isn't another on the screen.
        /// Precondition: background != null
        /// post-condition: bullet has been placed on the canvas or bullet already exists.
        /// </summary>
        /// <param name="background"></param>
        public void CreateAndPlacePlayerShipBullet(Canvas background)
        {
            ShipBullet bullet = new ShipBullet();

            if (this.PlayerBullet.Count <= 3)
            {
                this.PlayerBullet.Add(bullet);
                background.Children.Add(bullet.Sprite);
                this.placePlayerBullet(bullet);
                this.BulletFired = true;
            }
        }

        private void placePlayerBullet(ShipBullet bullet)
        {
            bullet.X = this.PlayerShip.X + 1; //(this.playerShip.Width / 2);
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

        private void placeLevel2EnemyShip(int count, GameObject ship)
        {
            var offset = (this.backgroundWidth - this.enemyShipsPerRow * ship.Width) / (this.enemyShipsPerRow + 1);
            ship.X = offset * (count + 1) + count * ship.Width;
            ship.Y = 103;
        }

        private void placeLevel3EnemyShip(int count, GameObject ship)
        {
            var offset = (this.backgroundWidth - 8 * ship.Width) / (8 + 1);
            ship.X = offset * (count + 1) + count * ship.Width;
            ship.Y = 50;
        }

        /// <summary>
        /// Adds the enemy bullets fired to the canvas and list of bullets
        /// Precondition: background != null
        /// Post-condition: the bullets fired should be added to the background
        /// </summary>
        /// <param name="background">The canvas background</param>
        public void GetEnemyBulletsFired(Canvas background)
        {
            var random = new Random();
            foreach (var ship in this.getLevel3Enemies())
            {
                this.createAndPlaceEnemyBullets(background, random, ship);
            }
        }

        private void createAndPlaceEnemyBullets(Canvas background, Random random, GameObject ship)
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

        private IList<GameObject> getLevel3Enemies()
        {
            IList<GameObject> level3Enemies = new List<GameObject>();
            foreach (var ship in this.EnemyShips)
            {
                if (ship.Sprite is Level3EnemySprite || ship.Sprite is Level4EnemySprite)
                {
                    level3Enemies.Add(ship);
                }
            }

            return level3Enemies;
        }

        private void placeBulletsBellowEnemies(GameObject ship, GameObject bullet)
        {
            bullet.X = ship.X;
            bullet.Y = ship.Y + 18;
        }

        /// <summary>
        /// Checks if an enemy ship is hit by a bullet
        /// Precondition: background != null
        /// Post-condition: the enemy and bullet should be removed if the ship is hit
        /// </summary>
        /// <param name="background">The canvas background</param>
        public EnemyShip EnemyDestroyed(Canvas background)
        {
            EnemyShip destroyedShip = null;
            ShipBullet hitBullet = null;
            foreach (var ship in this.EnemyShips)
            {
                foreach (var bullet in this.PlayerBullet)
                {
                    if (this.WithinShipHeight(ship, bullet) && this.WithinShipWidth(ship, bullet))
                    {
                        background.Children.Remove(ship.Sprite);
                        background.Children.Remove(bullet.Sprite);
                        destroyedShip = ship;
                        hitBullet = bullet;
                        this.BulletFired = false;
                    }
                }
            }

            this.EnemyShips.Remove(destroyedShip);
            this.PlayerBullet.Remove(hitBullet);
            return destroyedShip;
        }

        /// <summary>
        /// Checks if the player was hit by a bullet.
        /// Precondition: background != null
        /// Post-condition: player ship should be removed if hit
        /// </summary>
        /// <param name="background">the background canvas</param>
        public int PlayerDied(Canvas background)
        {
            foreach (var bullet in this.EnemyBullets)
            {
                if (this.WithinShipHeight(this.PlayerShip, bullet) && this.WithinShipWidth(this.PlayerShip, bullet))
                {
                    background.Children.Remove(this.PlayerShip.Sprite);
                    background.Children.Remove(bullet.Sprite);

                    if (this.Lives != 0)
                    {
                        this.Lives--;
                        this.CreateAndPlacePlayerShip(background);
                    }
                }
            }
            return this.Lives;
        }

        private bool WithinShipHeight(GameObject ship, GameObject bullet)
        {
            return bullet.Y <= ship.Y + ship.Height && bullet.Y >= ship.Y;
        }

        private bool WithinShipWidth(GameObject ship, GameObject bullet)
        {
            return bullet.X <= ship.X + ship.Width/2 && bullet.X >= ship.X - ship.Width/2;
        }
        #endregion
    }
}
