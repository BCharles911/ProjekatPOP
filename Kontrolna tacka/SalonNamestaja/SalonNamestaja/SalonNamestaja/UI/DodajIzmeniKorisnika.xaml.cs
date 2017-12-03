using POP_SF12016.Model;
using Projekat2017.Model;
using Projekat2017.Util;
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
    /// Interaction logic for DodajIzmeniKorisnika.xaml
    /// </summary>
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
            InitializeComponent();

            this.korisnik = korisnik;
            this.operacija = operacija;

            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbKorisnickoIme.DataContext = korisnik;
            cmbTipKorisnika.ItemsSource = Enum.GetValues(typeof(TipKorisnika)).Cast<TipKorisnika>();
            cmbTipKorisnika.DataContext = korisnik;
            
            
            
            
            
            
            //        foreach(var TipKorisnika in Projekat.Instance.Korisnik)
       //     {
       //         cmbTipKorisnika.Items.Add(korisnik.tipkorisnika);
       //     }
        }
/*
        private void Potvrdi(object sender, RoutedEventArgs e)
        {
           
            ObservableCollection<Korisnik> postojeciKorisnik = Projekat.Instance.Korisnik;

            switch (operacija)
            {
                case Operacija.DODAVANJE:
                   
                    korisnik.Id = postojeciKorisnik.Count + 1;
                    postojeciKorisnik.Add(korisnik);
                    break;
                case Operacija.IZMENA:
                   
                    foreach ( var k in postojeciKorisnik)
                    {
                        if (k.Id == korisnik.Id)
                        {
                            k.Id = korisnik.Id;
                            k.Ime = korisnik.Ime;
                            k.Prezime = korisnik.Prezime;
                            k.KorisnickoIme = korisnik.KorisnickoIme;
                            k.Lozinka = korisnik.Lozinka;
                            break;
                        }
                    }
                    break;
            }
            GenericSerializer.Serialize("Korisnik.xml", postojeciKorisnik);
            Projekat.Instance.Korisnik = postojeciKorisnik;

            Close(); 
        }*/

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Korisnik> postojeciKorisnik = Projekat.Instance.Korisnik;

            switch (operacija)
            {
                case Operacija.DODAVANJE:
     
                    try
                    {
 
                        korisnik.Id = postojeciKorisnik.Count + 1;
                        korisnik.Ime = tbIme.Text;
                        korisnik.Prezime = tbPrezime.Text;
                        korisnik.KorisnickoIme = tbKorisnickoIme.Text;
                        korisnik.Lozinka = pbLozinka.Password.ToString();
                        postojeciKorisnik.Add(korisnik);
                    }
                    catch (Exception)
                    {
                        if(cmbTipKorisnika.SelectedItem == null || tbIme.Text == null || tbKorisnickoIme.Text == null || tbPrezime.Text == null || pbLozinka == null)
                        {
                            MessageBox.Show("Ne mozete ostaviti prazna polja");
                        }


                    }
                    break;

                case Operacija.IZMENA:
                    foreach(var k in postojeciKorisnik)
                    {
                        if (k.Id == korisnik.Id)
                        {
                            k.Ime = korisnik.Ime;
                            k.Prezime = korisnik.Prezime;
                            break;
                        }
                    }
                    break;


            }
            GenericSerializer.Serialize("Korisnik.xml", postojeciKorisnik);
            Projekat.Instance.Korisnik = postojeciKorisnik;

            Close();
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
