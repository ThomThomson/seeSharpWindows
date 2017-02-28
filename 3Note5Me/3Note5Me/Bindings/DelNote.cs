using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using _3Note5Me.ViewModels;
using Windows.UI.Popups;

namespace _3Note5Me.Bindings
{
    class DelNote : ICommand{
        public event EventHandler CanExecuteChanged;
        private MainPageData mpd;
        public DelNote(MainPageData inMpd){
            mpd = inMpd;
        }

        public bool CanExecute(object parameter){
            return true;
        }

        public async void Execute(object parameter){
            MessageDialog DelDialog = new MessageDialog("Really (not actually) Delete note? ");
            DelDialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            DelDialog.Commands.Add(new UICommand("No") { Id = 1 });
            DelDialog.DefaultCommandIndex = 1;
            DelDialog.CancelCommandIndex = 1;
            var result = await DelDialog.ShowAsync();
        }

        public void FireCanExecuteChanged(){
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
