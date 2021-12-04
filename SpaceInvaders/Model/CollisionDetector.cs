namespace SpaceInvaders.Model
{
    /// <summary>
    /// The Collision Detector class.
    /// </summary>
    public static class CollisionDetector
    {

        #region Methods

        /// <summary>
        /// Detects if a collision occurred between 2 game objects.
        /// Precondition: none
        /// Post-Condition: none
        /// </summary>
        /// <param name="object1">The object1.</param>
        /// <param name="object2">The object2.</param>
        /// <returns></returns>
        public static bool DetectCollision(GameObject object1, GameObject object2)
        {
            return object1 != null && object2 != null && object1.X + object1.Width >= object2.X && object1.X <= object2.X + object2.Width && object1.Y + object1.Height >=
                   object2.Y && object1.Y <= object2.Y + object2.Height;
        }

        #endregion

    }
}
