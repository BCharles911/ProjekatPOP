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
    /// Interaction logic for DodajIzmeniUslugu.xaml
    /// </summary>
    public partial class DodajIzmeniUslugu : Window
    {

        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        }
        private DodatnaUsluga usluge;
        private Operacija operacija;

        public DodajIzmeniUslugu(DodatnaUsluga usluge, Operacija operacija)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.usluge = usluge;
            this.operacija = operacija;

            tbNazivUsluge.DataContext = usluge;
            tbCenaUsluge.DataContext = usluge;


        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            var listaUsluga = Projekat.Instance.DodatnaUsluga1;

            ObservableCollection<DodatnaUsluga> postojeceUsluge = Projekat.Instance.DodatnaUsluga1;

            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    DodatnaUsluga.Dodaj(usluge);
                    break;


                case Operacija.IZMENA:
                    foreach(var u in postojeceUsluge)
                    {
                        if(u.Id == usluge.Id)
                        {
                            u.Naziv = usluge.Naziv;
                            break;
                        }
                    }
                    DodatnaUsluga.Izmeni(usluge);
                    break;
            }
            //GenericSerializer.Serialize("usluge.xml", postojeceUsluge);
           // Projekat.Instance.DodatnaUsluga1 = postojeceUsluge;
            Close();
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
