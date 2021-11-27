using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.PointOfService.Provider;
using Windows.UI.Xaml;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model
{
    /// <summary>
    /// Manages the bullets.
    /// </summary>
    class BulletManager
    {
        #region DataMembers

        public IList<ShipBullet> playerBullets;
        public IList<ShipBullet> enemyBullets;

        private Canvas gameBackground;

        private const int MaxLives = 3;

        #endregion

        #region Constructor

        public BulletManager(Canvas gameBackground)
        {
            this.playerBullets = new List<ShipBullet>();
            this.enemyBullets = new List<ShipBullet>();

            this.gameBackground = gameBackground;
        }

        #endregion

        #region Methods

        public IList<ShipBullet> AddEnemyBullet(ShipBullet bullet)
        {
            this.enemyBullets.Add(bullet);
            return this.enemyBullets;
        }

        public IList<ShipBullet> AddPlayerBullet(ShipBullet bullet)
        {
            this.playerBullets.Add(bullet);
            return this.playerBullets;
        }

        public IList<ShipBullet> RemoveEnemyBullet(ShipBullet bullet)
        {
            this.enemyBullets.Remove(bullet);
            return this.enemyBullets;
        }

        public IList<ShipBullet> RemovePlayerBullet(ShipBullet bullet)
        {
            this.playerBullets.Remove(bullet);
            return this.playerBullets;
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

            return this.enemyBullets;
        }

        private void placeBulletsBellowEnemies(GameObject ship, GameObject bullet)
        {
            bullet.X = ship.X;
            bullet.Y = ship.Y + 18;
        }

        public IList<ShipBullet> CreateAndPlacePlayerShipBullet(Canvas gameBackground, PlayerShip playerShip)
            {
                ShipBullet bullet = new ShipBullet();

                if (this.playerBullets.Count < MaxLives)
                {
                    this.AddPlayerBullet(bullet);
                    gameBackground.Children.Add(bullet.Sprite);
                    this.placePlayerBullet(bullet, playerShip);
                    //this.BulletFired = true;
                    SoundPlayer.PlaySound("cannonFire.wav");
                    
                }

                return this.playerBullets;
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
            foreach (var bullet in this.playerBullets)
            {
                if (bullet.Y + bullet.SpeedY < 0)
                {
                    this.gameBackground.Children.Remove(bullet.Sprite);
                    shipBullet = bullet;
                }

                bullet.MoveUp();
            }

            this.RemoveEnemyBullet(shipBullet);
            //this.playerShipManager.PlayerBullets = this.playerShipManager.PlayerBullets;
        }

        /// <summary>
        /// Moves each player bullet in the list down
        /// Precondition: none
        /// Post-condition: each bullet should be moved down
        /// </summary>
        public void MoveBulletDown()
        {
            for (int index = 0; index < this.enemyBullets.Count; index++)
            {
                if (this.enemyBullets[index].Y + this.enemyBullets[index].SpeedY > this.gameBackground.Height)
                {
                    this.enemyBullets.Remove(this.enemyBullets[index]);
                }

                if (this.enemyBullets.Count > 0)
                {
                    this.enemyBullets[index].MoveDown();
                }
            }
        }

        #endregion
    }
}
