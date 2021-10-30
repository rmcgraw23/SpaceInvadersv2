using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SpaceInvaders.View.Sprites
{
    public sealed partial class Level4EnemySprite : BaseSprite
    {
        #region DataMembers

        private bool hasMoved;

        #endregion
        public Level4EnemySprite()
        {
            this.InitializeComponent();
            this.hasMoved = true;
        }

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
    }
}
