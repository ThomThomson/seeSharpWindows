using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using _3Note5Me.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using Windows.Storage;

namespace _3Note5Me.Bindings
{
    public class AddNote : ICommand{
        public event EventHandler CanExecuteChanged;
        private MainPageData mpd;
        public AddNote(MainPageData inMpd){
            mpd = inMpd;
        }

        public bool CanExecute(object parameter){
            return true;
        }

        public async void Execute(object parameter){
            TextBox titleInput = new TextBox { Height = 32, AcceptsReturn = false };
            ContentDialog titleDialog = new ContentDialog() {
                Title = "Note Title",
                Content = titleInput,
                PrimaryButtonText = "Create",
                SecondaryButtonText = "Cancel"
            };
            ContentDialogResult result = await titleDialog.ShowAsync();
            bool nameMatched = false;
            if (result == ContentDialogResult.Primary) {
                foreach(Model.Note currentNote in mpd.Notes) {
                    if (titleInput.Text == currentNote.Title) {
                        nameMatched = true;
                        break;
                    }
                }
                if (!nameMatched) {
                    await add(titleInput.Text);
                } else {
                    MessageDialog duplicateNameDialog = new MessageDialog("A note with that title already exists.");
                    await duplicateNameDialog.ShowAsync();
                    Execute(parameter);
                }
            }

        }

        public async Task add(string inTitle) {
            Model.Note createdNote = new Model.Note(mpd.Notes.Count, inTitle);
            StorageFile newNoteFile = await mpd.NotesFolder.CreateFileAsync(createdNote.Title + ".txt");
            createdNote.File = newNoteFile;
            mpd.Notes.Add(createdNote);
            mpd.SelectedNote = createdNote;
            mpd.Search();
        }

        public void FireCanExecuteChanged(){
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
