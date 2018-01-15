using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
    /// Interaction logic for HighscoresPage.xaml
    /// </summary>
    public partial class HighscoresPage : Page
    {
        List<Skor> nizSkorova;
        public HighscoresPage()
        {
            InitializeComponent();
            List<Skor> nizSkorova = new List<Skor>();

            if (File.Exists("easyScores.bin"))
            {
                FileStream fs = new FileStream("easyScores.bin", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                nizSkorova = (List<Skor>)bf.Deserialize(fs);
                fs.Close();

                SkorComparer sc = new SkorComparer();
                nizSkorova.Sort(sc);

                List<Skor> nasaLista = new List<Skor>();
                for (int i = 0; i < 10 && i < nizSkorova.Count; i++)
                {
                    nasaLista.Add(nizSkorova[i]);
                    nasaLista[i].pozicija = i + 1;
                }
                Tabela.ItemsSource = nasaLista;
                comboBox.SelectedIndex = 0;
            }

            mediaElement.Volume = MainPage.vol;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage stranica = new MainPage();
            NavigationService.Navigate(stranica);
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int x = comboBox.SelectedIndex;

            List<Skor> nizSkorova = new List<Skor>();
            switch (x)
            {
                case 0:
                    {
                        if (File.Exists("easyScores.bin"))
                        {
                            FileStream fs = new FileStream("easyScores.bin", FileMode.Open);
                            BinaryFormatter bf = new BinaryFormatter();
                            nizSkorova = (List<Skor>)bf.Deserialize(fs);
                            fs.Close();

                            SkorComparer sc = new SkorComparer();
                            nizSkorova.Sort(sc);

                            List<Skor> nasaLista = new List<Skor>();
                            for (int i = 0; i < 10 && i < nizSkorova.Count; i++)
                            {
                                nasaLista.Add(nizSkorova[i]);
                                nasaLista[i].pozicija = i + 1;
                            }

                            Tabela.ItemsSource = nasaLista;
                        }
                        break;
                    }
                case 1:
                    {
                        if (File.Exists("normalScores.bin"))
                        {
                            FileStream fs = new FileStream("normalScores.bin", FileMode.Open);
                            BinaryFormatter bf = new BinaryFormatter();
                            nizSkorova = (List<Skor>)bf.Deserialize(fs);
                            fs.Close();

                            SkorComparer sc = new SkorComparer();
                            nizSkorova.Sort(sc);

                            List<Skor> nasaLista = new List<Skor>();
                            for (int i = 0; i < 10 && i < nizSkorova.Count; i++)
                            {
                                nasaLista.Add(nizSkorova[i]);
                                nasaLista[i].pozicija = i + 1;
                            }

                            Tabela.ItemsSource = nasaLista;
                        }
                        break;
                    }
                case 2:
                    {
                        if (File.Exists("hardScores.bin"))
                        {
                            FileStream fs = new FileStream("hardScores.bin", FileMode.Open);
                            BinaryFormatter bf = new BinaryFormatter();
                            nizSkorova = (List<Skor>)bf.Deserialize(fs);
                            fs.Close();

                            SkorComparer sc = new SkorComparer();
                            nizSkorova.Sort(sc);

                            List<Skor> nasaLista = new List<Skor>();
                            for (int i = 0; i < 10 && i < nizSkorova.Count; i++)
                            {
                                nasaLista.Add(nizSkorova[i]);
                                nasaLista[i].pozicija = i + 1;
                            }

                            Tabela.ItemsSource = nasaLista;
                        }
                        break;
                    }
                default: break;

            }


        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = new TimeSpan();
        }
    }
}
