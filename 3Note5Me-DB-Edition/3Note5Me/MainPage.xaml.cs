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
    public sealed partial class MainPage : Page{
        MainPageData mpd;

        public MainPage(){
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
            mpd.CurrentNoteContent = documentContent;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ITextDocument document = NoteContent.Document;
            if (mpd.SelectedNote != null) {
                NoteContent.IsReadOnly = false;
                document.SetText(TextSetOptions.FormatRtf, mpd.CurrentNoteContent);
                //NoteContent.IsReadOnly = true;
                //boldButton.IsEnabled = false;
                //underlineButton.IsEnabled = false;
                //italicButton.IsEnabled = false;
            } else {
                NoteContent.IsReadOnly = false;
                document = NoteContent.Document;
                document.SetText(TextSetOptions.FormatRtf, "");
                //boldButton.IsEnabled = true;
                //underlineButton.IsEnabled = true;
                //italicButton.IsEnabled = true;
            }
        }
    }
}
