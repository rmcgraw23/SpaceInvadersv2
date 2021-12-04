using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.Annotations;
using SpaceInvaders.Extension;
using SpaceInvaders.Model;
using SpaceInvaders.Model.HighScoreBoard;
using SpaceInvaders.Utility;

namespace SpaceInvaders.ViewModel
{
    public class HighScoreBoardViewModel : INotifyPropertyChanged
    {
        #region DataMemebrs

        public RelayCommand addCommand { get; set; }
        public RelayCommand sortCommand { get; set; }
        public RelayCommand sortNameFirstCommand { get; set; }
        public RelayCommand sortLevelFirstCommand { get; set; }

        #endregion

        #region Properties

        private HighScoreBoardManager HighScoreBoardManager;

        private ObservableCollection<HighScore> highScores;

        public ObservableCollection<HighScore> HighScores
        {
            get { return this.highScores; }
            set
            {
                this.highScores = value;
                OnPropertyChanged();
                //this.sortCommand.OnCanExecuteChanged();
                this.sortNameFirstCommand.OnCanExecuteChanged();
                this.sortLevelFirstCommand.OnCanExecuteChanged();
            }
        }

        private string name;

        public string Name
        {
            get { return this.name;}
            set
            {
                this.name = value;
                OnPropertyChanged();
            }
        }

        private bool inTopTen;

        public bool InTopTen
        {
            get { return this.inTopTen; }
            set
            {
                this.inTopTen = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public HighScoreBoardViewModel()
        {
            this.HighScoreBoardManager = new HighScoreBoardManager();
            
            this.addCommand = new RelayCommand(AddScore, CanAddScore);
            this.sortCommand = new RelayCommand(SortScores, CanSortScores);
            this.sortNameFirstCommand = new RelayCommand(SortNameFirstScores, CanSortNameFirstScores);
            this.sortLevelFirstCommand = new RelayCommand(SortLevelFirstScores, CanSortLevelFirstScores);
            this.HighScores = this.HighScoreBoardManager.HighScores.ToObservableCollection();
            this.name = "Add your name to the board!";
            this.inTopTen = false;
            this.GetStanding();
        }

        #endregion

        #region Methods

        private void GetStanding()
        {
            this.inTopTen = this.HighScoreBoardManager.WithinTopTen();

        }

        private bool CanAddScore(object obj)
        {
            return this.HighScoreBoardManager.WithinTopTen();
        }

        private void AddScore(object obj)
        {
            this.HighScoreBoardManager.AddHighScore(this.name);
            this.HighScores = this.HighScoreBoardManager.HighScores.ToObservableCollection();

        }

        private bool CanSortScores(object obj)
        {
            return this.HighScores != null;
        }

        private void SortScores(object obj)
        {
            this.HighScoreBoardManager.HighScores.Sort();
        }

        private bool CanSortNameFirstScores(object obj)
        {
            return this.HighScores != null;
        }

        private void SortNameFirstScores(object obj)
        {
            this.HighScoreBoardManager.HighScores.Sort(new NameScoreLevelComparer());
            this.HighScores = this.HighScoreBoardManager.HighScores.ToObservableCollection();
        }

        private bool CanSortLevelFirstScores(object obj)
        {
            return this.HighScores != null;
        }

        private void SortLevelFirstScores(object obj)
        {
            this.HighScoreBoardManager.HighScores.Sort(new LevelScoreNameComparer());
            this.HighScores = this.HighScoreBoardManager.HighScores.ToObservableCollection();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void sortByName()
        {
            this.HighScoreBoardManager.HighScores.Sort(new NameScoreLevelComparer());
            this.highScores = this.HighScoreBoardManager.HighScores.ToObservableCollection();
        }

        public void sortByNameFirst(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.HighScoreBoardManager.HighScores.Sort(new NameScoreLevelComparer());
            this.highScores = this.HighScoreBoardManager.HighScores.ToObservableCollection();
        }

        public void sortByLevelFirst(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        #endregion
    }
}
