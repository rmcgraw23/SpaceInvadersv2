using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.Media.Core;

namespace SpaceInvaders.Model
{
    /// <summary>
    /// The sound player class.
    /// </summary>
    public static class SoundPlayer
    {
        #region DataMembers

        //private readonly MediaPlayer mediaPlayer;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundPlayer"/> class.
        /// Precondition: none
        /// Post-Condition: none
        /// </summary>
        //public SoundPlayer()
        //{
        //    this.mediaPlayer = new MediaPlayer();
        //}

        #endregion


        #region Methods

        /// <summary>
        /// Plays the sound.
        /// Precondition: none
        /// Post-Condition: none
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        //public static async void PlaySound(String fileName)
        //{
        //    Windows.Storage.StorageFolder folder =
        //        await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
        //    Windows.Storage.StorageFile file = await folder.GetFileAsync(fileName);

        //    this.mediaPlayer.AutoPlay = false;
        //    this.mediaPlayer.Source = MediaSource.CreateFromStorageFile(file);

        //    this.mediaPlayer.Play();
        //}

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
