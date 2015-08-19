using AudioPlayer.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace AudioPlayer.ViewModels
{
    public class PlaylistVM : BaseVM
    {
        private DispatcherTimer timer;
        private bool isSliderPressed = false;

        private string songNameTextBlock;
        public string SongNameTextBlock
        {
            get
            {
                return this.songNameTextBlock;
            }
            set
            {
                if (this.songNameTextBlock != value)
                {
                    this.songNameTextBlock = value;
                    OnPropertyChanged("SongNameTextBlock");
                }
            }
        }

        private Song currentSong;
        public Song CurrentSong
        {
            get
            {
                if (this.currentSong == null)
                {
                    this.currentSong = new Song();
                }
                return this.currentSong;
            }
            set
            {
                if (this.currentSong != value)
                {
                    this.currentSong = value;
                }
                OnPropertyChanged("CurrentSong");
            }
        }

        private MediaElement myMediaElement;
        public MediaElement MyMediaElement

        {
            get
            {
                if (myMediaElement == null)
                {
                    myMediaElement = MainPage.currentMainPage.FindName("mPlayer") as MediaElement;
                }
                return this.myMediaElement;
            }
            //set
            //{
            //    if (this.myMediaElement != value)
            //    {
            //        this.myMediaElement = value;
            //        OnPropertyChanged("MyMediaElement");
            //    }
            //}
        }

        private ListBox myListBox;
        public ListBox MyListBox
        {
            get
            {
                if (this.myListBox == null)
                {
                    this.myListBox = MainPage.currentMainPage.FindName("listBoxOfSongs") as ListBox;
                }
                return this.myListBox;
            }
        }

        private Slider mySlider;
        public Slider MySlider
        {
            get
            {
                if (this.mySlider == null)
                {
                    this.mySlider = MainPage.currentMainPage.FindName("AudioPlayerSeek") as Slider;
                }
                return this.mySlider;
            }
        }

        private ObservableCollection<Song> songs;
        public ObservableCollection<Song> Songs
        {
            get
            {
                if (this.songs == null)
                {
                    this.songs = new ObservableCollection<Song>();
                }
                return this.songs;
            }
            set
            {
                if (value != this.songs)
                {
                    this.songs = value;
                }
                OnPropertyChanged("Songs");
            }
        }

        private ICommand playCommand;
        public ICommand Play
        {
            get
            {
                if (this.playCommand == null)
                {
                    this.playCommand = new DelegateCommand(this.PerformPlay);
                }
                return this.playCommand;
            }
        }

        private ICommand stopCommand;
        public ICommand Stop
        {
            get
            {
                if (this.stopCommand == null)
                {
                    this.stopCommand = new DelegateCommand(this.PerformStop);
                }
                return this.stopCommand;
            }
        }

        private ICommand goLeftCommand;
        public ICommand GoLeft
        {
            get
            {
                if (this.goLeftCommand ==  null)
                {
                    this.goLeftCommand = new DelegateCommand(this.PerformGoLeft);
                }
                return this.goLeftCommand;
            }
        }

        private ICommand goRightCommand;
        public ICommand GoRight
        {
            get
            {
                if (this.goRightCommand == null)
                {
                    this.goRightCommand = new DelegateCommand(this.PerformGoRight);
                }
                return this.goRightCommand;
            }
        }

        private ICommand addCommand;
        public ICommand Add
        {
            get
            {
                if (this.addCommand == null)
                {
                    this.addCommand = new DelegateCommand(this.PerformAdd);
                }
                return this.addCommand;
            }
        }

        private ICommand deleteCommand;
        public ICommand Delete
        {
            get
            {
                if (this.deleteCommand == null)
                {
                    this.deleteCommand = new RelayCommand(this.DeleteSong);
                }
                return this.deleteCommand;
            }
        }

        private void DeleteSong(object obj)
        {
            var title = obj as string;
            foreach (var item in Songs)
            {
                if (item.Title.Equals(title))
                {
                    this.Songs.Remove(item);
                    break;
                }
            }
        }

        private void PerformPlayAgain()
        {
            Play_Media_Element();
        }

        private void PerformPlay()
        {
            SliderSetter();
            if (this.CurrentSong != null)
            {
                if (currentSong.Path != null)
                {
                    Play_Media_Element();
                }
            }
        }
        private void PerformStop()
        {
            if (currentSong.Path != null)
            {
                this.MyMediaElement.Stop();
            }
        }

        private void PerformGoLeft()
        {
            if (currentSong.Path != null)
            {
                int currentSongIndex = this.Songs.IndexOf(currentSong);
                if (currentSongIndex > 0)
                {
                    currentSong = this.Songs.ElementAt(currentSongIndex - 1);
                }
                else
                {
                    currentSong = this.Songs.ElementAt(this.Songs.Count - 1);
                }
                Play_Media_Element();
                this.MyListBox.SelectedItem = currentSong;
            }
        }

        private void PerformGoRight()
        {
            if (currentSong.Path != null)
            {
                int currentSongIndex = this.Songs.IndexOf(currentSong);
                if (currentSongIndex < this.Songs.Count - 1)
                {
                    currentSong = this.Songs.ElementAt(currentSongIndex + 1);
                }
                else
                {
                    currentSong = this.Songs.ElementAt(0);
                }
                Play_Media_Element();
                this.MyListBox.SelectedItem = currentSong;
            }
        }

        private void PerformAdd()
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.PickMultipleFilesAndContinue();
        }


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

                if (this.Songs != null)
                {
                    foreach (var item in this.Songs)
                    {
                        newList.Add(item);
                    }
                }
                this.Songs = newList;
            }
        }

        internal void PhonePickedFiles(FileOpenPickerContinuationEventArgs arguments)
        {
            var files = arguments.Files;
            DisplayFiles(files.ToArray());
        }

        private void Play_Media_Element()
        {
           
            this.SongNameTextBlock = this.CurrentSong.Title.Substring(0, currentSong.Title.Length - 4);

            //this.MyMediaElement = MainPage.currentMainPage.FindName("mPlayer") as MediaElement;
            //MainPage.currentMainPage.mPlayer.Source = new Uri(this.CurrentSong.Path, UriKind.RelativeOrAbsolute);
            //MainPage.currentMainPage.mPlayer.Play();
            this.MyMediaElement.Source = new Uri(this.CurrentSong.Path, UriKind.RelativeOrAbsolute);
            this.MyMediaElement.Play();
        }

        private void AudioPlayerSeek_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (!isSliderPressed)
            {
                this.MyMediaElement.Position = TimeSpan.FromSeconds(e.NewValue);
            }
        }

        private void SliderSetter()
        {
            SetupTimer();
            TimeSpan recordingTime = this.MyMediaElement.NaturalDuration.TimeSpan;
            this.MySlider.Maximum = recordingTime.TotalSeconds;
            this.MySlider.SmallChange = 1;
            this.MySlider.LargeChange = Math.Min(10, recordingTime.Seconds / 10);
        }

        private void SetupTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(this.MySlider.StepFrequency);
            StartTimer();
        }

        private void StartTimer()
        {
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            if (!isSliderPressed)
            {
                this.MySlider.Value = this.MyMediaElement.Position.TotalSeconds;
            }
        }
    }
}
