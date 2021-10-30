// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml.Media;
using SpaceInvaders.Model;

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
        public Level3EnemySprite() : base()
        {
         this.InitializeComponent();
         this.hasMoved = true;
        }

        public override void ChangeLightsColors()
        {
            if (this.hasMoved)
            {
                this.Window.Fill = new SolidColorBrush(color: Colors.Black);
                this.hasMoved = false;
            }
            else
            {
                this.Window.Fill = new SolidColorBrush(color: Colors.YellowGreen);
                this.hasMoved = true;
            }
        }


        #endregion
    }
}