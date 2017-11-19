using POP.Model;
using Projekat2017.Model;
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
            InitializeComponent();
            InicijalizujVrednosti(namestaj, operacija);

        }
        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            List<Namestaj> postojeciNamestaj = Projekat.Instance.Namestaj;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    var noviNamestaj = new Namestaj()
                    {

                        Naziv = tbNaziv.Text,
                    };
                    postojeciNamestaj.Add(noviNamestaj);
                    break;
                case Operacija.IZMENA:
                    foreach (var n in postojeciNamestaj)
                    {
                        if (n.Id == namestaj.Id)
                        {
                            InicijalizujVrednosti(namestaj, operacija);

                            n.Naziv = tbNaziv.Text;
                            break;
                        }
                    }
                    break;

            }

            Projekat.Instance.Namestaj = postojeciNamestaj;
        }



        private void InicijalizujVrednosti(Namestaj namestaj, Operacija operacija)
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




        private void Izlaz(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSacuvaj_click(object sender, RoutedEventArgs e)
        {
            var listaNamestaja = Projekat.Instance.Namestaj;
 
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    
                    namestaj = new Namestaj()
                    {
                        Id = listaNamestaja.Count + 1,
                        Naziv = tbNaziv.Text,
                        Cena = Double.Parse(tbCena.Text),
                        TipNamestajaId = int.Parse(cmbTipNamestaja.SelectedIndex.ToString())

                    };
                    listaNamestaja.Add(namestaj);
                    break;


                    

                case Operacija.IZMENA:

                    var namestajZaIzmenu = listaNamestaja.SingleOrDefault(x => x.Id == namestaj.Id);

                    namestajZaIzmenu.Naziv = tbNaziv.Text;
                    namestajZaIzmenu.Cena = Double.Parse(tbCena.Text);
                    //namestajZaIzmenu.TipNamestajaId = cmbTipNamestaja
                    cmbTipNamestaja.IsReadOnly = true;
                    break;
                default:
                    break;
            }
            if (String.IsNullOrEmpty(tbId.Text) || String.IsNullOrEmpty(tbNaziv.Text) || String.IsNullOrEmpty(tbCena.Text) || String.IsNullOrEmpty(cmbTipNamestaja.Text))
            {
                MessageBox.Show("Sva polja su obavezna!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                foreach( var namestaj in listaNamestaja)
                if (int.Parse(tbId.Text) == namestaj.Id)
                {
                    MessageBox.Show("Uneli ste postojeci ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }
    }
}
