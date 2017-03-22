using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Text;

namespace _3Note5Me.Model{
    public class Note {
        public int id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        //public StorageFile File { get; set; }

        public Note(string inTitle) {
            Title = inTitle;
        }

        public Note() { }        
    }
}
