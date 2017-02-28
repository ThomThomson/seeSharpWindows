using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3Note5Me.Model;
using _3Note5Me.Bindings;

namespace _3Note5Me.ViewModels{
    class MainPageData : INotifyPropertyChanged{
        public List<Note> Notes { get; set; }
        private Note _SelectedNote;

        public bool textAreaEditable { get; set; }

        public AddNote AddNoteCommand { get; }

        public EditNote EditNoteCommand { get; }
        public DelNote DelNoteCommand { get; }
        public SaveNote SaveNoteCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public Note SelectedNote{
            get{
                return _SelectedNote;
            }
            set{
                _SelectedNote = value;
                textAreaEditable = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedNote"));
            }
        }

        public MainPageData(){
            AddNoteCommand = new AddNote(this);
            AddNoteCommand.FireCanExecuteChanged();
            Notes = new List<Note>();
            Notes.Add(new Note(1, "NOTE 1 TITLE", "Some sweet content yo"));
            Notes.Add(new Note(2, "NOTE 2 TITLE", "Some more sweet content for your liking"));
            Notes.Add(new Note(3, "NOTE 3 TITLE", "Hello ladies & Gents. Content here"));
            Notes.Add(new Note(4, "NOTE 4 TITLE", "All bros love this content"));
            Notes.Add(new Note(5, "NOTE 5 TITLE", "I have. Just the best. Content. There is."));
        }
    }
}
