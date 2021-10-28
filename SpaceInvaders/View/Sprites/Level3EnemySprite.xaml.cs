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
        }

        public override void ChangeLightsColors()
        {
            throw new System.NotImplementedException();
        }


        #endregion
    }
}