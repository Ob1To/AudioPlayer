using AudioPlayer.ViewModels;
using System;
using System.Collections.Generic;
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
        private DispatcherTimer _timer;
        private bool _sliderpressed = false;

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

        private void On_Button_Add_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            //openPicker.SuggestedStartLocation = PickerLocationId.HomeGroup;
            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.PickMultipleFilesAndContinue();
        }

        private void DisplayFiles(StorageFile[] files)
        {
            if (files != null)
            {
                List<Song> newList = new List<Song>();
                foreach (var item in files)
                {
                    Song newSong = new Song(item.Name, item.Path);
                    newList.Add(newSong);
                }
                myPlaylist.Songs = newList;
            }
        }

        internal void PhonePickedFiles(FileOpenPickerContinuationEventArgs arguments)
        {
            var files = arguments.Files;
            DisplayFiles(files.ToArray());
        }

        private void On_Button_Play_Click(object sender, RoutedEventArgs e)
        {
            if (currentSong.Path != null)
            {
                Play_Media_Element();
                //mediaElement.Source = new Uri(currentSong.Path, UriKind.RelativeOrAbsolute);
                SetupTimer();
                TimeSpan recordingTime = mediaElement.NaturalDuration.TimeSpan;
                AudioPlayerSeek.Maximum = recordingTime.TotalSeconds;
                AudioPlayerSeek.SmallChange = 1;
                AudioPlayerSeek.LargeChange = Math.Min(10, recordingTime.Seconds / 10);
                //mediaElement.Play();
                //this.mediaElementTextBlock.Text = currentSong.Title.Substring(0, currentSong.Title.Length - 4);
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Song myObject = (sender as ListBox).SelectedItem as Song;
            if (myObject != null)
            {
                currentSong = myObject;
            }
        }

        private void AudioPlayerSeek_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (!_sliderpressed)
            {
                mediaElement.Position = TimeSpan.FromSeconds(e.NewValue);
            }
        }

        private void SetupTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(AudioPlayerSeek.StepFrequency);
            StartTimer();
        }

        private void StartTimer()
        {
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, object e)
        {
            if (!_sliderpressed)
            {
                AudioPlayerSeek.Value = mediaElement.Position.TotalSeconds;
            }
        }

        private void On_Button_Previous_Click(object sender, RoutedEventArgs e)
        {
            if (currentSong.Path != null)
            {
                int currentSongIndex = myPlaylist.Songs.IndexOf(currentSong);
                if (currentSongIndex > 0)
                {
                    currentSong = myPlaylist.Songs.ElementAt(currentSongIndex - 1);
                }
                else
                {
                    currentSong = myPlaylist.Songs.ElementAt(myPlaylist.Songs.Count - 1);
                }
                Play_Media_Element();
                listBoxSongs.SelectedItem = currentSong;
            }
        }

        private void On_Button_Next_Click(object sender, RoutedEventArgs e)
        {
            if (currentSong.Path != null)
            {
                int currentSongIndex = myPlaylist.Songs.IndexOf(currentSong);
                if (currentSongIndex < myPlaylist.Songs.Count - 1)
                {
                    currentSong = myPlaylist.Songs.ElementAt(currentSongIndex + 1);
                }
                else
                {
                    currentSong = myPlaylist.Songs.ElementAt(0);
                }
                Play_Media_Element();
                listBoxSongs.SelectedItem = currentSong;
            }
        }

        private void On_Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }

        private void Play_Media_Element()
        {
            mediaElement.Source = new Uri(currentSong.Path, UriKind.RelativeOrAbsolute);
            mediaElement.Play();
            this.mediaElementTextBlock.Text = currentSong.Title.Substring(0, currentSong.Title.Length - 4);
        }

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
