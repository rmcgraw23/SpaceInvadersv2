// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

using Windows.UI;
using Windows.UI.Xaml.Media;

namespace SpaceInvaders.View.Sprites
{
    /// <summary>
    ///     Draws a level 2 enemy ship.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    public partial class Level2EnemySprite
    {
        #region DataMembers

        private bool hasMoved;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Level2EnemySprite" /> class.
        ///     Precondition: none
        ///     Post-condition: Sprite created.
        /// </summary>
        public Level2EnemySprite()
        {
            this.InitializeComponent();
            this.hasMoved = true;
            //this.gameManager = new GameManager(640, 500);

            //this.gameManager.AnimationUpdated += this.AnimationOnAnimationUpdated;
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
                this.leftLight.Fill = new SolidColorBrush(color: Colors.Green);
                this.rightLight.Fill = new SolidColorBrush(color: Colors.Green);
                this.hasMoved = false;
            }
            else
            {
                this.leftLight.Fill = new SolidColorBrush(color: Colors.Red);
                this.rightLight.Fill = new SolidColorBrush(color: Colors.Red);
                this.hasMoved = true;
            }

            //hasMoved = false;
        }

        #endregion
    }
}