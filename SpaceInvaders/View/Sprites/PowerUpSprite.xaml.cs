using System;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SpaceInvaders.View.Sprites
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SpaceInvaders.View.Sprites.BaseSprite" />
    public sealed partial class PowerUpSprite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PowerUpSprite"/> class.
        /// </summary>
        public PowerUpSprite()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Changes the color of the ships lights.
        /// Precondition: none
        /// Post-condition: none
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void ChangeLightsColors()
        {
            throw new NotImplementedException();
        }
    }
}
