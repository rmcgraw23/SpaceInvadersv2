using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace SpaceInvaders.View
{
    /// <summary>
    ///     The high score board for the game.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.Page" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class HighScoreBoardView
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

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a new high score board instance.
        /// </summary>
        public HighScoreBoardView()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size {Width = ApplicationWidth, Height = ApplicationHeight};
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));
        }

        #endregion

        #region DataMemebers

        #endregion
    }
}