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
    /// Interaction logic for DifficultyPage.xaml
    /// </summary>
    public partial class DifficultyPage : Page
    {
        public DifficultyPage(TimeSpan x)
        {
            InitializeComponent();
            mediaElement.Position = x;
            
        }

        private void ButtonNazad_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan x = new TimeSpan();
            x = mediaElement.Position;
            MainPage stranica = new MainPage(x);
            NavigationService.Navigate(stranica);
        }

        private void ButtonEz_Click(object sender, RoutedEventArgs e)
        {
            Igra stranica = new Igra(9);
            NavigationService.Navigate(stranica);
        }

        private void ButtonNorm_Click(object sender, RoutedEventArgs e)
        {
            Igra stranica = new Igra(7);
            NavigationService.Navigate(stranica);
        }

        private void ButtonHard_Click(object sender, RoutedEventArgs e)
        {
            Igra stranica = new Igra(5);
            NavigationService.Navigate(stranica);
        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = new TimeSpan();
        }
    }
}
