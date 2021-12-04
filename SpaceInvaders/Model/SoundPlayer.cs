﻿using System;
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
        public static async void PlaySound(string fileName)
        {
            var mediaPlayer = new MediaPlayer();

            var folder =
                await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            var file = await folder.GetFileAsync(fileName);

            mediaPlayer.AutoPlay = false;
            mediaPlayer.Source = MediaSource.CreateFromStorageFile(file);

            mediaPlayer.Play();
        }

        #endregion

    }
}
