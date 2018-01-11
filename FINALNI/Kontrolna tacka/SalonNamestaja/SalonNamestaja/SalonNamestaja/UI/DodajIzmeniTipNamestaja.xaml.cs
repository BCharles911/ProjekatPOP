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
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.tipnamestaja = tipnamestaja;
            this.operacija = operacija;

            tbNazivTipaNamestaja.DataContext = tipnamestaja;


        }





        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
      

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<TipNamestaja> postojeciTipNamestaja = Projekat.Instance.TipoviNamestaja;
           

            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    TipNamestaja.Dodaj(tipnamestaja);
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
                    TipNamestaja.Izmeni(tipnamestaja );
                    break;
                default:
                    break;
            }
            
            Close();
        }
        



    }
}
