using AudioPlayer.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using System.Runtime.Serialization;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace AudioPlayer.ViewModels
{
    public sealed class SystemMediaTransportControls
    {

    }

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
                myListBox.DoubleTapped += MyListBox_DoubleTapped;
                return this.myListBox;
            }
        }

        private void MyListBox_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            PerformPlay();
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

        private ObservableCollection<Playlist> listOfPlaylists;
        public ObservableCollection<Playlist> ListOfPlayLists
        {
            get
            {
                if (this.listOfPlaylists == null)
                {
                    this.listOfPlaylists = new ObservableCollection<Playlist>();
                }
                return this.listOfPlaylists;
            }
        }

        private Playlist currentPlaylist;
        public Playlist CurrentPlaylist
        {
            get
            {
                if (this.currentPlaylist == null)
                {
                    this.currentPlaylist = new Playlist();
                }
                return this.currentPlaylist;
            }
            set
            {
                if (this.currentPlaylist != value)
                {
                    this.currentPlaylist = value;
                }
            }
        }

        private Popup savePopUp;

        private PopUpWindowSave popUpWindowSave;

        private Popup loadPopUp;

        private PopUpWindowLoad popUpWindowLoad;

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
                if (this.goLeftCommand == null)
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
                              this.deleteCommand = new RelayCommand(this.PerformDelete);
                }
                return this.deleteCommand;
            }
        }

        private ICommand saveCommand;
        public ICommand Save
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new DelegateCommand(this.PerformSave);
                }
                return this.saveCommand;
            }
        }

        private ICommand savePlaylistCommand;
        public ICommand SavePlaylist
        {
            get
            {
                if (this.savePlaylistCommand == null)
                {
                    this.savePlaylistCommand = new RelayCommand(this.PerformSavePlaylist);
                }
                return this.savePlaylistCommand;
            }
        }

        private ICommand cancelSavingCommand;
        public ICommand CancelSaving
        {
            get
            {
                if (this.cancelSavingCommand == null)
                {
                    this.cancelSavingCommand = new DelegateCommand(this.PerformCancelSaving);
                }
                return this.cancelSavingCommand;
            }
        }

        private ICommand loadCommand;
        public ICommand Load
        {
            get
            {
                if (this.loadCommand == null)
                {
                    this.loadCommand = new DelegateCommand(this.PerformLoad);
                }
                return this.loadCommand;
            }
        }

        private ICommand loadPlaylistCommand;

        public ICommand LoadPlaylist
        {
            get
            {
                if (this.loadPlaylistCommand == null)
                {
                    this.loadPlaylistCommand = new DelegateCommand(this.PerformLoadPlaylist);
                }
                return this.loadPlaylistCommand;
            }
        }

        private ICommand cancelLoadingCommand;

        public ICommand CancelLoading
        {
            get
            {
                if (this.cancelLoadingCommand == null)
                {
                    this.cancelLoadingCommand = new DelegateCommand(this.PerformCancelLoading);
                }
                return this.cancelLoadingCommand;
            }
        }


        /// <summary>
        /// Commands
        /// </summary>
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

        private void PerformAdd()
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.FileTypeFilter.Add(".mp3");
            openPicker.PickMultipleFilesAndContinue();
        }

        private void PerformDelete(object obj)
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


        private void PerformSave()
        {
            savePopUp = new Popup();
            //savePopUp.VerticalOffset = (Window.Current.Bounds.Height / 2);
            //savePopUp.VerticalOffset = 250;
            //savePopUp.HorizontalOffset = 100;
            MainPage.currentMainPage.IsHitTestVisible = false;
            popUpWindowSave = new PopUpWindowSave();
            savePopUp.Child = popUpWindowSave;
            popUpWindowSave.Width = Window.Current.Bounds.Width;
            popUpWindowSave.Height = Window.Current.Bounds.Height / 2;
            savePopUp.IsOpen = true;
        }

        private async void PerformSavePlaylist(object parameter)
        {
            TextBox playlistNameTextBox = popUpWindowSave.FindName("playlistNameTextBox") as TextBox;
            Playlist playlist = new Playlist();

            if (!playlistNameTextBox.Equals(null))
            {
                playlist.PlaylistName = playlistNameTextBox.Text;
                playlist.PlayListFileName = playlistNameTextBox.Text + ".txt";

                ListOfPlayLists.Add(playlist);

                await saveStringToLocalFile(playlist.PlayListFileName);

                MainPage.currentMainPage.IsHitTestVisible = true;
                savePopUp.IsOpen = false;
            }
        }

        private void PerformCancelSaving()
        {
            MainPage.currentMainPage.IsHitTestVisible = true;
            savePopUp.IsOpen = false;
        }

        private void PerformLoad()
        {
            loadPopUp = new Popup();
            //loadPopUp.VerticalOffset = 250;
            //loadPopUp.HorizontalOffset = 100;
            MainPage.currentMainPage.IsHitTestVisible = false;
            popUpWindowLoad = new PopUpWindowLoad();
            loadPopUp.Child = popUpWindowLoad;
            popUpWindowLoad.Width = Window.Current.Bounds.Width;
            popUpWindowLoad.Height = Window.Current.Bounds.Height / 2;
            loadPopUp.IsOpen = true;
        }

        private void PerformLoadPlaylist()
        {
            if (currentPlaylist != null)
            {
                MainPage.currentMainPage.IsHitTestVisible = true;
                loadPopUp.IsOpen = false;
                ConvertToList(currentPlaylist.PlayListFileName);
            }
        }

        private void PerformCancelLoading()
        {
            MainPage.currentMainPage.IsHitTestVisible = true;
            loadPopUp.IsOpen = false;
        }


        /// <summary>
        /// Helpers
        /// </summary>
        /// <param name="files"></param>
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
            this.MyMediaElement.Source = new Uri(this.CurrentSong.Path, UriKind.RelativeOrAbsolute);
            this.SongNameTextBlock = this.CurrentSong.Title.Substring(0, currentSong.Title.Length - 4);
            //this.MyMediaElement = MainPage.currentMainPage.FindName("mPlayer") as MediaElement;
            //MainPage.currentMainPage.mPlayer.Source = new Uri(this.CurrentSong.Path, UriKind.RelativeOrAbsolute);
            //MainPage.currentMainPage.mPlayer.Play();
            this.MyMediaElement.Play();
        }

        private async Task saveStringToLocalFile(string filename)
        {
            string info = JsonConvert.SerializeObject(this.Songs, Formatting.Indented);

            // saves the string 'content' to a file 'filename' in the app's local storage folder
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(info.ToCharArray());

            // create a file with the given filename in the local folder; replace any existing file with the same name
            StorageFile file = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            // write the char array created from the content string into the file
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                stream.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        public async Task<string> ReadFileContentsAsync(string fileName)
        {
            var folder = ApplicationData.Current.LocalFolder;

            try
            {
                var file = await folder.OpenStreamForReadAsync(fileName);

                using (var streamReader = new StreamReader(file))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async void ConvertToList(string fileName)
        {
            var list = await this.ReadFileContentsAsync(fileName);
            ObservableCollection<Song> s = JsonConvert.DeserializeObject<ObservableCollection<Song>>(list);
            this.Songs = s;
        }
    }
}
