using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SpaceInvaders.Annotations;
using SpaceInvaders.Extension;
using SpaceInvaders.Model.HighScoreBoard;
using SpaceInvaders.Utility;

namespace SpaceInvaders.ViewModel
{
    /// <summary>
    /// Manages connection between model and view
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class HighScoreBoardViewModel : INotifyPropertyChanged
    {
        #region DataMemebrs     

        private HighScoreBoardManager HighScoreBoardManager;

        private ObservableCollection<HighScore> highScores;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the add command.
        /// </summary>
        /// <value>
        /// The add command.
        /// </value>
        public RelayCommand AddCommand { get; set; }

        /// <summary>
        /// Gets or sets the sort command.
        /// </summary>
        /// <value>
        /// The sort command.
        /// </value>
        public RelayCommand SortCommand { get; set; }

        /// <summary>
        /// Gets or sets the sort name first command.
        /// </summary>
        /// <value>
        /// The sort name first command.
        /// </value>
        public RelayCommand SortNameFirstCommand { get; set; }

        /// <summary>
        /// Gets or sets the sort level first command.
        /// </summary>
        /// <value>
        /// The sort level first command.
        /// </value>
        public RelayCommand SortLevelFirstCommand { get; set; }

        /// <summary>
        /// Gets or sets the high scores.
        /// </summary>
        /// <value>
        /// The high scores.
        /// </value>
        public ObservableCollection<HighScore> HighScores
        {
            get { return this.highScores; }
            set
            {
                this.highScores = value;
                this.OnPropertyChanged();
                this.SortNameFirstCommand.OnCanExecuteChanged();
                this.SortLevelFirstCommand.OnCanExecuteChanged();
            }
        }

        private string name;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.OnPropertyChanged();
            }
        }

        private bool inTopTen;

        /// <summary>
        /// Gets or sets a value indicating whether [in top ten].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [in top ten]; otherwise, <c>false</c>.
        /// </value>
        public bool InTopTen
        {
            get { return this.inTopTen; }
            set
            {
                this.inTopTen = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HighScoreBoardViewModel"/> class.
        /// </summary>
        public HighScoreBoardViewModel()
        {
            this.HighScoreBoardManager = new HighScoreBoardManager();
            
            this.AddCommand = new RelayCommand(this.addScore, this.canAddScore);
            this.SortCommand = new RelayCommand(this.sortScores, this.canSortScores);
            this.SortNameFirstCommand = new RelayCommand(this.sortNameFirstScores, this.canSortNameFirstScores);
            this.SortLevelFirstCommand = new RelayCommand(this.sortLevelFirstScores, this.canSortLevelFirstScores);
            this.HighScores = this.HighScoreBoardManager.HighScores.ToObservableCollection();
            this.name = "Add your name to the board!";
            this.inTopTen = false;
            this.getStanding();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the standing.
        /// </summary>
        private void getStanding()
        {
            this.inTopTen = this.HighScoreBoardManager.WithinTopTen();

        }

        private bool canAddScore(object obj)
        {
            return this.HighScoreBoardManager.WithinTopTen();
        }

        private void addScore(object obj)
        {
            this.HighScoreBoardManager.AddHighScore(this.name);
            this.HighScores = this.HighScoreBoardManager.HighScores.ToObservableCollection();

        }

        private bool canSortScores(object obj)
        {
            return this.HighScores != null;
        }

        private void sortScores(object obj)
        {
            this.HighScoreBoardManager.HighScores.Sort();
        }

        private bool canSortNameFirstScores(object obj)
        {
            return this.HighScores != null;
        }

        private void sortNameFirstScores(object obj)
        {
            this.HighScoreBoardManager.HighScores.Sort(new NameScoreLevelComparer());
            this.HighScores = this.HighScoreBoardManager.HighScores.ToObservableCollection();
        }

        private bool canSortLevelFirstScores(object obj)
        {
            return this.HighScores != null;
        }

        private void sortLevelFirstScores(object obj)
        {
            this.HighScoreBoardManager.HighScores.Sort(new LevelScoreNameComparer());
            this.HighScores = this.HighScoreBoardManager.HighScores.ToObservableCollection();
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// <returns></returns>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
