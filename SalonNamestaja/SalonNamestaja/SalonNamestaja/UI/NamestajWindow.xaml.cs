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
    /// Interaction logic for NamestajWindow.xaml
    /// </summary>
    public partial class NamestajWindow : Window
    {
        public NamestajWindow()
        {
            InitializeComponent();

            OsveziPrikaz();
        }

        public void OsveziPrikaz()
        {
            ListBoxNamestaja.Items.Clear();
            foreach (var namestaj in Projekat.Instance.Namestaj)
            {
                ListBoxNamestaja.Items.Add(namestaj);
            }

            ListBoxNamestaja.SelectedIndex = 0;
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDodajNamestaj(object sender, RoutedEventArgs e)
        {
            var prazanNamestaj = new Namestaj()
            {
                
                Naziv = "",
                Cena = 0,
                KolicinaUMagacinu = 0

            };
            var namestajProzor = new DodajNamestaj(prazanNamestaj, DodajNamestaj.Operacija.DODAVANJE);

            namestajProzor.ShowDialog();
            OsveziPrikaz();

        }

        private void btnIzmeni(object sender, RoutedEventArgs e)
        {
            var izabraniNamestaj = (Namestaj)ListBoxNamestaja.SelectedItem;
            var namestajProzor = new DodajNamestaj(izabraniNamestaj, DodajNamestaj.Operacija.IZMENA);
            namestajProzor.ShowDialog();
            OsveziPrikaz();
        }
    }
}
