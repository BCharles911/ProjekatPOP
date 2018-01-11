using SalonNamestaja.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for DodavanjeIzmenaRacuna.xaml
    /// </summary>
    public partial class DodavanjeIzmenaRacuna : Window
    {
        Racun racun;
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        }
        Operacija operacija;
        ICollectionView view1;
        ICollectionView view2;
        ICollectionView view11;
        ICollectionView view22;

        public DodavanjeIzmenaRacuna(Racun racun, Operacija operacija = Operacija.DODAVANJE)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            this.racun = racun;
            this.operacija = operacija;

            tbUkupnaCena.DataContext = racun;
            tbImeKupca.DataContext = racun;
            dpDatum.DataContext = racun;


            view1 = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj1);
            dgNamestaj.ItemsSource = view1;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            view2 = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatnaUsluga1);
            dgUsluge.ItemsSource = view2;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


            view11 = CollectionViewSource.GetDefaultView(racun.Stavke);
            dgDodatiNamestaj.ItemsSource = view11;
            dgDodatiNamestaj.IsSynchronizedWithCurrentItem = true;
            dgDodatiNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            view22 = CollectionViewSource.GetDefaultView(racun.Usluge);
            dgDodateUsluge.ItemsSource = view22;
            dgDodateUsluge.IsSynchronizedWithCurrentItem = true;
            dgDodateUsluge.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


            for (int i = 1; i < 10; i++)
            {
                cmbKolicina.Items.Add(i);

            }
            cmbKolicina.SelectedIndex = 0;


        }






  
        private void btnDodajNamestaj_Click(object sender, RoutedEventArgs e)
        {
            Namestaj selektovani = view1.CurrentItem as Namestaj;
            int kolicina = int.Parse(cmbKolicina.SelectedItem.ToString());

            if(kolicina > selektovani.KolicinaUMagacinu)
            {
                lblUpozorenje.Visibility = Visibility.Visible;
            }
            else
            {
                lblUpozorenje.Visibility = Visibility.Hidden;
                selektovani.KolicinaUMagacinu -= kolicina;

                foreach(var x in racun.Stavke)
                {
                    if (x.Namestaj.Id.Equals(selektovani.Id))
                    {
                        x.BrojKomada += kolicina;
                        
                        StavkaRacuna.izmeniZaRacun(racun, x);
                        
                        dgDodatiNamestaj.ItemsSource = racun.Stavke;
                        return;
                    }
                }
                Namestaj.Izmeni(selektovani);
                StavkaRacuna novaStavka = new StavkaRacuna(selektovani, kolicina);
                StavkaRacuna.Dodaj(racun, novaStavka);
                dgDodatiNamestaj.ItemsSource = racun.Stavke;
                return;
            

            }

        }

        private void btnUkloniNamestaj_Click(object sender, RoutedEventArgs e)
        {
            dgDodatiNamestaj.SelectedIndex = 0;
            int kolicina = int.Parse(cmbKolicina.SelectedItem.ToString());
  
            StavkaRacuna stavka = dgDodatiNamestaj.Items.CurrentItem as StavkaRacuna;


            if (kolicina > stavka.BrojKomada)
            {
                lblUpozorenje2.Visibility = Visibility.Visible;
            }
            else
            {
                lblUpozorenje2.Visibility = Visibility.Hidden;
                foreach(var namestaj in Projekat.Instance.Namestaj1)
                {
                    if (namestaj == stavka.Namestaj)
                    {
                        stavka.BrojKomada -= kolicina;
                        namestaj.KolicinaUMagacinu += kolicina;
                    }
                    Namestaj.Izmeni(namestaj);
                }
               
                StavkaRacuna.izmeniZaRacun(racun, stavka);
                
                dgDodatiNamestaj.ItemsSource = racun.Stavke;
            }
        }

        





        private void btnDodajUsl_Click(object sender, RoutedEventArgs e)
        {
            DodatnaUsluga selektovana = view2.CurrentItem as DodatnaUsluga;
            foreach(var usluga in racun.Usluge)
            {
                if (usluga.Id.Equals(selektovana.Id))
                {
                    return;
                }
            }

            DodatnaUsluga.DodajZaRacun(racun, selektovana);
            dgDodateUsluge.ItemsSource = racun.Usluge;
            tbUkupnaCena.DataContext = racun;

        }

        private void btnUkloniUslugu_Click(object sender, RoutedEventArgs e)
        {

            DodatnaUsluga selektovana = dgDodateUsluge.SelectedItem as DodatnaUsluga;
            DodatnaUsluga.IzbrisiZaRacun(racun, selektovana);
            dgDodateUsluge.ItemsSource = racun.Usluge;

        }



        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSacuvaj_Cllick(object sender, RoutedEventArgs e)
        {
            if (operacija == Operacija.DODAVANJE)
            {
                Projekat.Instance.Racun1.Add(racun);
               

            }
            Racun.Izmeni(racun);
            this.Close();
        }
    }
}
