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
    /// Interaction logic for RacuniProzor.xaml
    /// </summary>
    public partial class RacuniProzor : Window
    {
        CollectionViewSource cvs;
        ICollectionView view;
        public RacuniProzor()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();

            cvs = new CollectionViewSource();
            cvs.Source = Projekat.Instance.Racun1;

            view = cvs.View;
            dgRacuni.ItemsSource = view;
            dgRacuni.IsSynchronizedWithCurrentItem = true;
            dgRacuni.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            cmbKriterijum.Items.Add("Ime i prezime");
            cmbKriterijum.Items.Add("Broj racuna");
            cmbKriterijum.Items.Add("Prodat komad namestaja");
            cmbKriterijum.Items.Add("Datum prodaje");

            cmbNamestajZaPretragu.ItemsSource = Projekat.Instance.Namestaj1;


        }

        private void dgRacuni_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Usluge" | e.Column.Header.ToString() == "namestajStavke" | e.Column.Header.ToString() == "Stavke" | e.Column.Header.ToString() == "Id" | e.Column.Header.ToString() == "Obrisan" | e.Column.Header.ToString() == "strNamestaj")
            {
                e.Cancel = true;
            }

        }



        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Racun racun = new Racun();
            Racun.Dodaj(racun);
            DodavanjeIzmenaRacuna dir = new DodavanjeIzmenaRacuna(racun);
            dir.ShowDialog();

        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {

            Racun selektovani = view.CurrentItem as Racun;
            DodavanjeIzmenaRacuna dir = new DodavanjeIzmenaRacuna(selektovani, DodavanjeIzmenaRacuna.Operacija.IZMENA);
            dir.ShowDialog();

        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {

            Racun selektovani = view.CurrentItem as Racun;
            if (MessageBox.Show("Da li ste sigurni", "Potvrda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Racun.Obrisi(selektovani);
            }
        }


        private void Pretraga(object sender, FilterEventArgs e)
        {
            string x = cmbKriterijum.SelectedItem.ToString();
            string s = tbPretraga.Text.ToLower();
            Racun racun = e.Item as Racun;
            switch (x)
            {
                case "Broj racuna":
                    break;
                case "Datum prodaje":
                    e.Accepted = racun.DatumProdaje.ToString().ToLower().Contains(s);
                    break;
                case "Ime i prezime":
                    e.Accepted = racun.ImeKupca.ToString().ToLower().Contains(s);
                    break;
                default:
                    break;
            }
        }

        private void tbTekstPretrage_TextChanged(object sender, TextChangedEventArgs e)
        {
            cvs.Filter += new FilterEventHandler(Pretraga);
        }



        private void cmbKriterijumPretrage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbKriterijum.SelectedItem.ToString().Equals("Prodat komad namestaja"))
            {
                tbPretraga.Text = "";
                tbPretraga.Visibility = Visibility.Hidden;
                cmbNamestajZaPretragu.Visibility = Visibility.Visible;
            }
            else
            {
                view.Filter = null;
                tbPretraga.Visibility = Visibility.Visible;
                cmbNamestajZaPretragu.Visibility = Visibility.Hidden;
            }
        }

        private void cmbNamestajZaPretragu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cvs.Filter += new FilterEventHandler(pretragaNamestaj);
        }

        private void pretragaNamestaj(object sender, FilterEventArgs e)
        {
            Namestaj namestaj = cmbNamestajZaPretragu.SelectedItem as Namestaj;

            Racun racun = e.Item as Racun;
            e.Accepted = racun.strNamestaj.Contains(namestaj.Naziv);
        }




        private void tbTekstPretrage_TextChanged()
        {
            cvs.Filter += new FilterEventHandler(Pretraga);
        }

        private void btnOdjavi_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }
    }

}