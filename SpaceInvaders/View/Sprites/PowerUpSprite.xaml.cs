﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        public override void ChangeLightsColors()
        {
            throw new NotImplementedException();
        }
    }
}
