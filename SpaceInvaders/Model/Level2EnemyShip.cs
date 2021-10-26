using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the level 2 enemy ship.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    internal class Level2EnemyShip : GameObject
    {
        private const int SpeedXDirection = 20;
        private const int SpeedYDirection = 0;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Level2EnemyShip" /> class.
        /// </summary>
        public Level2EnemyShip()
        {
            Sprite = new Level2EnemySprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
        }
    }
}