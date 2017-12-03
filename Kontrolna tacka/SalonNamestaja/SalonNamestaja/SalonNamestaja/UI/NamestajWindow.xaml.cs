using POP.Model;
using POP_SF12016.Model;
using Projekat2017.Model;
using Projekat2017.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        private ICollectionView view;
        private ICollectionView view2;
        private ICollectionView view3;
        private ICollectionView view4;
        private ICollectionView view5;
        private Korisnik ulogovaniKorisnik;
        public NamestajWindow()
        {
     

            InitializeComponent();
            this.ulogovaniKorisnik = ulogovaniKorisnik;
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);
            view2 = CollectionViewSource.GetDefaultView(Projekat.Instance.Korisnik);
            view3 = CollectionViewSource.GetDefaultView(Projekat.Instance.TipoviNamestaja);
            view4 = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatnaUsluga);
            view5 = CollectionViewSource.GetDefaultView(Projekat.Instance.Akcija);


            view.Filter = NamestajFilter;
            view2.Filter = KorisnikFilter;
            view3.Filter = TipNamestajaFilter;
            view4.Filter = UslugeFilter;
            view5.Filter = AkcijeFilter;

            //grid namestaja
            dgNamestaj.ItemsSource = view;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            //grid korisnika
            dgKorisnici.ItemsSource = view2;
            dgKorisnici.IsSynchronizedWithCurrentItem = true;
            dgKorisnici.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            //grid tipa namestaja
            dgTipNamestaja.ItemsSource = view3;
            dgTipNamestaja.IsSynchronizedWithCurrentItem = true;
            dgTipNamestaja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            dgUsluge.ItemsSource = view4;
            dgUsluge.IsSynchronizedWithCurrentItem = true;
            dgTipNamestaja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


            dgAkcije.ItemsSource = view5;
            dgAkcije.IsSynchronizedWithCurrentItem = true;
            dgAkcije.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            

        }

    

        private bool NamestajFilter(object item)
        {
            Namestaj nam = item as Namestaj;
            return !nam.Obrisan;
        }

        private bool KorisnikFilter(object item)
        {
            Korisnik kor = item as Korisnik;
            return !kor.Obrisan;
        }
        private bool TipNamestajaFilter(object item)
        {
            TipNamestaja tipN = item as TipNamestaja;
            return !tipN.Obrisan;
        }

        private bool UslugeFilter(object item)
        {
            DodatnaUsluga usluga = item as DodatnaUsluga;
            return !usluga.Obrisan;
        }
        
        private bool AkcijeFilter(object item)
        {
            Akcija akcije = item as Akcija;
            return !akcije.Obrisan;
        }




        //BUTTONI ZA DODAVANJE, IZMENU I BRISANJE NAMESTAJA


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
            

        }

        private void btnIzmeni(object sender, RoutedEventArgs e)
        {
            var izabraniNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            var namestajProzor = new DodajNamestaj(izabraniNamestaj, DodajNamestaj.Operacija.IZMENA);
            namestajProzor.ShowDialog();
            
        }

        private void btnObrisi_click(object sender, RoutedEventArgs e)
        {
            var NamestajZaBrisanje = (Namestaj)dgNamestaj.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {NamestajZaBrisanje.Naziv}", "Poruka o brisanju", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {

                {
                    var tipoviNam = Projekat.Instance.TipoviNamestaja;
                    var lista = Projekat.Instance.Namestaj;
                    foreach (var n in lista)
                    {
                        if (n.Id == NamestajZaBrisanje.Id)
                        {
                            n.Obrisan = true;
                            view.Refresh();


                        }
                 
                    }
                    GenericSerializer.Serialize("namestaj.xml", lista);
                }
               
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //BUTTONI ZA DODAVANJE, IZMENU I BRISANJE KORISNIKA

        private void btnDodajKorisnika_Click(object sender, RoutedEventArgs e)
        {
           
            var prazanKorisnik = new Korisnik()
            {

                Ime = "",
                Prezime = "",
                KorisnickoIme = "",
                Lozinka = ""

            };
            var dodajKorisnikaProzor = new DodajIzmeniKorisnika(prazanKorisnik, DodajIzmeniKorisnika.Operacija.DODAVANJE);

            dodajKorisnikaProzor.ShowDialog();

        }

        private void btn_izmeni2_Click(object sender, RoutedEventArgs e)
        {
            var izabraniKorisnik = (Korisnik)dgKorisnici.SelectedItem;
            var KorProzor = new DodajIzmeniKorisnika(izabraniKorisnik, DodajIzmeniKorisnika.Operacija.IZMENA);
            KorProzor.ShowDialog();
        }

        private void btnObrisiKorisnika_Click(object sender, RoutedEventArgs e)
        {

            var KorisnikZaBrisanje = (Korisnik)dgKorisnici.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {KorisnikZaBrisanje.KorisnickoIme}", "Poruka o brisanju", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                {
                    var lista = Projekat.Instance.Korisnik;
                    foreach (var k in lista)
                    {
                        if (k.Id == KorisnikZaBrisanje.Id)
                        {
                            k.Obrisan = true;
                            view2.Refresh();


                        }

                    }
                    GenericSerializer.Serialize("Korisnik.xml", lista);
                }

            }

        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //BUTTONI ZA DODAVANJE, IZMENU I BRISANJE TIPA NAMESTAJA




        private void btnDodajTipNam_Click(object sender, RoutedEventArgs e)
        {

            var prazanTipNamestaja = new TipNamestaja()
            {

                
                Naziv = ""
               

            };
            var tipNam = new DodajIzmeniTipNamestaja(prazanTipNamestaja, DodajIzmeniTipNamestaja.Operacija.DODAVANJE);

            tipNam.ShowDialog();



        }
        private void btnIzmeniTipNam_Click(object sender, RoutedEventArgs e)
        {

            var izabraniTipNamestaja = (TipNamestaja)dgTipNamestaja.SelectedItem;
            var tipnamProzor = new DodajIzmeniTipNamestaja(izabraniTipNamestaja, DodajIzmeniTipNamestaja.Operacija.IZMENA);
            tipnamProzor.ShowDialog();

        }
        private void btnObrisiTipNam_Click(object sender, RoutedEventArgs e)
        {
            var TipNamestajaZaBrisanje = (TipNamestaja)dgTipNamestaja.SelectedItem;
            
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {TipNamestajaZaBrisanje.Naziv}", "Poruka o brisanju", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                {
                    var lista = Projekat.Instance.TipoviNamestaja;
                    foreach (var t in lista)
                    {
                        if (t.Id == TipNamestajaZaBrisanje.Id)
                        {
                            t.Obrisan = true;
                            view3.Refresh();


                        }
                    }
                    GenericSerializer.Serialize("tip_namestaja.xml", lista);
                }

            }

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //BUTTONI ZA DODAVANJE, IZMENU I BRISANJE USLUGA





        private void btnDodajUsluge_Click(object sender, RoutedEventArgs e)
        {
            var prazneUsluge = new DodatnaUsluga()
            {

                Naziv = "",
                Cena = 0
                

            };
     

        }




        private void btnIzmeniUsluge_Click(object sender, RoutedEventArgs e)
        {
            var izabranaUsluga = (DodatnaUsluga)dgUsluge.SelectedItem;
            var uslugeProzor = new DodajIzmeniUslugu(izabranaUsluga, DodajIzmeniUslugu.Operacija.IZMENA);
            uslugeProzor.ShowDialog();
        }




        private void btnIzbrisiUsluge_Click(object sender, RoutedEventArgs e)
        {
            var UslugaZaBrisanje = (DodatnaUsluga)dgUsluge.SelectedItem;
            dgUsluge.SelectedIndex = 0;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {UslugaZaBrisanje.Naziv}", "Poruka o brisanju", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                {
                    var lista = Projekat.Instance.DodatnaUsluga;
                    foreach (var u in lista)
                    {
                        if (u.Id == UslugaZaBrisanje.Id)
                        {
                            u.Obrisan = true;
                            view4.Refresh();


                        }
                    }
                    GenericSerializer.Serialize("usluge.xml", lista);
                }

            }

        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //BUTTONI ZA DODAVANJE, IZMENU I BRISANJE AKCIJE




        private void btnDodajAkciju_Click(object sender, RoutedEventArgs e)
        {
            var praznaAkcija = new Akcija()
            {

                Popust = 0


            };
            var akcijeProzor = new DodavanjeIzmenaAkcija(praznaAkcija, DodavanjeIzmenaAkcija.Operacija.DODAVANJE);

            akcijeProzor.ShowDialog();
        }



        private void btnIzmeniAkciju_Click(object sender, RoutedEventArgs e)
        {

            var izabranaAkcija = (Akcija)dgAkcije.SelectedItem;
            var akcijeProzor = new DodavanjeIzmenaAkcija(izabranaAkcija, DodavanjeIzmenaAkcija.Operacija.IZMENA);
            akcijeProzor.ShowDialog();

        }

        private void btnObrisiAkciju_Click(object sender, RoutedEventArgs e)
        {
            var AkcijaZaBrisanje = (Akcija)dgAkcije.SelectedItem;
            dgAkcije.SelectedIndex = 0;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {AkcijaZaBrisanje.Id}", "Poruka o brisanju", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                {
                    var lista = Projekat.Instance.Akcija;
                    foreach (var a in lista)
                    {
                        if (a.Id == AkcijaZaBrisanje.Id)
                        {
                            a.Obrisan = true;
                            view5.Refresh();


                        }
                    }
                    GenericSerializer.Serialize("akcije.xml", lista);
                }

            }



        }





        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // AUTOGENERATINGCOLUMNS ZA SVE ENTITETE

        private void dgNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" | e.Column.Header.ToString() == "TipNamestajaId")
            {
                e.Cancel = true;
            }

        }

        private void dgAkcije_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id")
            {
                e.Cancel = true;
            }

        }

        private void dgKorisnici_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Lozinka" || e.Column.Header.ToString() == "KorisnickoIme")
            {
                e.Cancel = true;
            }
        }

        private void dgTipNamestaja_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id")
            {
                e.Cancel = true;
            }
        }

        private void dgUsluge_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id")
            {
                e.Cancel = true;
            }

        }






        private void Izlaz(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(1);
        }


    }
}
