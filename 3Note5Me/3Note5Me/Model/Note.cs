using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace _3Note5Me.Model{
    public class Note{
        public int id { get; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool readOnly { get; set; }
        public StorageFile File { get; }

        public Note(int inID, string inTitle, string inContent){
            Content = inContent; id = inID; Title = inTitle;
        }

        public Note(int inID) {
            id = inID;
            readOnly = true;
            Title = "New Note";
        }

        
    }
}
