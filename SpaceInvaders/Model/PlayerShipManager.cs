using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model
{
    class PlayerShipManager
    {

        #region DataMembers

        private const double PlayerShipBottomOffset = 30;
        private const int MaxLives = 3;
        private const int OutOfLives = 0;
        private const int OneLifeLeft = 1;

        private Canvas gameBackground;

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
        ///     Gets or sets the player bullet.
        /// </summary>
        /// <value>
        ///     The player bullet.
        /// </value>
        public IList<ShipBullet> PlayerBullets { get; set; }

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

        public PlayerShipManager(Canvas background)
        {
            this.gameBackground = background;
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
            this.BulletFired = false;
            this.PlayerBullets = new List<ShipBullet>();
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
            this.PlayerShip.X = this.gameBackground.Width / 2 - this.PlayerShip.Width / 2.0;
            this.PlayerShip.Y = this.gameBackground.Height - this.PlayerShip.Height - PlayerShipBottomOffset;
        }

        /// <summary>
        /// Creates and places a bullet as long as there isn't another on the screen.
        /// Precondition: none
        /// post-condition: bullet has been placed on the canvas or bullet already exists.
        /// </summary>
        public void CreateAndPlacePlayerShipBullet()
        {
            ShipBullet bullet = new ShipBullet();

            if (this.PlayerBullets.Count < MaxLives)
            {
                this.PlayerBullets.Add(bullet);
                this.gameBackground.Children.Add(bullet.Sprite);
                this.placePlayerBullet(bullet);
                this.BulletFired = true;
            }
        }

        private void placePlayerBullet(ShipBullet bullet)
        {
            bullet.X = this.PlayerShip.X + 1;
            bullet.Y = this.PlayerShip.Y - 15;
        }

        /// <summary>
        /// Checks if the player was hit by a bullet.
        /// Precondition: background != null
        /// Post-condition: player ship should be removed if hit
        /// </summary>
        public int PlayerDied(IList<GameObject> enemyBullets)
        {
            GameObject hitBullet = null;
            foreach (var bullet in enemyBullets)
            {
                this.playerDestroyed(bullet, ref hitBullet);
            }

            //TODO: Fix line below
            //this.EnemyBullets.Remove(hitBullet);
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
        }

        private bool WithinShipWidth(GameObject ship, GameObject bullet)
        {
            return bullet.withinObjectWidth(ship, bullet);
        }

        #endregion

    }
}

