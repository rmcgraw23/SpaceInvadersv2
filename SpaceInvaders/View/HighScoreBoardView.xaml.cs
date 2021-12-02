using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.View
{
    public sealed partial class HighScoreBoardView
    {

        #region Constructors

        public HighScoreBoardView()
        {
            this.AskForNameAsync();
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
