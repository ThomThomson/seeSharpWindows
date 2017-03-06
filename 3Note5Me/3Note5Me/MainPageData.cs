using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3Note5Me.Model;
using _3Note5Me.Bindings;
using Windows.Storage;
using System.Collections.ObjectModel;

namespace _3Note5Me.ViewModels{
    class MainPageData : INotifyPropertyChanged{
        public ObservableCollection<Note> Notes { get; set; }
        private StorageFolder NotesFolder;
        public event PropertyChangedEventHandler PropertyChanged;
        //C O M M A N D  B I N D I N G S
        public AddNote AddNoteCommand { get; }
        public EditNote EditNoteCommand { get; }
        public DelNote DelNoteCommand { get; }
        public SaveNote SaveNoteCommand { get; }
        private Note _SelectedNote;
        public Note SelectedNote{get{return _SelectedNote;}
            set{
                _SelectedNote = value;
                _SelectedNote.readOnly = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedNote"));
            }
        }

        //C O N S T R U C T O R
        public MainPageData(){
            AddNoteCommand = new AddNote(this);
            EditNoteCommand = new EditNote(this);
            DelNoteCommand = new DelNote(this);
            SaveNoteCommand = new SaveNote(this);
            Init();
        }//E N D  C O N S T R U C T O R

        //M E T H O D init
        private async void Init() {
            Notes = new ObservableCollection<Note>();
            await PopulateNotes();
        }

        //M E T H O D PopulateNotes
        private async Task PopulateNotes() {
            Notes = new ObservableCollection<Note>();//delete notes
            StorageFolder directory = ApplicationData.Current.LocalFolder;
            //Create folder for notes, or get folder for notes.
            if (NotesFolder == null) {
                try {
                    NotesFolder = await directory.GetFolderAsync("Notes");
                }
                catch {
                    NotesFolder = await directory.CreateFolderAsync("Notes");
                }
            }
            IReadOnlyList<StorageFile> files = await NotesFolder.GetFilesAsync();
            int id = 0;
            foreach (StorageFile currentFile in files) {
                Notes.Add(new Model.Note(id, currentFile.Name.Substring(0, currentFile.Name.LastIndexOf(".")), await FileIO.ReadTextAsync(currentFile)));
                id++;
            }
        }

    }
}
