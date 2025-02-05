﻿using System;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpaceInvaders.View
{
    /// <summary>
    ///     The main page for the game.
    /// </summary>
    public sealed partial class MainPage
    {
        #region Data members

        /// <summary>
        ///     The application height
        /// </summary>
        public const double ApplicationHeight = 580;

        /// <summary>
        ///     The application width
        /// </summary>
        public const double ApplicationWidth = 840;

        private readonly GameManager gameManager;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size {Width = ApplicationWidth, Height = ApplicationHeight};
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));

            Window.Current.CoreWindow.KeyDown += this.coreWindowOnKeyDown;
            Window.Current.CoreWindow.KeyUp += this.coreWindowOnKeyUp;

            this.gameManager = new GameManager(ApplicationHeight, ApplicationWidth);
            this.gameManager.InitializeGame(this.theCanvas);

            this.gameManager.ScoreCountUpdated += this.scoreOnScoreCountUpdated;
            this.gameManager.GameOverUpdated += this.gameOnGameOverUpdated;
            this.gameManager.LivesCountUpdated += this.livesOnLivesCountUpdated;
            this.gameManager.PowerUpCountUpdated += this.powerUpOnPowerUpCountUpdated;
        }

        #endregion

        #region Methods

        private void scoreOnScoreCountUpdated(int score)
        {
            this.scoreTextBlock.Text = "Score: " + score;
        }

        private void livesOnLivesCountUpdated(int lives)
        {
            this.livesTextBlock.Text = "Lives: " + lives;
        }

        private void powerUpOnPowerUpCountUpdated(object self, EventArgs e)
        {
            this.powerUpTextBlock.Text = "PowerUp: " + self;
        }

        private async void gameOnGameOverUpdated(object self, EventArgs e)
        {
            var content = "Score: " + this.gameManager.Score;
            string title;
            if (this.gameManager.Result == "win")
            {
                title = "Congratulation you won!";
            }
            else
            {
                title = "GameOver, You Lose!";
            }

            var playerLostDialog = new ContentDialog {
                Title = title,

                Content = content,
                PrimaryButtonText = "Ok"
            };

            await playerLostDialog.ShowAsync();
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

                await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newAppView.Id, ViewSizePreference.UseMinimum,
                    currentAv.Id, ViewSizePreference.UseMinimum);
            }

            await newAv.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, AgileCallback);
        }

        private void coreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.gameManager.LeftKeyDown = true;
                    this.gameManager.MovePlayerShipLeft();
                    break;
                case VirtualKey.Right:
                    this.gameManager.RightKeyDown = true;
                    this.gameManager.MovePlayerShipRight();
                    break;
                case VirtualKey.Space:
                    this.gameManager.CreateAndPlacePlayerShipBullet();
                    break;
                case VirtualKey.Up:
                    this.gameManager.MovePlayerShipUp();
                    break;
                case VirtualKey.Down:
                    this.gameManager.MovePlayerShipDown();
                    break;
            }
        }

        private void coreWindowOnKeyUp(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.gameManager.LeftKeyDown = false;
                    break;
                case VirtualKey.Right:
                    this.gameManager.RightKeyDown = false;
                    break;
            }
        }

        #endregion
    }
}