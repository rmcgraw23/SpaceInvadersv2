// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

using Windows.UI;
using Windows.UI.Xaml.Media;

namespace SpaceInvaders.View.Sprites
{
    /// <summary>
    ///     Draws a level 3 enemy ship.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    public sealed partial class Level3EnemySprite
    {
        #region DataMembers

        private bool hasMoved;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Level3EnemySprite" /> class.
        ///     Precondition: none
        ///     Post-condition: Sprite created.
        /// </summary>
        public Level3EnemySprite()
        {
            this.InitializeComponent();
            this.hasMoved = true;
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
            if (this.hasMoved)
            {
                this.window.Fill = new SolidColorBrush(color: Colors.Black);
                this.hasMoved = false;
            }
            else
            {
                this.window.Fill = new SolidColorBrush(color: Colors.YellowGreen);
                this.hasMoved = true;
            }
        }

        #endregion
    }
}