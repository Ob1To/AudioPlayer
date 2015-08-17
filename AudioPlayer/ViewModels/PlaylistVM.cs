﻿using AudioPlayer.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
                return this.currentSong;
            }
            set
            {
                if (this.currentSong != value)
                {
                    this.currentSong = value;
                }
            }
        }

        private MediaElement myMediaElement;
        public MediaElement MyMediaElement

        {
            get
            {
                return this.myMediaElement;
            }
            set
            {
                if (this.myMediaElement != value)
                {
                    this.myMediaElement = value;
                    OnPropertyChanged("MyMediaElement");
                }
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

        private void PerformPlay()
        {
            
        }

        private void Play_Media_Element()
        {
            this.SongNameTextBlock = this.CurrentSong.Title.Substring(0, currentSong.Title.Length - 4);
            this.MyMediaElement.Source = new Uri(this.CurrentSong.Path, UriKind.RelativeOrAbsolute);
            this.MyMediaElement.Play();
        }

        public void DeleteSong(Song s)
        {
            this.songs.Remove(s);
        }
    }
}
