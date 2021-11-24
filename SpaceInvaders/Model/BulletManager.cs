using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.PointOfService.Provider;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model
{
    /// <summary>
    /// Manages the bullets.
    /// </summary>
    class BulletManager
    {
        #region DataMembers

        private IList<ShipBullet> playerBullets;
        private IList<ShipBullet> enemyBullets;

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

        public IList<ShipBullet> createAndPlaceEnemyBullets(Random random, GameObject ship)
        {
            var value = random.Next(0, 10);
            if (value == 0)
            {
                ShipBullet bullet = new ShipBullet();
                this.placeBulletsBellowEnemies(ship, bullet);

                this.gameBackground.Children.Add(bullet.Sprite);
                this.AddEnemyBullet(bullet);

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
                }

                return this.playerBullets;
            }

        private void placePlayerBullet(ShipBullet bullet, PlayerShip playerShip)
        {
            bullet.X = playerShip.X + 1;
            bullet.Y = playerShip.Y - 15;
        }

        #endregion
    }
}
