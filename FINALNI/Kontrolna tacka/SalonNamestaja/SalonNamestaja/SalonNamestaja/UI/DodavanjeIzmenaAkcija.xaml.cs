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
    



    public partial class DodavanjeIzmenaAkcija : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        }
        private Akcija akcija;
        private Operacija operacija;
        public DodavanjeIzmenaAkcija(Akcija akcija, Operacija operacija)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();


            this.akcija = akcija;
            this.operacija = operacija;

            
            
            dpPocetak.DataContext = akcija;
            dpKraj.DataContext = akcija;
            var listaNamestaja = Projekat.Instance.Namestaj1;
            var listaAkcija = Projekat.Instance.Akcija1;
            var akcLista = new List<Akcija>();
            var filtLista = new List<Namestaj>();
            for (int i = 0; i < listaNamestaja.Count; i++)
                if (listaNamestaja[i].Obrisan == false)
                    filtLista.Add(listaNamestaja[i]);
            cbNamestaj.ItemsSource = filtLista;
            cbNamestaj.DataContext = akcija;
            tbNaziv.DataContext = akcija;
            tbPopust.DataContext = akcija;

         /*   for (int a = 0; a < listaAkcija.Count; a++)
                if (listaAkcija[a].Obrisan == false)
                    akcLista.Add(listaAkcija[a]);
            cmbNazivAkcije.ItemsSource = akcLista;
            cmbNazivAkcije.DataContext = akcija; */

        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Akcija> listaAkcija = Projekat.Instance.Akcija1;
            var namestaj = (Namestaj)cbNamestaj.SelectedItem;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    Akcija.Dodaj(akcija);
                    break;
                case Operacija.IZMENA:
                    foreach(var a in listaAkcija)
                    {
                        if (a.Id == akcija.Id)
                        {
                            a.Popust = akcija.Popust;
                            break;
                        }
                    }
                    Akcija.Izmeni(akcija);
                    break;
                default:
                    break;

            }
            Close();
        }
        
    }
}
