using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.ViewModels
{
    public class Song
    {
        private string title;
        private string path;

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

        public Song(string title,string path)
        {
            this.Title = title;
            this.Path = path;
        }
    }
}
