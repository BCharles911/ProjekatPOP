using POP.Model;
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
            this.namestaj = namestaj;
            this.operacija = operacija;

            cmbTipNamestaja.ItemsSource = Projekat.Instance.TipoviNamestaja;
            tbNaziv.DataContext = namestaj;
            cmbTipNamestaja.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            tbKolicina.DataContext = namestaj;
            
        }





        private void Izlaz(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSacuvaj_click(object sender, RoutedEventArgs e)
        {
            var listaNamestaja = Projekat.Instance.Namestaj;
          //  var izabraniTipNamestaja = (TipNamestaja) cmbTipNamestaja.SelectedItem;
            ObservableCollection<Namestaj> postojeciNamestaj = Projekat.Instance.Namestaj;


            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    try
                    {
                        namestaj.Id = postojeciNamestaj.Count + 1;
                        namestaj.Naziv = tbNaziv.Text;
                        namestaj.Cena = Convert.ToDouble(tbCena.Text);
                        namestaj.KolicinaUMagacinu = Convert.ToInt16(tbKolicina.Text);
                        //namestaj.TipNamestaja = (TipNamestaja)cmbTipNamestaja.SelectedItem;
                        postojeciNamestaj.Add(namestaj);
                    }
                    catch (Exception)
                    {
                        if (cmbTipNamestaja.SelectedItem == null)
                        {
                            MessageBox.Show("Ne mozete ostaviti prazna polja");
                        }
                       
                        
                    }
                        break;
                    
                case Operacija.IZMENA:
                    foreach (var n in postojeciNamestaj)
                    {
                        if (n.Id == namestaj.Id)
                        {
                            n.TipNamestajaId = namestaj.TipNamestajaId;
                            n.Naziv = namestaj.Naziv;
                            break;
                        }
                    }
                    break;

            }
            
            GenericSerializer.Serialize("namestaj.xml", postojeciNamestaj);
            Projekat.Instance.Namestaj = postojeciNamestaj;
            Close();

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
