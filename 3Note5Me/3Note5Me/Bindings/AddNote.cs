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

        public bool CanExecute(object parameter)
        {
            return mpd.SelectedNote != null;
        }

        public void Execute(object parameter){
            Debug.Write("ADD EXECUTED");
            mpd.SelectedNote = null;
        }

        public void FireCanExecuteChanged(){
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
