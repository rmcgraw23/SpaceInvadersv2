using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the shield object.
    /// </summary>
    /// <seealso cref="SpaceInvaders.Model.GameObject" />
    internal class Shield : GameObject
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the hits remaining.
        /// </summary>
        /// <value>
        ///     The hits remaining.
        /// </value>
        public int HitsRemaining { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Shield" /> class.
        ///     Precondition: none
        ///     Post-condition: new shield created
        /// </summary>
        public Shield()
        {
            Sprite = new ShieldSprite();
            SetSpeed(SpeedXDirection, SpeedYDirection);
            this.HitsRemaining = 2;
        }

        #endregion

        #region Data Members

        private const int SpeedXDirection = 0;
        private const int SpeedYDirection = 0;

        #endregion
    }
}