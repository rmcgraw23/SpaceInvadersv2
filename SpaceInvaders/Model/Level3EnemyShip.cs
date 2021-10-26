using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the level 3 enemy ship.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    internal class Level3EnemyShip : GameObject
    {
        private const int SpeedXDirection = 20;
        private const int SpeedYDirection = 0;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Level3EnemyShip" /> class.
        /// </summary>
        public Level3EnemyShip()
        {
            Sprite = new Level3EnemySprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}