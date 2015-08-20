using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.ViewModels
{
    public class Playlist
    {
        private string playListName;
        public string PlaylistName
        {
            get
            {
                return this.playListName;
            }
            set
            {
                if (value != null)
                {
                    this.playListName = value;
                }
            }
        }

        private string playListFileName;
        public string PlayListFileName
        {
            get
            {
                return this.playListFileName;
            }
            set
            {
                if (value != null)
                {
                    this.playListFileName = value;
                }
            }
        }
    }
}
