using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.ViewModels
{
    public class PlaylistVM : BaseVM
    {
        private List<Song> songs;
        public List<Song> Songs
        {
            get
            {
                return this.songs;
            }
            set
            {
                if (this.songs == null)
                {
                    this.songs = new List<Song>();
                }
                if (value != null)
                {
                    this.songs = value;
                    OnPropertyChanged("Songs");
                }
            }
        }
    }
}
