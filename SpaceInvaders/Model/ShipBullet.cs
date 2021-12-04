using SpaceInvaders.Model.Enemies;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the ship bullet.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    internal class ShipBullet : GameObject
    {
        private const int SpeedXDirection = 10;
        private const int SpeedYDirection = 10;

        /// <summary>
        /// Gets or sets the starting x.
        /// </summary>
        /// <value>
        /// The starting x.
        /// </value>
        public double StartingX { get; set; }

        /// <summary>
        /// Gets or sets the ending x.
        /// </summary>
        /// <value>
        /// The ending x.
        /// </value>
        public double EndingX { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is level4 enemy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is level4 enemy; otherwise, <c>false</c>.
        /// </value>
        public bool IsLevel4Enemy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is bonus enemy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is bonus enemy; otherwise, <c>false</c>.
        /// </value>
        public bool IsBonusEnemy { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnemyShip" /> class.
        /// </summary>
        public ShipBullet()
        {
            Sprite = new ShipBulletSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
            this.IsLevel4Enemy = false;
            this.IsBonusEnemy = false;
        }
    }
}