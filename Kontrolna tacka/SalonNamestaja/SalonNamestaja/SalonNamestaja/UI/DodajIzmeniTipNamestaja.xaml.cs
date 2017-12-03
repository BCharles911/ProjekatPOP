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
    /// Interaction logic for DodajIzmeniTipNamestaja.xaml
    /// </summary>
    public partial class DodajIzmeniTipNamestaja : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        }
        private TipNamestaja tipnamestaja;
        private Operacija operacija;

        public DodajIzmeniTipNamestaja(TipNamestaja tipnamestaja, Operacija operacija)
        {
            InitializeComponent();
            this.tipnamestaja = tipnamestaja;
            this.operacija = operacija;

            tbNazivTipaNamestaja.DataContext = tipnamestaja;


        }





        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /*
        private void SacuvajIzmene(object sender, RoutedEventArgs e)
        {
            ObservableCollection<TipNamestaja> postojeciTipoviNamestaja = Projekat.Instance.TipoviNamestaja;

            switch (operacija) {
                case Operacija.DODAVANJE:

                    tipnamestaja.Id = postojeciTipoviNamestaja.Count + 1;
                    postojeciTipoviNamestaja.Add(tipnamestaja);
                    break;
                case Operacija.IZMENA:
                    foreach (var t in postojeciTipoviNamestaja)
                    {
                        if (t.Id == tipnamestaja.Id)
                        {
                            t.Naziv = tipnamestaja.Naziv;
                            break;
                        }
                    }
                    break;
            }
          
            Projekat.Instance.TipoviNamestaja = postojeciTipoviNamestaja;

            Close();
        }*/


        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            var listaTipNamestaja = Projekat.Instance.Namestaj;

            ObservableCollection<TipNamestaja> postojeciTipNamestaja = Projekat.Instance.TipoviNamestaja;


            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    try
                    {
                        tipnamestaja.Id = postojeciTipNamestaja.Count + 1;
                        tipnamestaja.Naziv = tbNazivTipaNamestaja.Text;
                        postojeciTipNamestaja.Add(tipnamestaja);
                    }catch (Exception)
                    {
                        if(String.IsNullOrEmpty(tbNazivTipaNamestaja.Text) )
                        {
                            MessageBox.Show("Ne mozete ostaviti prazno polje");
                        }
                    }
                    break;

                case Operacija.IZMENA:
                    foreach(var t in postojeciTipNamestaja)
                    {
                        if(t.Id == tipnamestaja.Id)
                        {
                            t.Naziv = tipnamestaja.Naziv;
                            break;
                        }
                    }
                    break;
            }
            GenericSerializer.Serialize("tip_namestaja.xml", postojeciTipNamestaja);
            Projekat.Instance.TipoviNamestaja = postojeciTipNamestaja;
            Close();
        }
        



    }
}
