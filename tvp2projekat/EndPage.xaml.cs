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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace tvp2projekat
{
    /// <summary>
    /// Interaction logic for EndPage.xaml
    /// </summary>
    public partial class EndPage : Page
    {
        int status;
        int score;
        int gameMode;
        public EndPage(int i, double sc, int brojRedova)
        {
            InitializeComponent();
            status = i;
            score = (int)sc;
            gameMode = brojRedova;

            lblScore.Content = "Score: " + score;
            if (status == 0)
            {
                lblStatus.Content = "YOU LOSE";
                lblName.Visibility = Visibility.Collapsed;
                txtBoxName.Visibility = Visibility.Collapsed;
                btnOK.Visibility = Visibility.Collapsed;
                btnCancel.Content = "OK";
            }

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage stranica = new MainPage();
            NavigationService.Navigate(stranica);
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            //sacuvaj u fajl
            Skor novi = new Skor(txtBoxName.Text, score);
            List<Skor> nizSkorova = new List<Skor>();
            switch (gameMode)
            {
                case 9:
                    {
                        if (File.Exists("easyScores.bin"))
                        {
                            FileStream fs = new FileStream("easyScores.bin", FileMode.Open);
                            BinaryFormatter bf = new BinaryFormatter();
                            nizSkorova = (List<Skor>)bf.Deserialize(fs);
                            fs.Close();

                            nizSkorova.Add(novi);
                            FileStream fss = new FileStream("easyScores.bin", FileMode.Create);
                            bf.Serialize(fss, nizSkorova);
                            fss.Close();
                            break;
                        }
                        else
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            nizSkorova.Add(novi);
                            FileStream fss = new FileStream("easyScores.bin", FileMode.Create);
                            bf.Serialize(fss, nizSkorova);
                            fss.Close();
                            break;
                        }
                    }
                case 7:
                    {
                        if (File.Exists("normalScores.bin"))
                        {
                            FileStream fs = new FileStream("normalScores.bin", FileMode.Open);
                            BinaryFormatter bf = new BinaryFormatter();
                            nizSkorova = (List<Skor>)bf.Deserialize(fs);
                            fs.Close();

                            nizSkorova.Add(novi);
                            FileStream fss = new FileStream("normalScores.bin", FileMode.Create);
                            bf.Serialize(fss, nizSkorova);
                            fss.Close();
                            break;
                        }
                        else
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            nizSkorova.Add(novi);
                            FileStream fss = new FileStream("normalScores.bin", FileMode.Create);
                            bf.Serialize(fss, nizSkorova);
                            fss.Close();
                            break;
                        }
                    }
                case 5:
                    {
                        if (File.Exists("hardScores.bin"))
                        {
                            FileStream fs = new FileStream("hardScores.bin", FileMode.Open);
                            BinaryFormatter bf = new BinaryFormatter();
                            nizSkorova = (List<Skor>)bf.Deserialize(fs);
                            fs.Close();

                            nizSkorova.Add(novi);
                            FileStream fss = new FileStream("hardScores.bin", FileMode.Create);
                            bf.Serialize(fss, nizSkorova);
                            fss.Close();
                            break;
                        }
                        else
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            nizSkorova.Add(novi);
                            FileStream fss = new FileStream("hardScores.bin", FileMode.Create);
                            bf.Serialize(fss, nizSkorova);
                            fss.Close();
                            break;
                        }
                    }
            }
            MainPage stranica = new MainPage();
            NavigationService.Navigate(stranica);
        }

        private void textChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBoxName.Text.Trim().Equals(""))
                btnOK.IsEnabled = false;
            else
                btnOK.IsEnabled = true;
        }
    }
}
