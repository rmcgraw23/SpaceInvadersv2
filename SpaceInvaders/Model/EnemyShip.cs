using System;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Creates the enemy ships
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
        private const int EnemyBonusScore = 100;

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyShip"/> class.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">level - null</exception>
        public EnemyShip(EnemyShipLevels level)
        {
            switch (level)
            {
                case EnemyShipLevels.LevelOne:
                    Sprite = new Level1EnemySprite();
                    this.Score = EnemyLevel1Score;
                    break;
                case EnemyShipLevels.LevelTwo:
                    Sprite = new Level2EnemySprite();
                    this.Score = EnemyLevel2Score;
                    break;
                case EnemyShipLevels.LevelThree:
                    Sprite = new Level3EnemySprite();
                    this.Score = EnemyLevel3Score;
                    break;
                case EnemyShipLevels.LevelFour:
                    Sprite = new Level4EnemySprite();
                    this.Score = EnemyLevel4Score;
                    break;
                case EnemyShipLevels.Bonus:
                    this.Score = EnemyBonusScore;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }
    }
}