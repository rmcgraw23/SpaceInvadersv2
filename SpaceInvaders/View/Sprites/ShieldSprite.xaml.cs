namespace SpaceInvaders.View.Sprites
{
    /// <summary>
    ///     Draws a Shield.
    /// </summary>
    /// <seealso cref="SpaceInvaders.View.Sprites.BaseSprite" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class ShieldSprite
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ShieldSprite" /> class.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        public ShieldSprite()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Changes the color of the ships lights.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        public override void ChangeLightsColors()
        {
        }

        #endregion
    }
}