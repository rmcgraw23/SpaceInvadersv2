// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SpaceInvaders.View.Sprites
{
    /// <summary>
    ///     Draws a ship bullet.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    public sealed partial class ShipBulletSprite
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ShipBulletSprite" /> class.
        ///     Precondition: none
        ///     Post-condition: Sprite created.
        /// </summary>
        public ShipBulletSprite()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Changes the color of the ships lights.
        /// Precondition: none
        /// Post-condition: none
        /// </summary>
        public override void ChangeLightsColors()
        {

        }

        #endregion
    }
}