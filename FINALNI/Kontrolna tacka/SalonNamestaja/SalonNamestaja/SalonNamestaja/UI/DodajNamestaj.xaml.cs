using SalonNamestaja.Model;
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
    /// Interaction logic for DodajNamestaj.xaml
    /// </summary>
    public partial class DodajNamestaj : Window
    {

        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        }
        private Namestaj namestaj;
        private Operacija operacija;
        public DodajNamestaj(Namestaj namestaj, Operacija operacija)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.namestaj = namestaj;
            this.operacija = operacija;

            cmbTipNamestaja.ItemsSource = Projekat.Instance.TipoviNamestaja;
            tbNaziv.DataContext = namestaj;
            cmbTipNamestaja.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            tbKolicina.DataContext = namestaj;
            cmbAkcije.ItemsSource = Projekat.Instance.Akcija1;
            cmbAkcije.DataContext = namestaj;
            
        }





        private void Izlaz(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSacuvaj_click(object sender, RoutedEventArgs e)
        {
            var listaNamestaja = Projekat.Instance.Namestaj1;
          //  var izabraniTipNamestaja = (TipNamestaja) cmbTipNamestaja.SelectedItem;
            ObservableCollection<Namestaj> postojeciNamestaj = Projekat.Instance.Namestaj1;


            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    Namestaj.Dodaj(namestaj);
                    break;
                    
                case Operacija.IZMENA:
                    foreach (var n in postojeciNamestaj)
                    {
                        if (n.Id == namestaj.Id)
                        {
                            n.TipNamestajaId = namestaj.TipNamestajaId;
                            n.Naziv = namestaj.Naziv;
                            n.Cena = namestaj.Cena;
                            n.KolicinaUMagacinu = namestaj.KolicinaUMagacinu;
                            n.TipNamestajaId = namestaj.TipNamestajaId;
                            break;
                        }
                    }
                    Namestaj.Izmeni(namestaj);
                    break;
                default:
                    break;
            }
            
            Close();

        }

        private void checkBoxAkcija_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbAkcije.IsEnabled = true;
            cmbAkcije.SelectedIndex = 0;
        }

        private void checkBoxAkcija_Checked(object sender, RoutedEventArgs e)
        {
            cmbAkcije.IsEnabled = false;
            cmbAkcije.SelectedItem = null;
            namestaj.Akcija = new Akcija();
        }
    }
}





/*    private void InicijalizujVrednosti(Namestaj namestaj, Operacija operacija)
    {
        this.operacija = operacija;
        this.namestaj = namestaj;
        tbNaziv.Text = namestaj.Naziv;
        tbCena.Text = Convert.ToString(namestaj.Cena);
        tbKolicina.Text = Convert.ToString(namestaj.KolicinaUMagacinu);
        foreach (var idNamestaja in Projekat.Instance.TipoviNamestaja)
        {
            cmbTipNamestaja.Items.Add(idNamestaja.Naziv);
        }




    }

*/
