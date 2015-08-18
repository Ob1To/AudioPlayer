using AudioPlayer.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AudioPlayer.ViewModels
{
    public class PlaylistVM : BaseVM
    {
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
                    OnPropertyChanged("Songs");
                }
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

        private void PerformPlay()
        {
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

        private void Play_Media_Element()
        {
           
            this.SongNameTextBlock = this.CurrentSong.Title.Substring(0, currentSong.Title.Length - 4);

            //this.MyMediaElement = MainPage.currentMainPage.FindName("mPlayer") as MediaElement;
            //MainPage.currentMainPage.mPlayer.Source = new Uri(this.CurrentSong.Path, UriKind.RelativeOrAbsolute);
            //MainPage.currentMainPage.mPlayer.Play();
            this.MyMediaElement.Source = new Uri(this.CurrentSong.Path, UriKind.RelativeOrAbsolute);
            this.MyMediaElement.Play();
        }

        public void DeleteSong(Song s)
        {
            this.songs.Remove(s);
        }
    }
}
