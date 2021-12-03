using System;
using Windows.Media.Playback;
using Windows.Media.Core;

namespace SpaceInvaders.Model
{
    /// <summary>
    /// The sound player class.
    /// </summary>
    public static class SoundPlayer
    {


        #region Methods

        /// <summary>
        /// Plays the sound.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public static async void PlaySound(String fileName)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();

            Windows.Storage.StorageFolder folder =
                await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync(fileName);

            mediaPlayer.AutoPlay = false;
            mediaPlayer.Source = MediaSource.CreateFromStorageFile(file);

            mediaPlayer.Play();
        }

        #endregion

    }
}
