using POP.Model;
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
            InitializeComponent();
            this.akcija = akcija;
            this.operacija = operacija;
            dpPocetak.DataContext = akcija;
            dpKraj.DataContext = akcija;
            var listaNamestaja = Projekat.Instance.Namestaj;
            var filtLista = new List<Namestaj>();
            for (int i = 0; i < listaNamestaja.Count; i++)
                if (listaNamestaja[i].Obrisan == false)
                    filtLista.Add(listaNamestaja[i]);
            cbNamestaj.ItemsSource = filtLista;
            cbNamestaj.DataContext = akcija;
            tbPopust.DataContext = akcija;

        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Akcija> listaAkcija = Projekat.Instance.Akcija;
            var namestaj = (Namestaj)cbNamestaj.SelectedItem;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    akcija.Id = listaAkcija.Count + 1;
                    listaAkcija.Add(akcija);
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
                    break;

            }
            GenericSerializer.Serialize("akcije.xml", listaAkcija);
            Close();
        }
        
    }
}
