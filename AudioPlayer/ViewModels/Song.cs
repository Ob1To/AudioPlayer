using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.ViewModels
{
    public class Song : BaseVM
    {
        private string title;
        private string path;
        private TimeSpan duration;

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (value != null)
                {
                    this.title = value;
                }
            }
        }
        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                if (value != null)
                {
                    this.path = value;
                }
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return this.duration;
            }
            set
            {
                if (value != null)
                {
                    this.duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        public Song()
        {

        }

        public Song(string title,string path)
        {
            this.Title = title;
            this.Path = path;
        }
    }
}
