using System;
using System.Collections;
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
        //private BulletManager manager;

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
        //public IList<ShipBullet> PlayerBullets { get; set; }

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
            //this.PlayerBullets = new List<ShipBullet>();
            this.Lives = 3;
            //this.manager = new BulletManager(this.gameBackground);
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
        /// Checks if the player was hit by a bullet.
        /// Precondition: background != null
        /// Post-condition: player ship should be removed if hit
        /// </summary>
        public IDictionary<ShipBullet, int> PlayerDied(IList<ShipBullet> enemyBullets)
        {
            IDictionary<ShipBullet, int> result = new Dictionary<ShipBullet, int>();
            ShipBullet hitBullet = null;
            foreach (var bullet in enemyBullets)
            {
                this.playerDestroyed(bullet, ref hitBullet);
            }

            //TODO: Fix line below
            //this.EnemyBullets.Remove(hitBullet);
            //enemyBullets = this.manager.RemoveEnemyBullet(hitBullet);
            if (hitBullet != null)
            {
                result.Add(hitBullet, Lives);
            }

            return result;
        }

        private void playerDestroyed(ShipBullet bullet, ref ShipBullet hitBullet)
        {
            if (CollisionDetector.detectCollision(this.PlayerShip, bullet))
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

        #endregion

    }
}

