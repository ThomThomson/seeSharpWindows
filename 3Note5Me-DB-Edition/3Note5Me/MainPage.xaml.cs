using _3Note5Me.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _3Note5Me{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {
        MainPageData mpd;

        public MainPage() {
            this.InitializeComponent();
            mpd = (MainPageData)this.DataContext;
        }

        private void Exit_Click(object sender, RoutedEventArgs e) {
            CoreApplication.Exit();
        }

        private void About_Click(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(AboutPage));
        }

        private void OnTextChanged(object sender, RoutedEventArgs e) {
            ITextDocument document = NoteContent.Document;
            string documentContent;
            document.GetText(TextGetOptions.FormatRtf, out documentContent);
            if (mpd.SelectedNote != null) { mpd.SelectedNote.Content = documentContent; }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ITextDocument document = NoteContent.Document;
            if (mpd.SelectedNote != null) {
                NoteContent.IsReadOnly = false;
                if (mpd.SelectedNote.Content != null) {
                    mpd.CurrentNoteContent = mpd.SelectedNote.Content;
                    document.SetText(TextSetOptions.FormatRtf, mpd.CurrentNoteContent);
                } else {
                    document.SetText(TextSetOptions.FormatRtf, "");
                }
                NoteContent.IsReadOnly = true;
            } else {
                NoteContent.IsReadOnly = false;
                document = NoteContent.Document;
                document.SetText(TextSetOptions.FormatRtf, "");
                NoteContent.IsReadOnly = true;
            }
        }

        private void EditClick(object sender, RoutedEventArgs e) {
            NoteContent.IsReadOnly = !NoteContent.IsReadOnly;
        }

        private void BoldClick(object sender, RoutedEventArgs e) {
            Windows.UI.Text.ITextSelection selected = NoteContent.Document.Selection;
            if (selected != null && !NoteContent.IsReadOnly) {
                ITextCharacterFormat format = selected.CharacterFormat;
                format.Bold = Windows.UI.Text.FormatEffect.Toggle;
                selected.CharacterFormat = format;
            }
        }
        private void ItalicClick(object sender, RoutedEventArgs e) {
            Windows.UI.Text.ITextSelection selected = NoteContent.Document.Selection;
            if (selected != null && !NoteContent.IsReadOnly) {
                ITextCharacterFormat format = selected.CharacterFormat;
                format.Italic = Windows.UI.Text.FormatEffect.Toggle;
                selected.CharacterFormat = format;
            }
        }
        private void UnderlineClick(object sender, RoutedEventArgs e) {
            ITextSelection selected = NoteContent.Document.Selection;
            if (selected != null && !NoteContent.IsReadOnly) {
                ITextCharacterFormat format = selected.CharacterFormat;
                format.Underline = (format.Underline == UnderlineType.None) ? UnderlineType.Single : UnderlineType.None;
                selected.CharacterFormat = format;
            }
        }

        private void PrepSave(object sender, RoutedEventArgs e) {
            String newContent;
            NoteContent.Document.GetText(TextGetOptions.FormatRtf, out newContent);
            mpd.CurrentNoteContent = newContent;
        }
    }
}
