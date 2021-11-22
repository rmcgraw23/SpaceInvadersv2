using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    class EnemyShipManager
    {

        #region DataMembers

        private int enemyShipsPerRow;
        private Canvas gameBackground;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the enemy ships.
        /// </summary>
        /// <value>
        ///     The enemy ships.
        /// </value>
        public IList<EnemyShip> EnemyShips { get; set; }

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

        #region Constructors

        public EnemyShipManager(Canvas background)
        {
            this.gameBackground = background;
            this.EnemyBullets = new List<GameObject>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the ships.
        /// Precondition: none
        /// Post-condition: none
        /// </summary>
        public void InitializeShips()
        {
            this.enemyShipsPerRow = 8;
            this.EnemyFired = false;
            this.BulletFired = false;
            this.EnemyBullets = new List<GameObject>();
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
                if (index < (int)EnemyAmounts.Level1EnemyCount)
                {
                    this.placeEnemyShips(count - 1, this.EnemyShips[index]);
                }

                else if (index < (int)EnemyAmounts.Level2EnemyCount)
                {
                    this.placeEnemyShips(count - 4, this.EnemyShips[index]);
                }

                else if (index < (int)EnemyAmounts.Level3EnemyCount)
                {
                    this.placeEnemyShips(count - 9, this.EnemyShips[index]);
                }
                else
                {
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

        private void placeEnemyShips(int count, GameObject ship)
        {
            var offset = (this.gameBackground.Width - this.enemyShipsPerRow * ship.Width) / (this.enemyShipsPerRow + 1);
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
        public EnemyShip EnemyDestroyed(IList<ShipBullet> playerBullets)
        {
            EnemyShip destroyedShip = null;
            ShipBullet hitBullet = null;
            foreach (var ship in this.EnemyShips)
            {
                destroyedShip = this.shipDestroyed(ship, destroyedShip, ref hitBullet, playerBullets);
            }

            this.EnemyShips.Remove(destroyedShip);
            //TODO: Fix line below
            // this.PlayerBullet.Remove(hitBullet);
            return destroyedShip;
        }

        private EnemyShip shipDestroyed(EnemyShip ship, EnemyShip destroyedShip, ref ShipBullet hitBullet, IList<ShipBullet> playerBullets)
        {
            foreach (var bullet in playerBullets)
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

        private bool WithinShipHeight(GameObject ship, GameObject bullet)
        {
            return bullet.withinObjectHeight(ship, bullet);
        }

        private bool WithinShipWidth(GameObject ship, GameObject bullet)
        {
            return bullet.withinObjectWidth(ship, bullet);
        }

        #endregion

    }
}
