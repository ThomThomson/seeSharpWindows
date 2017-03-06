using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using _3Note5Me.ViewModels;

namespace _3Note5Me.Bindings
{
    class AddNote : ICommand{
        public event EventHandler CanExecuteChanged;
        private MainPageData mpd;
        public AddNote(MainPageData inMpd){
            mpd = inMpd;
        }

        public bool CanExecute(object parameter){
            return true;
        }

        public void Execute(object parameter){
            Model.Note newNote = new Model.Note(mpd.Notes.Count);
            mpd.SelectedNote = newNote;
            mpd.Notes.Add(newNote);
        }

        public void FireCanExecuteChanged(){
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
