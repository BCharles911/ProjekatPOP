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
            InitializeComponent();
            this.usluge = usluge;
            this.operacija = operacija;

            tbNazivUsluge.DataContext = usluge;
            tbCenaUsluge.DataContext = usluge;


        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            var listaUsluga = Projekat.Instance.DodatnaUsluga;

            ObservableCollection<DodatnaUsluga> postojeceUsluge = Projekat.Instance.DodatnaUsluga;

            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    try
                    {
                        usluge.Id = postojeceUsluge.Count + 1;
                        usluge.Naziv = tbNazivUsluge.Text;
                        usluge.Cena = Convert.ToDouble(tbCenaUsluge.Text);
                        postojeceUsluge.Add(usluge);
                    }
                    catch (Exception)
                    {
                        if (String.IsNullOrEmpty(tbNazivUsluge.Text) || String.IsNullOrEmpty(tbCenaUsluge.Text))
                        {
                            MessageBox.Show("Ne mozete ostaviti prazna polja");
                            return;
                        }
                    }
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
                    break;
            }
            GenericSerializer.Serialize("usluge.xml", postojeceUsluge);
            Projekat.Instance.DodatnaUsluga = postojeceUsluge;
            Close();
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
