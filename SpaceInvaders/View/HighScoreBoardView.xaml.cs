using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace SpaceInvaders.View
{
    public sealed partial class HighScoreBoardView
    {
        #region DataMemebers

        private bool show;

        #endregion

        #region Properties

        // <summary>
        ///     The application height
        /// </summary>
        public const double ApplicationHeight = 580;

        /// <summary>
        ///     The application width
        /// </summary>
        public const double ApplicationWidth = 840;

        #endregion

        #region Constructors

        public HighScoreBoardView()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size { Width = ApplicationWidth, Height = ApplicationHeight };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));
            //this.AskForNameAsync();
            //this.name = "";

        }

        #endregion

        #region Methods

        private void sortByNameFirst(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        private void sortByLevelFirst(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void SubmitButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        #endregion

    }
}
