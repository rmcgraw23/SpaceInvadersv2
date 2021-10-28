// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using SpaceInvaders.Model;
using Color = System.Drawing.Color;

namespace SpaceInvaders.View.Sprites
{
    /// <summary>
    ///     Draws a level 2 enemy ship.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    public sealed partial class Level2EnemySprite
    {
        #region DataMembers

        private GameManager gameManager;

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
            //this.gameManager = new GameManager(640, 500);

            //this.gameManager.AnimationUpdated += this.AnimationOnAnimationUpdated;
        }

        #endregion

        #region Methods

        public override void ChangeLightsColors()
        {
            this.LeftLight.Fill = this.LeftLight.Fill.GetValue(SolidColorBrush.ColorProperty) == new SolidColorBrush(color:Colors.White)
                ? new SolidColorBrush(color: Colors.Green)
                : new SolidColorBrush(color: Colors.White);

            this.RightLight.Fill = this.RightLight.Fill == new SolidColorBrush(color: Colors.White)
                ? new SolidColorBrush(color: Colors.Green)
                : new SolidColorBrush(color: Colors.White);
        }

        #endregion
    }
}