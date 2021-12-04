using Windows.UI;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SpaceInvaders.View.Sprites
{
    /// <summary>
    ///     Draws a Bonus Enemy Ship.
    /// </summary>
    /// <seealso cref="SpaceInvaders.View.Sprites.BaseSprite" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class BonusEnemySprite
    {
        #region Data members

        #region DataMembers

        private bool hasMoved;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BonusEnemySprite" /> class.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        public BonusEnemySprite()
        {
            this.InitializeComponent();
            this.hasMoved = true;
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
            if (this.hasMoved)
            {
                this.turret3.Fill = new SolidColorBrush(Colors.Yellow);
                this.hasMoved = false;
            }
            else
            {
                this.turret3.Fill = new SolidColorBrush(Colors.Red);
                this.hasMoved = true;
            }
        }

        #endregion
    }
}