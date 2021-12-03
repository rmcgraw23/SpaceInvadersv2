using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SpaceInvaders.View
{
    public sealed partial class HighScoreBoardView
    {
        #region DataMemebers

        private bool show;

        #endregion
        #region Constructors

        public HighScoreBoardView()
        {
            //this.AskForNameAsync();
            //this.name = "";
            
        }

        #endregion

        #region Methods

        public async Task AskForNameAsync()
        {
            //if (this.gameManager.OnTheBoard)
            {
                var nameInputDialog = new NameInputDialog();
                await nameInputDialog.ShowAsync();
            }
        }

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
