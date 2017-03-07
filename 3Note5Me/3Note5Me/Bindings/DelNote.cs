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
    public class DelNote : ICommand{
        public event EventHandler CanExecuteChanged;
        private MainPageData mpd;
        public DelNote(MainPageData inMpd){
            mpd = inMpd;
        }

        public bool CanExecute(object parameter){
            return mpd.SelectedNote != null;
        }

        public async void Execute(object parameter){
            MessageDialog DelDialog = new MessageDialog("Delete this note? ");
            DelDialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            DelDialog.Commands.Add(new UICommand("No") { Id = 1 });
            DelDialog.DefaultCommandIndex = 1;
            DelDialog.CancelCommandIndex = 1;
            var result = await DelDialog.ShowAsync();
            if((int)result.Id == 0) {
                RemoveNote();
            }

        }

        public async void RemoveNote() {
            if (mpd.SelectedNote.File != null) {
                await mpd.SelectedNote.File.DeleteAsync();
            }
            mpd.Notes.Remove(mpd.SelectedNote);
            mpd.SelectedNote = null;
            mpd.Search();
        }

        public void FireCanExecuteChanged(){
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
