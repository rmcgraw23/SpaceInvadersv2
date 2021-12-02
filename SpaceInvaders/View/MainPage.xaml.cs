using SpaceInvaders.Model;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using VirtualKey = Windows.System.VirtualKey;

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

            ApplicationView.PreferredLaunchViewSize = new Size { Width = ApplicationWidth, Height = ApplicationHeight };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));

            Window.Current.CoreWindow.KeyDown += this.coreWindowOnKeyDown;
            Window.Current.CoreWindow.KeyUp += this.coreWindowOnKeyUp;

            this.gameManager = new GameManager(ApplicationHeight, ApplicationWidth);
            this.gameManager.InitializeGame(this.theCanvas);

            this.gameManager.ScoreCountUpdated += this.ScoreOnScoreCountUpdated;
            this.gameManager.GameOverUpdated += this.GameOnGameOverUpdated;
            this.gameManager.LivesCountUpdated += this.LivesOnLivesCountUpdated;

        }

        #endregion

        #region Methods

        private void ScoreOnScoreCountUpdated(int score)
        {
            this.scoreTextBlock.Text = "Score: " + score.ToString();
        }

        private void LivesOnLivesCountUpdated(int lives)
        {
            this.livesTextBlock.Text = "Lives: " + lives.ToString();
        }

        private async void GameOnGameOverUpdated(string title, string content)
        {
            var playerLostDialog = new ContentDialog
            {
                Title = title,

                Content = content,
                PrimaryButtonText = "Ok"
            };

            await playerLostDialog.ShowAsync();
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