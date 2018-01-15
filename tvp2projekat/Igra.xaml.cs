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
using System.Threading;

namespace tvp2projekat
{
    /// <summary>
    /// Interaction logic for Igra.xaml
    /// </summary>
    public partial class Igra : Page
    {
        int brojRedova;
        Grid gridPogodjenih;
        Image[,] pogodjeni;
        Grid gridPokusaja;
        Image[,] polja;
        Image[] zadateSlike;
        Image[,] pokusajiSlike;

        CancellationToken ct;
        CancellationTokenSource cs;


        int[] zadataKombinacija = { 0, 0, 0, 0 };
        int[] pokusajKombinacija = { 0, 0, 0, 0 };

        int zadatiBrojac = 0;

        int curRow;
        int curCol;

        double score;
        double vreme;

        bool winCondition;

        public Igra(int _gameMode)
        {
            InitializeComponent();
            brojRedova = _gameMode;
            postaviPogodjene();
            postaviPolja();
            zadajSlike();
            curRow = 0;
            curCol = 0;

            score = (brojRedova + 1) * 100;
            vreme = 100;

            BtnOK.IsEnabled = false;

            lblVreme.Content = "Vreme: " + vreme;

            cs = new CancellationTokenSource();
            ct = cs.Token;

            Task task = new Task(vremeTece);
            task.Start();

            winCondition = false;
           
        }

        private void vremeTece()
        {
            while (vreme > 0)
            {
                vreme--;
                System.Threading.Thread.Sleep(1000);
                this.Dispatcher.Invoke(() => { lblVreme.Content = "Vreme: " + vreme; });
                if (ct.IsCancellationRequested)
                {
                    return;
                }
            }
            if (winCondition == false)
            {
                score = 0;
                this.Dispatcher.Invoke(() => sakrijKontrole());
               
            }
        }

        private void zadajSlike()
        {
            Random r = new Random();
            zadateSlike = new Image[4];

            for (int i = 0; i < 4; i++)
            {

                zadateSlike[i] = new Image();
                int x = r.Next(6);
                switch (x)
                {
                    case 0: zadateSlike[i].Source = BitInit("Slike/Herc.png"); zadataKombinacija[zadatiBrojac] = x; zadatiBrojac++; break;
                    case 1: zadateSlike[i].Source = BitInit("Slike/Karo.png"); zadataKombinacija[zadatiBrojac] = x; zadatiBrojac++; break;
                    case 2: zadateSlike[i].Source = BitInit("Slike/Pik.png"); zadataKombinacija[zadatiBrojac] = x; zadatiBrojac++; break;
                    case 3: zadateSlike[i].Source = BitInit("Slike/Tref.png"); zadataKombinacija[zadatiBrojac] = x; zadatiBrojac++; break;
                    case 4: zadateSlike[i].Source = BitInit("Slike/Zvezda.png"); zadataKombinacija[zadatiBrojac] = x; zadatiBrojac++; break;
                    case 5: zadateSlike[i].Source = BitInit("Slike/Grom.png"); zadataKombinacija[zadatiBrojac] = x; zadatiBrojac++; break;
                    default: break;
                }
            }
            zadatiBrojac = 0;
        }

        private void postaviPolja()
        {
            pokusajiSlike = new Image[brojRedova, 4];
            gridPokusaja = new Grid();

            for (int i = 0; i < brojRedova; i++)
            {
                gridPokusaja.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < 4; i++)
            {
                gridPokusaja.ColumnDefinitions.Add(new ColumnDefinition());
            }
            polja = new Image[brojRedova, 4];
            for (int i = 0; i < brojRedova; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    polja[i, j] = new Image();
                    pokusajiSlike[i, j] = new Image();
                    pokusajiSlike[i, j].Margin = new Thickness(1);
                    polja[i, j].Margin = new Thickness(1);
                    polja[i, j].Source = BitInit("Slike/Polje.png");

                    Grid.SetRow(polja[i, j], i);
                    Grid.SetColumn(polja[i, j], j);
                    gridPokusaja.Children.Add(polja[i, j]);

                }
            }
            Grid.SetRow(gridPokusaja, 0);
            Grid.SetColumn(gridPokusaja, 0);
            GridPanel.Children.Add(gridPokusaja);
        }

        private void postaviPogodjene()
        {
            gridPogodjenih = new Grid();
            for (int i = 0; i < brojRedova; i++)
            {
                gridPogodjenih.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < 4; i++)
            {
                gridPogodjenih.ColumnDefinitions.Add(new ColumnDefinition());
            }
            pogodjeni = new Image[brojRedova, 4];
            for (int i = 0; i < brojRedova; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    pogodjeni[i, j] = new Image();
                    pogodjeni[i, j].Source = BitInit("Slike/PogodjenoPrazno.png");

                    Grid.SetRow(pogodjeni[i, j], i);
                    Grid.SetColumn(pogodjeni[i, j], j);
                    gridPogodjenih.Children.Add(pogodjeni[i, j]);

                }
            }
            Grid.SetRow(gridPogodjenih, 0);
            Grid.SetColumn(gridPogodjenih, 1);
            GridPanel.Children.Add(gridPogodjenih);
        }

        private BitmapImage BitInit(string url)
        {
            BitmapImage bit = new BitmapImage();
            bit.BeginInit();
            bit.UriSource = new Uri(url, UriKind.RelativeOrAbsolute);
            bit.EndInit();
            return bit;
        }



        private void HercKlik(object sender, MouseButtonEventArgs e)
        {
            if (curCol < 4)
            {

                pokusajiSlike[curRow, curCol].Source = BitInit("Slike/Herc.png");
                Grid.SetRow(pokusajiSlike[curRow, curCol], curRow);
                Grid.SetColumn(pokusajiSlike[curRow, curCol], curCol);
                gridPokusaja.Children.Add(pokusajiSlike[curRow, curCol]);
                curCol++;
                if (curCol == 4)
                    BtnOK.IsEnabled = true;
                pokusajKombinacija[zadatiBrojac] = 0; zadatiBrojac++;
            }
        }

        private void KaroKlik(object sender, MouseButtonEventArgs e)
        {
            if (curCol < 4)
            {

                pokusajiSlike[curRow, curCol].Source = BitInit("Slike/Karo.png");
                Grid.SetRow(pokusajiSlike[curRow, curCol], curRow);
                Grid.SetColumn(pokusajiSlike[curRow, curCol], curCol);
                gridPokusaja.Children.Add(pokusajiSlike[curRow, curCol]);
                curCol++;
                if (curCol == 4)
                    BtnOK.IsEnabled = true;
                pokusajKombinacija[zadatiBrojac] = 1; zadatiBrojac++;
            }
        }

        private void PikKlik(object sender, MouseButtonEventArgs e)
        {
            if (curCol < 4)
            {
                pokusajiSlike[curRow, curCol].Source = BitInit("Slike/Pik.png");
                Grid.SetRow(pokusajiSlike[curRow, curCol], curRow);
                Grid.SetColumn(pokusajiSlike[curRow, curCol], curCol);
                gridPokusaja.Children.Add(pokusajiSlike[curRow, curCol]);
                curCol++;
                if (curCol == 4)
                    BtnOK.IsEnabled = true;
                pokusajKombinacija[zadatiBrojac] = 2; zadatiBrojac++;
            }
        }

        private void TrefKlik(object sender, MouseButtonEventArgs e)
        {
            if (curCol < 4)
            {
                pokusajiSlike[curRow, curCol].Source = BitInit("Slike/Tref.png");
                Grid.SetRow(pokusajiSlike[curRow, curCol], curRow);
                Grid.SetColumn(pokusajiSlike[curRow, curCol], curCol);
                gridPokusaja.Children.Add(pokusajiSlike[curRow, curCol]);
                curCol++;
                if (curCol == 4)
                    BtnOK.IsEnabled = true;
                pokusajKombinacija[zadatiBrojac] = 3; zadatiBrojac++;
            }

        }

        private void ZvezdaKlik(object sender, MouseButtonEventArgs e)
        {
            if (curCol < 4)
            {
                pokusajiSlike[curRow, curCol].Source = BitInit("Slike/Zvezda.png");
                Grid.SetRow(pokusajiSlike[curRow, curCol], curRow);
                Grid.SetColumn(pokusajiSlike[curRow, curCol], curCol);
                gridPokusaja.Children.Add(pokusajiSlike[curRow, curCol]);
                curCol++;
                if (curCol == 4)
                    BtnOK.IsEnabled = true;
                pokusajKombinacija[zadatiBrojac] = 4; zadatiBrojac++;
            }
        }

        private void GromKlik(object sender, MouseButtonEventArgs e)
        {
            if (curCol < 4)
            {
                pokusajiSlike[curRow, curCol].Source = BitInit("Slike/Grom.png");
                Grid.SetRow(pokusajiSlike[curRow, curCol], curRow);
                Grid.SetColumn(pokusajiSlike[curRow, curCol], curCol);
                gridPokusaja.Children.Add(pokusajiSlike[curRow, curCol]);
                curCol++;
                if (curCol == 4)
                    BtnOK.IsEnabled = true;
                pokusajKombinacija[zadatiBrojac] = 5; zadatiBrojac++;
            }
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            zadatiBrojac = 0;
            for (int i = 0; i < 4; i++)
            {

                gridPokusaja.Children.Remove(pokusajiSlike[curRow, i]);
                pokusajiSlike[curRow, i].Source = null;
                curCol = 0;
                BtnOK.IsEnabled = false;
            }
        }

        private void BtnOKClick(object sender, RoutedEventArgs e)
        {
            bool[] naMestu1 = { false, false, false, false };
            bool[] naMestu2 = { false, false, false, false };
            int brNaMestu = 0;
            int brVanMesta = 0;
            //nalazenje crvenih + zakljucavanje
            for (int i = 0; i < 4; i++)
            {
                if (pokusajKombinacija[i] == zadataKombinacija[i])
                {
                    brNaMestu++;
                    naMestu1[i] = true;
                    naMestu2[i] = true;
                }
            }
            //nalazenje zutih + zakljucavanje
            for (int i = 0; i < 4; i++)
            {
                if (naMestu1[i] == false)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (pokusajKombinacija[i] == zadataKombinacija[j] && i != j && naMestu2[j] == false)
                        {
                            brVanMesta++;
                            naMestu1[i] = true;
                            naMestu2[j] = true;
                            break;

                        }
                    }
                }
            }
            //postavljanje krugova
            for (int i = 0; i < brNaMestu; i++)
            {
                pogodjeni[curRow, i].Source = BitInit("Slike/PogodjenoNaMestu.png");
            }
            for (int i = brNaMestu; i < brNaMestu + brVanMesta; i++)
            {
                pogodjeni[curRow, i].Source = BitInit("Slike/PogodjenoVanMesta.png");
            }
            //ako nije pogodjeno idemo dalje,ubaciti lose condition
            zadatiBrojac = 0;
            curRow++;
            curCol = 0;
            BtnOK.IsEnabled = false;
            //ubaciti win condition
            if (brNaMestu == 4)  // WIN
            {
                for (int i = 0; i < 4; i++)
                {
                    Grid.SetColumn(zadateSlike[i], i);
                    GridKraj.Children.Add(zadateSlike[i]);

                }
                winCondition = true;
                sakrijKontrole();
                score = (score * (1.0 * vreme / 100 + 1));
               
            }
            if (curRow == brojRedova && brNaMestu != 4) // LOSE
            {
                for (int i = 0; i < 4; i++)
                {
                    Grid.SetColumn(zadateSlike[i], i);
                    GridKraj.Children.Add(zadateSlike[i]);

                }
                score = 0;
                sakrijKontrole();
                
            }
            score = score - 100;

        }

        private void Win()
        {
            EndPage stranica = new EndPage(1, score, brojRedova);
            NavigationService.Navigate(stranica);
        }

        private void Lose()
        {
            EndPage stranica = new EndPage(0, 0, brojRedova);
            NavigationService.Navigate(stranica);
        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            Win();
        }

        private void mediaElement1_MediaEnded(object sender, RoutedEventArgs e)
        {
            Lose();
        }

        private void sakrijKontrole()
        {
            BtnOK.Visibility = Visibility.Collapsed;
            BtnDelete.Visibility = Visibility.Collapsed;
            lblGG.Visibility = Visibility.Visible;
            ContinueBtn.Visibility = Visibility.Visible;

            Slika0.Visibility = Visibility.Hidden;
            Slika1.Visibility = Visibility.Hidden;
            Slika2.Visibility = Visibility.Hidden;
            Slika3.Visibility = Visibility.Hidden;
            Slika4.Visibility = Visibility.Hidden;
            Slika5.Visibility = Visibility.Hidden;
            cs.Cancel();
        }

        private void ContinueBtnClick(object sender, RoutedEventArgs e)
        {
            if (winCondition == true)
            {
                Win();
            }
            else
            {
                Lose();
            }
        }
    }
}
