﻿using AudioPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace AudioPlayer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private PlaylistVM myPlaylist = new PlaylistVM();
        private Song currentSong = new Song();
        private DispatcherTimer timer;
        private bool isSliderPressed = false;

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = myPlaylist;
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.
            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        /// <summary>
        /// Click Button Methods
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void On_Button_Add_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.PickMultipleFilesAndContinue();
        }

        //private void On_Button_Play_Click(object sender, RoutedEventArgs e)
        //{
        //    if (currentSong.Path != null)
        //    {
        //        Play_Media_Element();

        //        SetupTimer();
        //        TimeSpan recordingTime = mediaElement.NaturalDuration.TimeSpan;
        //        AudioPlayerSeek.Maximum = recordingTime.TotalSeconds;
        //        AudioPlayerSeek.SmallChange = 1;
        //        AudioPlayerSeek.LargeChange = Math.Min(10, recordingTime.Seconds / 10);

        //    }
        //}

        //private void On_Button_Stop_Click(object sender, RoutedEventArgs e)
        //{
        //    this.mediaElement.Stop();
        //}

        //private void On_Button_Previous_Click(object sender, RoutedEventArgs e)
        //{
        //    if (currentSong.Path != null)
        //    {
        //        int currentSongIndex = myPlaylist.Songs.IndexOf(currentSong);
        //        if (currentSongIndex > 0)
        //        {
        //            currentSong = myPlaylist.Songs.ElementAt(currentSongIndex - 1);
        //        }
        //        else
        //        {
        //            currentSong = myPlaylist.Songs.ElementAt(myPlaylist.Songs.Count - 1);
        //        }
        //        Play_Media_Element();
        //        listBoxSongs.SelectedItem = currentSong;
        //    }
        //}

        //private void On_Button_Next_Click(object sender, RoutedEventArgs e)
        //{
        //    if (currentSong.Path != null)
        //    {
        //        int currentSongIndex = myPlaylist.Songs.IndexOf(currentSong);
        //        if (currentSongIndex < myPlaylist.Songs.Count - 1)
        //        {
        //            currentSong = myPlaylist.Songs.ElementAt(currentSongIndex + 1);
        //        }
        //        else
        //        {
        //            currentSong = myPlaylist.Songs.ElementAt(0);
        //        }
        //        Play_Media_Element();
        //        listBoxSongs.SelectedItem = currentSong;
        //    }
        //}

        //private void On_Button_Delete_Click(object sender, RoutedEventArgs e)
        //{
        //    Song currentItem = ((Button)sender).DataContext as Song;
        //    myPlaylist.DeleteSong(currentItem);

        //    //listBoxSongs.ItemsSource = myPlaylist.Songs;
        //}

        ///// <summary>
        ///// OnChanged Metheds
        ///// </summary>
        ///// <param name="files"></param>

        //private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Song myObject = (sender as ListBox).SelectedItem as Song;
        //    if (myObject != null)
        //    {
        //        currentSong = myObject;
        //    }
        //}

        //private void AudioPlayerSeek_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        //{
        //    if (!isSliderPressed)
        //    {
        //        mediaElement.Position = TimeSpan.FromSeconds(e.NewValue);
        //    }
        //}

        //private void Grid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        //{
        //    SliderSetter();
        //    Play_Media_Element();
        //}

        ///// <summary>
        ///// Helpers
        ///// </summary>
        ///// <param name="files"></param>

        private void DisplayFiles(StorageFile[] files)
        {
            if (files != null)
            {
                ObservableCollection<Song> newList = new ObservableCollection<Song>();
                foreach (var item in files)
                {
                    Song newSong = new Song(item.Name, item.Path);
                    newList.Add(newSong);
                }

                if (myPlaylist.Songs != null)
                {
                    foreach (var item in myPlaylist.Songs)
                    {
                        newList.Add(item);
                    }
                }
                myPlaylist.Songs = newList;
            }
        }

        internal void PhonePickedFiles(FileOpenPickerContinuationEventArgs arguments)
        {
            var files = arguments.Files;
            DisplayFiles(files.ToArray());
        }

        //private void Play_Media_Element()
        //{

        //    this.mediaElementTextBlock.Text = currentSong.Title.Substring(0, currentSong.Title.Length - 4);
        //    this.mediaElement.Source = new Uri(currentSong.Path, UriKind.RelativeOrAbsolute);
        //    this.mediaElement.Play();
        //}

        //private void SliderSetter()
        //{
        //    SetupTimer();
        //    TimeSpan recordingTime = mediaElement.NaturalDuration.TimeSpan;
        //    AudioPlayerSeek.Maximum = recordingTime.TotalSeconds;
        //    AudioPlayerSeek.SmallChange = 1;
        //    AudioPlayerSeek.LargeChange = Math.Min(10, recordingTime.Seconds / 10);
        //}

        //private void SetupTimer()
        //{
        //    timer = new DispatcherTimer();
        //    timer.Interval = TimeSpan.FromSeconds(AudioPlayerSeek.StepFrequency);
        //    StartTimer();
        //}

        //private void StartTimer()
        //{
        //    timer.Tick += Timer_Tick;
        //    timer.Start();
        //}

        //private void Timer_Tick(object sender, object e)
        //{
        //    if (!isSliderPressed)
        //    {
        //        AudioPlayerSeek.Value = mediaElement.Position.TotalSeconds;
        //    }
        //}



        //private double SliderFrequency(TimeSpan timevalue)
        //{
        //    double stepfrequency = -1;

        //    double absvalue = (int)Math.Round(timevalue.TotalSeconds, MidpointRounding.AwayFromZero);

        //    stepfrequency = (int)(Math.Round(absvalue / 100));

        //    if (timevalue.TotalMinutes >= 10 && timevalue.TotalMinutes < 30)
        //    {
        //        stepfrequency = 10;
        //    }
        //    else if (timevalue.TotalMinutes >= 30 && timevalue.TotalMinutes < 60)
        //    {
        //        stepfrequency = 30;
        //    }
        //    else if (timevalue.TotalHours >= 1)
        //    {
        //        stepfrequency = 60;
        //    }

        //    if (stepfrequency == 0) stepfrequency += 1;

        //    if (stepfrequency == 1)
        //    {
        //        stepfrequency = absvalue / 100;
        //    }

        //    return stepfrequency;
        //}
    }
}
