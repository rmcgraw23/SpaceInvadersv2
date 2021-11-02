using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the enemy ships.
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

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnemyShip" /> class.
        /// </summary>
        public EnemyShip(BaseSprite enemyShipSprite)
        {
            Sprite = enemyShipSprite;
            SetSpeed(SpeedXDirection, SpeedYDirection);

            if (Sprite is Level1EnemySprite)
            {
                this.Score = EnemyLevel1Score;
            }
            else if (Sprite is Level2EnemySprite)
            {
                this.Score = EnemyLevel2Score;
            }
            else if (Sprite is Level3EnemySprite)
            {
                this.Score = EnemyLevel3Score;
            }
            else
            {
                this.Score = EnemyLevel4Score;
            }
        }
    }
}