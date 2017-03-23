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
    public class SaveNote : ICommand {
        public event EventHandler CanExecuteChanged;
        private MainPageData mpd;
        public SaveNote(MainPageData inMpd) {
            mpd = inMpd;
        }

        public bool CanExecute(object parameter) {
            return mpd.SelectedNote != null;
        }

        public async void Execute(object parameter) {
            save();
            MessageDialog SavedDialog = new MessageDialog("Saved!");
            SavedDialog.Commands.Add(new UICommand("OK") { Id = 0 });
            SavedDialog.DefaultCommandIndex = 0;
            await SavedDialog.ShowAsync();
        }

        public void FireCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void save() {
            using (var dbNotes = new Model.NoteContext()) {
                mpd.SelectedNote.Content = mpd.CurrentNoteContent;
                var existingNote = dbNotes.Notes.Find(mpd.SelectedNote.id);
                if(existingNote != null) {
                    dbNotes.Entry(existingNote).CurrentValues.SetValues(mpd.SelectedNote);
                }else {
                    dbNotes.Notes.Add(mpd.SelectedNote);
                }
                dbNotes.SaveChanges();
            }
        }//E N D  M E T H O D  save()
    }
}
