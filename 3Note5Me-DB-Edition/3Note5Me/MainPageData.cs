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
using Windows.UI.Popups;

namespace _3Note5Me.ViewModels{
    public class MainPageData : INotifyPropertyChanged{

        #region <----- P R O P E R T I E S ----->
        public List<Note> Notes { get; set; }
        public ObservableCollection<Note> ShownNotes { get; set; }
        public StorageFolder NotesFolder { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        //C O M M A N D  B I N D I N G S
        public AddNote AddNoteCommand { get; }
        public EditNote EditNoteCommand { get; }
        public DelNote DelNoteCommand { get; }
        public SaveNote SaveNoteCommand { get; }
        //P R I V A T E  N O T E  V A R I A B L E S
        private Note _selectedNote;
        private bool _currentNoteReadOnly;
        private string _currentNoteContent;
        private string _searchText = "";

        //A C C E S S O R S  &  M U T A T O R S
        public Note SelectedNote {
            get { return _selectedNote; }
            set {
                if ((_selectedNote == null || _selectedNote.Content == CurrentNoteContent) && value != null) {
                    _selectedNote = value;
                    CurrentNoteContent = _selectedNote.Content;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedNote"));
                    CurrentNoteReadOnly = true;
                    EditNoteCommand.FireCanExecuteChanged();
                    DelNoteCommand.FireCanExecuteChanged();
                    SaveNoteCommand.FireCanExecuteChanged();
                } else if (CurrentNoteContent != "" && value != null) {
                    SaveOnSwitch(value);
                } else {
                    CurrentNoteContent = "";
                    CurrentNoteReadOnly = true;
                    _selectedNote = null;
                    EditNoteCommand.FireCanExecuteChanged();
                    DelNoteCommand.FireCanExecuteChanged();
                    SaveNoteCommand.FireCanExecuteChanged();
                }

            }
        }//E N D SelectedNote

        public bool CurrentNoteReadOnly { get {return _currentNoteReadOnly; }
            set {
                _currentNoteReadOnly = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentNoteReadOnly"));
                SaveNoteCommand.FireCanExecuteChanged();
            }
        }//E N D CurrentNoteReadOnly

        public string CurrentNoteContent { get {return _currentNoteContent; }
            set {
                _currentNoteContent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentNoteContent"));
            }
        }//E N D CurrentNoteContent

        public string SearchText { get { return _searchText; }
            set {
                if (value == _searchText) {
                    return;
                }
                _searchText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SearchText"));
                Search();
            }
        }//E N D SearchText

        #endregion

        #region <----- M E T H O D S ----->
        //C O N S T R U C T O R
        public MainPageData() {
            AddNoteCommand = new AddNote(this);
            EditNoteCommand = new EditNote(this);
            DelNoteCommand = new DelNote(this);
            SaveNoteCommand = new SaveNote(this);
            Init();
        }//E N D  C O N S T R U C T O R

        //M E T H O D Init
        public void Init() {
            using (var dbNotes = new Model.NoteContext()) {
                Notes = dbNotes.Notes.ToList();
            }
            ShownNotes = new ObservableCollection<Note>();
            //await PopulateNotes();
            Search();
        }//E N D  M E T H O D Init

        //M E T H O D  SaveOnSwitch
        public async void SaveOnSwitch(Note noteToSwitchTo) {
            MessageDialog SaveDialog = new MessageDialog("Save current note before switching? ");
            SaveDialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            SaveDialog.Commands.Add(new UICommand("No") { Id = 1 });
            SaveDialog.DefaultCommandIndex = 1;
            SaveDialog.CancelCommandIndex = 1;
            var result = await SaveDialog.ShowAsync();
            if ((int)result.Id == 0) {
                SaveNoteCommand.save();
            }
            _selectedNote = noteToSwitchTo;
            CurrentNoteContent = _selectedNote.Content;
            CurrentNoteReadOnly = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedNote"));
            EditNoteCommand.FireCanExecuteChanged();
            DelNoteCommand.FireCanExecuteChanged();
            SaveNoteCommand.FireCanExecuteChanged();
        }//E N D  M E T H O D SaveOnSwitch


        //M E T H O D Search
        public void Search() {
            ShownNotes.Clear();
            var filtered = Notes.Where(note => note.Title.ToUpperInvariant().Contains(SearchText.ToUpperInvariant())).ToList();
            foreach(var note in filtered) {
                ShownNotes.Add(note);
            }
        }//E N D  M E T H O D Search
        #endregion
    }
}
