using Windows.UI;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SpaceInvaders.View.Sprites
{
    /// <summary>
    ///     Draws a level 4 enemy ship.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    public sealed partial class Level4EnemySprite
    {
        #region DataMembers

        private bool hasMoved;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Level4EnemySprite" /> class.
        ///     Precondition: none
        ///     Post-condition: Sprite created.
        /// </summary>
        public Level4EnemySprite()
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
                this.turret1.Fill = new SolidColorBrush(color: Colors.Yellow);
                this.turret2.Fill = new SolidColorBrush(color: Colors.Yellow);
                this.hasMoved = false;
            }
            else
            {
                this.turret1.Fill = new SolidColorBrush(color: Colors.Red);
                this.turret2.Fill = new SolidColorBrush(color: Colors.Red);
                this.hasMoved = true;
            }
        }

        #endregion
    }
}
