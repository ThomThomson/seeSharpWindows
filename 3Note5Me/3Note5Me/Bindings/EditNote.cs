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
    class EditNote : ICommand{
        public event EventHandler CanExecuteChanged;
        private MainPageData mpd;
        public EditNote(MainPageData inMpd){
            mpd = inMpd;
        }

        public bool CanExecute(object parameter){
            return true;
        }

        public void Execute(object parameter){
            mpd.textAreaEditable = !mpd.textAreaEditable;
        }

        public void FireCanExecuteChanged(){
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
