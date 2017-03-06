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
    class SaveNote : ICommand{
        public event EventHandler CanExecuteChanged;
        private MainPageData mpd;
        public SaveNote(MainPageData inMpd){
            mpd = inMpd;
        }

        public bool CanExecute(object parameter){
            return mpd.SelectedNote != null; ;
        }

        public async void Execute(object parameter){
            MessageDialog SaveDialog = new MessageDialog("Really (not actually) Save note? ");
            SaveDialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            SaveDialog.Commands.Add(new UICommand("No") { Id = 1 });
            SaveDialog.DefaultCommandIndex = 1;
            SaveDialog.CancelCommandIndex = 1;
            var result = await SaveDialog.ShowAsync();
        }

        public void FireCanExecuteChanged(){
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
