using SalonNamestaja.Model;

using SalonNamestaja.Util;
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
    public partial class DodajIzmeniKorisnika : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        }

        private Korisnik korisnik;
        private Operacija operacija;
        public DodajIzmeniKorisnika(Korisnik korisnik, Operacija operacija)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            this.korisnik = korisnik;
            this.operacija = operacija;

            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbKorisnickoIme.DataContext = korisnik;
            cmbTipKorisnika.Items.Add(Korisnik.TipKorisnika.Administrator);
            cmbTipKorisnika.Items.Add(Korisnik.TipKorisnika.Prodavac);
            cmbTipKorisnika.DataContext = korisnik;

        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Korisnik> postojeciKorisnik = Projekat.Instance.Korisnik1;

            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    Korisnik.Dodaj(korisnik);
                    break;
     
                

                case Operacija.IZMENA:
                    foreach(var k in postojeciKorisnik)
                    {
                        if (k.Id == korisnik.Id)
                        {
                            k.Ime = korisnik.Ime;
                            k.Prezime = korisnik.Prezime;
                            k.KorisnickoIme = korisnik.KorisnickoIme;
                            k.Lozinka = korisnik.Lozinka;
                            
                            break;
                        }
                    }
                    Korisnik.Izmeni(korisnik);
                    break;
                default:
                    break;


            }
      
            Close();
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
