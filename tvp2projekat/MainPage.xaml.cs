using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tvp2projekat
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public static double vol = 0.5;
        public MainPage()
        {
            InitializeComponent();
            //VolumeSlider.Value = vol;

        }

        public MainPage(TimeSpan x)
        {
            InitializeComponent();
            //VolumeSlider.Value = vol;
            mediaElement.Position = x;

        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = new TimeSpan();
        }

        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan x = mediaElement.Position;
            DifficultyPage stranica = new DifficultyPage(x);
            NavigationService.Navigate(stranica);
        }

        private void ButtonHighscores_Click(object sender, RoutedEventArgs e)
        {
            HighscoresPage stranica = new HighscoresPage();
            NavigationService.Navigate(stranica);
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void valueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //vol = VolumeSlider.Value;
            mediaElement.Volume = vol;
        }
    }
}
