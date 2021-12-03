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
                this.sortCommand.OnCanExecuteChanged();
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

        #endregion

        #region Constructors

        public HighScoreBoardViewModel()
        {
            this.HighScoreBoardManager = new HighScoreBoardManager();
            this.highScores = this.HighScoreBoardManager.HighScores.ToObservableCollection();
            this.addCommand = new RelayCommand(AddScore, CanAddScore);
            this.sortCommand = new RelayCommand(SortScores, CanSortScores);
            this.sortNameFirstCommand = new RelayCommand(SortNameFirstScores, CanSortNameFirstScores);
            this.sortLevelFirstCommand = new RelayCommand(SortLevelFirstScores, CanSortLevelFirstScores);
        }

        #endregion

        #region Methods
        private bool CanAddScore(object obj)
        {
            return this.HighScoreBoardManager.WithinTopTen();
        }

        private void AddScore(object obj)
        {
            this.HighScoreBoardManager.AddHighScore();
            this.Students = this.roster.Students.ToObservableCollection();

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
        }

        private bool CanSortLevelFirstScores(object obj)
        {
            return this.HighScores != null;
        }

        private void SortLevelFirstScores(object obj)
        {
            this.HighScoreBoardManager.HighScores.Sort(new LevelScoreNameComparer());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
