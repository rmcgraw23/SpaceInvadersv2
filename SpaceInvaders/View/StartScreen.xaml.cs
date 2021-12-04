using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.View
{
    /// <summary>
    ///     The page for the start screen.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.Page" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class StartScreen
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StartScreen"/> class.
        /// Precondition: none
        /// Post-condition: Creates a new start screen.
        /// </summary>
        public StartScreen()
        {
            this.InitializeComponent();

        }

        #endregion

        #region Methods

        private void startGame_Button_Click(object sender, RoutedEventArgs e)
        {
            this.waitOnStart();
        }

        private async void waitOnStart()
        {
            var currentAv = ApplicationView.GetForCurrentView();
            var newAv = CoreApplication.CreateNewView();

            async void AgileCallback()
            {
                var newWindow = Window.Current;
                var newAppView = ApplicationView.GetForCurrentView();

                var frame = new Frame();
                frame.Navigate(typeof(MainPage), null);
                newWindow.Content = frame;
                newWindow.Activate();

                await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newAppView.Id, ViewSizePreference.UseMinimum, currentAv.Id, ViewSizePreference.UseMinimum);
            }

            await newAv.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, AgileCallback);

        }

        private void viewHighScoreBoard_Button_Click(object sender, RoutedEventArgs e)
        {
            this.waitOnScoreBoard();
        }

        private async void waitOnScoreBoard()
        {
            var currentAv = ApplicationView.GetForCurrentView();
            var newAv = CoreApplication.CreateNewView();

            async void AgileCallback()
            {
                var newWindow = Window.Current;
                var newAppView = ApplicationView.GetForCurrentView();

                var frame = new Frame();
                frame.Navigate(typeof(HighScoreBoardView), null);
                newWindow.Content = frame;
                newWindow.Activate();

                await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newAppView.Id, ViewSizePreference.UseMinimum, currentAv.Id, ViewSizePreference.UseMinimum);
            }

            await newAv.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, AgileCallback);

        }

        private void resetHighScoreBoard_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
