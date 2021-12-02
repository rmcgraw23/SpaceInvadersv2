using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Store;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace SpaceInvaders.View
{
    public sealed partial class StartScreen
    {

        #region Constructors

        public StartScreen()
        {
            this.InitializeComponent();

        }

        #endregion

        #region Methods

        private void startGame_Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.waitOnStart();
            this.Visibility = Visibility.Collapsed;
        }

        private async void waitOnStart()
        {
            var currentAV = ApplicationView.GetForCurrentView();
            var newAV = CoreApplication.CreateNewView();
            await newAV.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                var newWindow = Window.Current;
                var newAppView = ApplicationView.GetForCurrentView();

                var frame = new Frame();
                frame.Navigate(typeof(MainPage), null);
                newWindow.Content = frame;
                newWindow.Activate();

                await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newAppView.Id, ViewSizePreference.UseMinimum,
                    currentAV.Id, ViewSizePreference.UseMinimum);
            });

        }

        private void viewHighScoreBoard_Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.waitOnScoreBoard();
            this.Visibility = Visibility.Collapsed;
        }

        private async void waitOnScoreBoard()
        {
            var currentAV = ApplicationView.GetForCurrentView();
            var newAV = CoreApplication.CreateNewView();
            await newAV.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                var newWindow = Window.Current;
                var newAppView = ApplicationView.GetForCurrentView();

                var frame = new Frame();
                frame.Navigate(typeof(HighScoreBoardView), null);
                newWindow.Content = frame;
                newWindow.Activate();

                await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newAppView.Id, ViewSizePreference.UseMinimum,
                    currentAV.Id, ViewSizePreference.UseMinimum);
            });

        }

        private void resetHighScoreBoard_Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        #endregion
    }
}
