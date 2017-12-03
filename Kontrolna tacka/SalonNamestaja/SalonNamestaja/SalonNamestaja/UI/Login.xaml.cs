using POP_SF12016.Model;
using Projekat2017.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace SalonNamestaja.UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
        

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Korisnik> korisnik1 = Projekat.Instance.Korisnik;
            foreach (var korisnik in korisnik1)
            {
                if (tbKorIme.Text.Equals(korisnik.KorisnickoIme) && tbPass.Password.Equals(korisnik.Lozinka ) )
                {
                    
                    this.Hide();
                    var namestajProzor = new NamestajWindow();
                     
                    namestajProzor.ShowDialog();
                    
                }
                else
                {
                    if (String.IsNullOrEmpty(tbKorIme.Text) || String.IsNullOrEmpty(tbPass.Password))
                    {
                        MessageBox.Show("Niste uneli nista!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    
                        else
                        {
                           foreach (var korisnik2 in korisnik1)
                                while (tbKorIme.Text != korisnik2.KorisnickoIme || tbPass.Password != korisnik2.Lozinka)
                                {
                                    MessageBox.Show("Uneli ste nepostojece podatke", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                                }

                    }
                }
                   

            }

        }
    }
}
