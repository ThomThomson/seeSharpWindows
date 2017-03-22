using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _3Note5MeTest
{
    [TestClass]
    public class MainPageDataTesting
    {
        [TestMethod]
        public void CreateMainPageData() {
            _3Note5Me.ViewModels.MainPageData mpd = new _3Note5Me.ViewModels.MainPageData();
            Assert.AreNotEqual(mpd, null, "Null MainPageData");
        }

        [TestMethod]
        public void TestNotesListAgainstFolderContents() {
            _3Note5Me.ViewModels.MainPageData mpd = new _3Note5Me.ViewModels.MainPageData();
            Task.Run(async () => {
                //await mpd.PopulateNotes();
                IReadOnlyList<StorageFile> files = (IReadOnlyList<StorageFile>)mpd.NotesFolder.GetFilesAsync();
                Assert.AreEqual(mpd.Notes.Count, files.Count, "All Txt Files in Folder NOT added to list");
            });
        }

        //Test adding a note
        [TestMethod]
        public void TestAddingANote() {
            _3Note5Me.ViewModels.MainPageData mpd = new _3Note5Me.ViewModels.MainPageData();
            Task.Run(async () => {
                //await mpd.AddNoteCommand.add("testNote");
                Assert.AreEqual(mpd.Notes[mpd.Notes.Count].Title, "testNote");
            });
        }

        [TestMethod]
        public void TestSelectingAddedNote() {
            _3Note5Me.ViewModels.MainPageData mpd = new _3Note5Me.ViewModels.MainPageData();
            Task.Run(async () => {
                //await mpd.AddNoteCommand.add("testNote");
                mpd.SelectedNote = mpd.Notes[mpd.Notes.Count];
                Assert.AreEqual(mpd.SelectedNote.Title, "testNote");
            });
        }

        [TestMethod]
        public void TestAddingThenDeletingNote() {
            _3Note5Me.ViewModels.MainPageData mpd = new _3Note5Me.ViewModels.MainPageData();
            Task.Run(async () => {
                //await mpd.AddNoteCommand.add("testNote");
                mpd.SelectedNote = mpd.Notes[mpd.Notes.Count];
                mpd.DelNoteCommand.RemoveNote();
                Assert.AreEqual(mpd.SelectedNote, null);
            });
        }

    }
}
