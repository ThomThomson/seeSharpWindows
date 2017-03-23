using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Windows.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;
using _3Note5Me.Model;
using Microsoft.EntityFrameworkCore;

namespace _3Note5MeTest
{
    
    [TestClass]
    public class MainPageDataTesting
    {
        [TestMethod]
        public void CreateMainPageData() {
            _3Note5Me.App app = new _3Note5Me.App();
            Assert.AreNotEqual(app, null, "Null MainPageData");
            //using (var dbNotes = new NoteContext()) {
            //    dbNotes.Database.Migrate();
            //    _3Note5Me.ViewModels.MainPageData mpd = new _3Note5Me.ViewModels.MainPageData();
                
            //}
        }

        [TestMethod]
        public void TestNotesListAgainstFolderContents() {
            //Tests unable to run due to the database not being created
            //I didn't have to run any tasks as async because the database operations aren't async
            List<Note> TestNoteList;
            using (var dbNotes = new NoteContext()) {
                dbNotes.Database.Migrate();
                TestNoteList = dbNotes.getNotes();
            }
            _3Note5Me.ViewModels.MainPageData mpd = new _3Note5Me.ViewModels.MainPageData();
            Assert.AreEqual(mpd.Notes.Count, TestNoteList.Count);
        }

        //Test adding a note
        [TestMethod]
        public void TestAddingANote() {
            //I didn't have to run any tasks as async because the database operations aren't async
            _3Note5Me.ViewModels.MainPageData mpd = new _3Note5Me.ViewModels.MainPageData();
            mpd.AddNoteCommand.add("testNote");
            Assert.AreEqual(mpd.Notes[mpd.Notes.Count].Title, "testNote");
        }

        [TestMethod]
        public void TestSelectingAddedNote() {
            _3Note5Me.ViewModels.MainPageData mpd = new _3Note5Me.ViewModels.MainPageData();
            mpd.AddNoteCommand.add("testNote");
            mpd.SelectedNote = mpd.Notes[mpd.Notes.Count];
            Assert.AreEqual(mpd.SelectedNote.Title, "testNote");
        }

        [TestMethod]
        public void TestAddingThenDeletingNote() {
            _3Note5Me.ViewModels.MainPageData mpd = new _3Note5Me.ViewModels.MainPageData();
            mpd.AddNoteCommand.add("testNote");
            mpd.SelectedNote = mpd.Notes[mpd.Notes.Count];
            mpd.DelNoteCommand.RemoveNote();
            Assert.AreEqual(mpd.SelectedNote, null);
        }

    }
}
