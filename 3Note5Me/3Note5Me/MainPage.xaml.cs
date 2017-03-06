using _3Note5Me.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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

        public MainPage(){
            this.InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) {
            CoreApplication.Exit();
        }

        private async void About_Click(object sender, RoutedEventArgs e) {
            MessageDialog AboutDialog = new MessageDialog("Give Devon 100% for this plz.");
            AboutDialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            AboutDialog.Commands.Add(new UICommand("No") { Id = 1 });
            AboutDialog.DefaultCommandIndex = 1;
            AboutDialog.CancelCommandIndex = 1;
            var result = await AboutDialog.ShowAsync();
        }
    }
}
