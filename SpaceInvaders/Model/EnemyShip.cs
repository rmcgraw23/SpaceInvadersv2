using Windows.Media.Streaming.Adaptive;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the level 1 enemy ship.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    internal class EnemyShip : GameObject
    {
        private const int SpeedXDirection = 20;
        private const int SpeedYDirection = 0;

        private const int EnemyLevel1Score = 10;
        private const int EnemyLevel2Score = 20;
        private const int EnemyLevel3Score = 30;
        private const int EnemyLevel4Score = 40;

        public int Score { get; private set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnemyShip" /> class.
        /// </summary>
        public EnemyShip(BaseSprite enemyShipSprite)
        {
            Sprite = enemyShipSprite;
            SetSpeed(SpeedXDirection, SpeedYDirection);

            if (Sprite is Level1EnemySprite)
            {
                Score = EnemyLevel1Score;
            }
            else if (Sprite is Level2EnemySprite)
            {
                Score = EnemyLevel2Score;
            }
            else if (Sprite is Level3EnemySprite)
            {
                Score = EnemyLevel3Score;
            }
            else 
            {
                Score = EnemyLevel4Score;
            }
        }
    }
}