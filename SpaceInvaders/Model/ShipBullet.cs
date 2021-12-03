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
        private const int SpeedXDirection = 0;
        private const int SpeedYDirection = 10;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnemyShip" /> class.
        /// </summary>
        public ShipBullet()
        {
            Sprite = new ShipBulletSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}