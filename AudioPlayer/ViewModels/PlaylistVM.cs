using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.ViewModels
{
    public class PlaylistVM : BaseVM
    {
        private Dictionary<string, string> listOfSongs;

        public Dictionary<string, string> ListOfSongs
        {
            get
            {
                return this.listOfSongs;
            }
            set
            {
                if (this.listOfSongs == null)
                {
                    this.listOfSongs = new Dictionary<string, string>();
                }
                if (value != null)
                {
                    this.listOfSongs = value;
                    OnPropertyChanged("ListOfSongs");
                }
            }
        }

        public PlaylistVM()
        {
            this.ListOfSongs = new Dictionary<string, string>();
        }

        public PlaylistVM(Dictionary<string,string> playlist) : base()
        {
            this.ListOfSongs = playlist;
        }

        public void AddSong(Song song)
        {
            this.ListOfSongs.Add(song.Title, song.Path);
        }
    }
}
