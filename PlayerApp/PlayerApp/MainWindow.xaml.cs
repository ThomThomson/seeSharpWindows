using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;

namespace PlayerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        bool playingMedia;
        bool movingScrubber;
        DispatcherTimer timer;

        //C O N S T R U C T O R
        public MainWindow(){
            InitializeComponent();
            btnPlay.IsEnabled = false;
            playingMedia = false;
            movingScrubber = false;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += ScrubberTick;
            timer.Start();
        }
        //S T O P
        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e) {e.CanExecute = playingMedia;}
        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e) {
            MediaPlayer.Stop();
            playingMedia = false;
            MediaPlayer.Source = null;
        }

        //M E T H O D S for M O V I N G through the media
        private void ScrubberTick(object sender, EventArgs e) {
            if ((MediaPlayer.Source != null) && (MediaPlayer.NaturalDuration.HasTimeSpan) && (!movingScrubber)) {
                TimeSlider.Minimum = 0;
                TimeSlider.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                TimeSlider.Value = MediaPlayer.Position.TotalSeconds;
            }
        }
        private void btnMoveBack_Click(object sender, RoutedEventArgs e){MediaPlayer.Position -= TimeSpan.FromSeconds(10);}
        private void btnMoveForward_Click(object sender, RoutedEventArgs e){MediaPlayer.Position += TimeSpan.FromSeconds(10);}
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            TimeText.Content = TimeSpan.FromSeconds(TimeSlider.Value).ToString(@"hh\:mm\:ss");
        }
        private void DragStarted(object sender, DragStartedEventArgs e) {movingScrubber = true;}
        private void DragCompleted(object sender, DragCompletedEventArgs e) {
            movingScrubber = false;
            MediaPlayer.Position = TimeSpan.FromSeconds(TimeSlider.Value);
        }

        //M E T H O D S for O P E N I N G files
        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e) { e.CanExecute = true; }
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e) {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Title = "Open Media";
            dialog.FileName = "Videos";
            dialog.DefaultExt = ".MP4";
            dialog.Filter = "Audio files (*.mp3)|*.mp3|Video files (.*.mpg;*.mpeg;*.mp4)|*.mpg;*.mpeg;*.mp4|All files (*.*)|*.*";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true) {
                MediaPlayer.Source = new Uri(dialog.FileName);
                btnPlay.IsEnabled = true;
            }
        }

        //M E T H O D S for P L A Y I N G the files.
        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e) {e.CanExecute = (MediaPlayer != null) && (MediaPlayer.Source != null);}
        private void Play_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (!playingMedia) {
                btnPlay.Content = "||";
                playingMedia = true;
                MediaPlayer.Play();
            }
            else {
                btnPlay.Content = ">";
                playingMedia = false;
                MediaPlayer.Pause();
            }
        }

        //A B O U T
        private void AboutMenuItem(object sender, RoutedEventArgs e) {
            //need a fucking dialog box
            MessageBox.Show("DEVON MAED DIS");
        }

        //E X I T
        private void ExitMenuItem(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }
}
