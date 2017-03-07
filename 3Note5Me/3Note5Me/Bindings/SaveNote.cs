using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using _3Note5Me.ViewModels;
using Windows.UI.Popups;
using Windows.Storage;
using Windows.Storage.Streams;

namespace _3Note5Me.Bindings
{
    class SaveNote : ICommand{
        public event EventHandler CanExecuteChanged;
        private MainPageData mpd;
        public SaveNote(MainPageData inMpd){
            mpd = inMpd;
        }

        public bool CanExecute(object parameter){
            return mpd.SelectedNote != null && !mpd.CurrentNoteReadOnly;
        }

        public async void Execute(object parameter) {
            save();
            MessageDialog SavedDialog = new MessageDialog("Saved!");
            SavedDialog.Commands.Add(new UICommand("OK") { Id = 0 });
            SavedDialog.DefaultCommandIndex = 0;
            await SavedDialog.ShowAsync();
        }

        public void FireCanExecuteChanged(){
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public async Task save() {
            using (StorageStreamTransaction storageStreamTransaction = await mpd.SelectedNote.File.OpenTransactedWriteAsync()) {
                using (DataWriter dataWriter = new DataWriter(storageStreamTransaction.Stream)) {
                    mpd.SelectedNote.Content = mpd.CurrentNoteContent;
                    dataWriter.WriteString(mpd.CurrentNoteContent);
                    storageStreamTransaction.Stream.Size = await dataWriter.StoreAsync();
                    await storageStreamTransaction.CommitAsync();
                }
            }
        }
    }
}
