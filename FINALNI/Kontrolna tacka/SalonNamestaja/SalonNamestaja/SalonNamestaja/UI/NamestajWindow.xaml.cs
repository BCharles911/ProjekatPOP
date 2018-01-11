using SalonNamestaja.Model;
using SalonNamestaja.Util;
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

        CollectionViewSource cvs1;
        CollectionViewSource cvs2;
        CollectionViewSource cvs3;
        CollectionViewSource cvs4;
        CollectionViewSource cvs5;


        private ICollectionView view;
        private ICollectionView view2;
        private ICollectionView view3;
        private ICollectionView view4;
        private ICollectionView view5;

        //public TipNamestaja IzabraniTipNamestaja { get; set; }
        private Korisnik ulogovaniKorisnik;
        public NamestajWindow()
        {

            cvs1 = new CollectionViewSource();
            cvs1.Source = Projekat.Instance.Namestaj1;

            cvs2 = new CollectionViewSource();
            cvs2.Source = Projekat.Instance.Korisnik1;

            cvs3 = new CollectionViewSource();
            cvs3.Source = Projekat.Instance.TipoviNamestaja;

            cvs4 = new CollectionViewSource();
            cvs4.Source = Projekat.Instance.DodatnaUsluga1;

            cvs5 = new CollectionViewSource();
            cvs5.Source = Projekat.Instance.Akcija1;


            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            //this.ulogovaniKorisnik = ulogovaniKorisnik;


            view = cvs1.View;
            view2 = cvs2.View;
            view3 = cvs3.View;
            view4 = cvs4.View;
            view5 = cvs5.View;


            // view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj1);
           // view2 = CollectionViewSource.GetDefaultView(Projekat.Instance.Korisnik1);
           // view3 = CollectionViewSource.GetDefaultView(Projekat.Instance.TipoviNamestaja);
          //  view4 = CollectionViewSource.GetDefaultView(Projekat.Instance.DodatnaUsluga1);
           // view5 = CollectionViewSource.GetDefaultView(Projekat.Instance.Akcija1);


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

            //grid usluga
            dgUsluge.ItemsSource = view4;
            dgUsluge.IsSynchronizedWithCurrentItem = true;
            dgUsluge.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            //grid akcija
            dgAkcije.ItemsSource = view5;
            dgAkcije.IsSynchronizedWithCurrentItem = true;
            dgAkcije.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);



            #region CMB DEO




            // USLUGE
            cmbPretragaUsluge.Items.Add("");
            cmbPretragaUsluge.Items.Add("Naziv");
            cmbPretragaUsluge.Items.Add("Cena");
            cmbSortiranjeUsluga.Items.Add("");
            cmbSortiranjeUsluga.Items.Add("Naziv");
            cmbSortiranjeUsluga.Items.Add("Cena");
            cmbPretragaUsluge.SelectedIndex = 0;
            cmbSortiranjeUsluga.SelectedIndex = 0;






            //Namestaj
            cmbPretragaNamestaja.Items.Add("");
            cmbPretragaNamestaja.Items.Add("Naziv");
            cmbPretragaNamestaja.Items.Add("Tip");
            cmbPretragaNamestaja.Items.Add("Cena");
            cmbPretragaNamestaja.Items.Add("Kolicina");

            cmbPretragaNamestaja.SelectedIndex = 0;
            var sortNamestaj = new List<string>() {"", "Naziv", "Cena", "Kolicina", "Tip namestaja" };
            cmbSortirajNamestaj.ItemsSource = sortNamestaj;
            cmbSortirajNamestaj.SelectedIndex = 0;


            // Korisnik
            var sortKorisnik = new List<string>() {"", "Ime", "Prezime", "Korisnicko ime" };
            cmbSortirajKorisnike.ItemsSource = sortKorisnik;
            cmbPretragaKorisnika.ItemsSource = sortKorisnik;
            cmbPretragaKorisnika.SelectedIndex = 0;
            cmbSortirajKorisnike.SelectedIndex = 0;

            //Tip Namestaja
            cmbSortiranjeTipaNamestaja.Items.Add("");
            cmbPretragaTipaNamestaja.Items.Add("");
            cmbSortiranjeTipaNamestaja.Items.Add("Naziv");
            cmbPretragaTipaNamestaja.Items.Add("Naziv");
            cmbSortiranjeTipaNamestaja.SelectedIndex = 0;
            cmbPretragaTipaNamestaja.SelectedIndex = 0;



            // Akcije
            var sortAkcije = new List<string>() {"", "Naziv", "Pocetak akcije", "Kraj akcije", "Popust" };
            cmbSortiranjeAkcija.ItemsSource = sortAkcije;
            cmbPretragaAkcija.ItemsSource = sortAkcije;
            cmbSortiranjeAkcija.SelectedIndex = 0;
            cmbPretragaAkcija.SelectedIndex = 0;

#endregion


        }

        #region pretraga!
        private void PretragaNamestaja(object sneder, FilterEventArgs e)
        {
            string a = cmbPretragaNamestaja.SelectedItem.ToString();
            string b = tbPretragaNamestaja.Text.ToLower();
            Namestaj namestaj = e.Item as Namestaj;
            switch (a)
            {
                case "Naziv":
                    e.Accepted = namestaj.Naziv.ToString().ToLower().Contains(b);
                    break;
                case "Tip":
                    e.Accepted = namestaj.TipNamestaja.Naziv.ToString().ToLower().Contains(b);
                    break;
                case "Cena":
                    e.Accepted = namestaj.Cena.ToString().Contains(b);
                    break;
                case "Kolicina":
                    e.Accepted = namestaj.KolicinaUMagacinu.ToString().Contains(b);
                    break;
                default:
                    break;
            }



        }



        private void PretragaKorisnika(object sender, FilterEventArgs e)
        {
            string a = cmbPretragaKorisnika.SelectedItem.ToString();
            string b = tbPretragaKorisnika.Text.ToLower();
            Korisnik korisnik = e.Item as Korisnik;
            switch (a)
            {
                case "Ime":
                    e.Accepted = korisnik.Ime.ToString().ToLower().Contains(b);
                    break;
                case "Prezime":
                    e.Accepted = korisnik.Prezime.ToString().ToLower().Contains(b);
                    break;
                case "Korisnicko ime":
                    e.Accepted = korisnik.KorisnickoIme.ToString().ToLower().Contains(b);
                    break;
                default:
                    break;
            }


        }
        

        private void PretragaATipaNamestaja(object sender, FilterEventArgs e)
        {
            string a = cmbPretragaTipaNamestaja.SelectedItem.ToString();
            string b = tbPretragaTipaNamestaja.Text.ToLower();
            TipNamestaja tipNamestaja = e.Item as TipNamestaja;
            switch (a)
            {
                case "Naziv":
                    e.Accepted = tipNamestaja.Naziv.ToString().ToLower().Contains(b);
                    break;
                default:
                    break;
            }
        }





        private void PretragaUsluge(object sender, FilterEventArgs e)
        {
            string a = cmbPretragaUsluge.SelectedItem.ToString();
            string b = tbPretragaUsluge.Text.ToLower();
            DodatnaUsluga usluga = e.Item as DodatnaUsluga;
            switch (a)
            {
                case "Naziv":
                    e.Accepted = usluga.Naziv.ToString().ToLower().Contains(b);
                    break;
                case "Cena":
                    e.Accepted = usluga.Cena.ToString().Contains(b);
                    break;
                default:
                    break;
            }
        }


        private void PretragaAkcije(object sender, FilterEventArgs e)
        {
            string a = cmbPretragaAkcija.SelectedItem.ToString();
            string b = tbPretragaAkcija.Text.ToLower();
            Akcija akcija = e.Item as Akcija;
            switch (a)
            {
                case "Naziv":
                    e.Accepted = akcija.Naziv.ToString().ToLower().Contains(b);
                    break;
                case "Pocetak akcije":
                    e.Accepted = akcija.PocetakAkcije.ToString().Contains(b);
                    break;
                case "Kraj akcije":
                    e.Accepted = akcija.KrajAkcije.ToString().Contains(b);
                    break;
                case "Popust":
                    e.Accepted = akcija.Popust.ToString().Contains(b);
                    break;
                default:
                    break;
            }
        }





        private void tbPretragaNamestaja_TextChanged(object sender, TextChangedEventArgs e)
        {
            cvs1.Filter += new FilterEventHandler(PretragaNamestaja);

        }



        private void tbPretragaKorisnika_TextChanged(object sender, TextChangedEventArgs e)
        {
            cvs2.Filter += new FilterEventHandler(PretragaKorisnika);
        }

        private void tbPretragaTipaNamestaja_TextChanged(object sender, TextChangedEventArgs e)
        {
            cvs3.Filter += new FilterEventHandler(PretragaATipaNamestaja);
        }


        private void tbPretragaUsluge_TextChanged(object sender, TextChangedEventArgs e)
        {
            cvs4.Filter += new FilterEventHandler(PretragaUsluge);
        }

        private void tbPretragaAkcija_TextChanged(object sender, TextChangedEventArgs e)
        {
            cvs5.Filter += new FilterEventHandler(PretragaAkcije);

        }






#endregion




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


            Namestaj kopija = (Namestaj)izabraniNamestaj.Clone();

            var namestajProzor = new DodajNamestaj(kopija, DodajNamestaj.Operacija.IZMENA);
            namestajProzor.ShowDialog();
            view.Refresh();
            
        }

        private void btnObrisi_click(object sender, RoutedEventArgs e)
        {
          

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete?", "Poruka o brisanju", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {

                var NamestajZaBrisanje = view.CurrentItem as Namestaj;
                Namestaj.Obrisi(NamestajZaBrisanje);
                view.Refresh();


            }
        }














        #region KORISNIK
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
                    var lista = Projekat.Instance.Korisnik1;
                    foreach (var k in lista)
                    {
                        if (k.Id == KorisnikZaBrisanje.Id)
                        {
                            Korisnik.Obrisi(k);
                            view2.Refresh();


                        }

                    }
                  
                }

            }

        }

        #endregion

        #region TIP NAMESTAJA

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

            TipNamestaja kopija = (TipNamestaja) izabraniTipNamestaja.Clone();

            
            var tipnamProzor = new DodajIzmeniTipNamestaja(kopija, DodajIzmeniTipNamestaja.Operacija.IZMENA);
            tipnamProzor.ShowDialog();
            view.Refresh();
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
                            TipNamestaja.Obrisi(t);
                            view3.Refresh();


                        }
                    }
                
                }

            }
            view.Refresh();

        }

        #endregion
    
        #region USLUGE


        private void btnDodajUsluge_Click(object sender, RoutedEventArgs e)
        {
            var prazneUsluge = new DodatnaUsluga()
            {

                Naziv = "",
                Cena = 0
                

            };
            var dodatneUsl = new DodajIzmeniUslugu(prazneUsluge, DodajIzmeniUslugu.Operacija.DODAVANJE);
            view4.Refresh();
            dodatneUsl.ShowDialog();



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
                    var lista = Projekat.Instance.DodatnaUsluga1;
                    foreach (var u in lista)
                    {
                        if (u.Id == UslugaZaBrisanje.Id)
                        {
                            DodatnaUsluga.Obrisi(u);
                            view4.Refresh();


                        }

                    }
                   

                }

            }
            
        }
        #endregion

        #region AKCIJA

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

            Akcija kopija = (Akcija)izabranaAkcija.Clone();

            var akcijeProzor = new DodavanjeIzmenaAkcija(kopija, DodavanjeIzmenaAkcija.Operacija.IZMENA);
            akcijeProzor.ShowDialog();

        }

        private void btnObrisiAkciju_Click(object sender, RoutedEventArgs e)
        {
            var AkcijaZaBrisanje = (Akcija)dgAkcije.SelectedItem;
            dgAkcije.SelectedIndex = 0;
            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete: {AkcijaZaBrisanje.Id}", "Poruka o brisanju", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                {
                    var lista = Projekat.Instance.Akcija1;
                    foreach (var a in lista)
                    {
                        if (a.Id == AkcijaZaBrisanje.Id)
                        {
                            Akcija.Obrisi(a);
                            view5.Refresh();


                        }
                    }
                 //   GenericSerializer.Serialize("akcije.xml", lista);
                }

            }



        }



        #endregion

        #region autogenerating Kolumne

        private void dgNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" | e.Column.Header.ToString() == "TipNamestajaId" | e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }

        }

        private void dgAkcije_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" | e.Column.Header.ToString() == "Obrisan" )
            {
                e.Cancel = true;
            }

        }

        private void dgKorisnici_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Lozinka"  || e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }
        }

        private void dgTipNamestaja_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }
        }

        private void dgUsluge_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" | e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }

        }


        #endregion



        private void Izlaz(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }

        private void cmbSortirajNamestaj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var namestaj = (string)cmbSortirajNamestaj.SelectedItem;
            if (namestaj != null)
            {
                switch (namestaj)
                {
                    case "":
                        cmbPretragaNamestaja.SelectedIndex = 0;
                        dgNamestaj.ItemsSource = view;
                        break;
                    case "Naziv":
                        dgNamestaj.ItemsSource = Projekat.Instance.Namestaj1.OrderBy(a => a.Naziv);
                        break;
                    case "Cena":
                        dgNamestaj.ItemsSource = Projekat.Instance.Namestaj1.OrderBy(a => a.Cena);
                        break;
                    case "Kolicina":
                        dgNamestaj.ItemsSource = Projekat.Instance.Namestaj1.OrderBy(a => a.KolicinaUMagacinu);
                        break;
                    case "Tip namestaja":
                        dgNamestaj.ItemsSource = Projekat.Instance.Namestaj1.OrderBy(a => a.TipNamestaja.Naziv);
                        break;
                }

            }


        }




        private void cmbSortirajKorisnike_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            var korisnik = (string)cmbSortirajKorisnike.SelectedItem;
            if (korisnik != null)
            {
                switch (korisnik)
                {
                    case "":
                        cmbPretragaKorisnika.SelectedIndex = 0;
                        dgKorisnici.ItemsSource = view2;
                        break;
                    case "Ime":
                        dgKorisnici.ItemsSource = Projekat.Instance.Korisnik1.OrderBy(a => a.Ime);
                        break;
                    case "Prezime":
                        dgKorisnici.ItemsSource = Projekat.Instance.Korisnik1.OrderBy(a => a.Prezime);
                        break;
                    case "Korisnicko ime":
                        dgKorisnici.ItemsSource = Projekat.Instance.Korisnik1.OrderBy(a => a.KorisnickoIme);
                        break;

                }



            }

        }



        private void cmbSortiranjeAkcija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            var akcija = (string)cmbSortiranjeAkcija.SelectedItem;
            if (akcija != null)
            {
                switch (akcija)
                {
                    case "":
                        cmbPretragaAkcija.SelectedIndex = 0;
                        dgAkcije.ItemsSource = view5;
                        break;
                    case "Naziv":
                        dgAkcije.ItemsSource = Projekat.Instance.Akcija1.OrderBy(a => a.Naziv);
                        break;
                    case "Pocetak akcije":
                        dgAkcije.ItemsSource = Projekat.Instance.Akcija1.OrderBy(a => a.PocetakAkcije);
                        break;
                    case "Kraj akcije":
                        dgAkcije.ItemsSource = Projekat.Instance.Akcija1.OrderBy(a => a.KrajAkcije);
                        break;
                    case "Popust":
                        dgAkcije.ItemsSource = Projekat.Instance.Akcija1.OrderBy(a => a.Popust);
                        break;
                    default:
                        break;
                }



            }



        }

        private void cmbSortiranjeTipaNamestaja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var tipNamestaja = (string)cmbSortiranjeTipaNamestaja.SelectedItem;
            if (tipNamestaja != null)
            {
                switch (tipNamestaja)
                {
                    case "":
                        cmbPretragaTipaNamestaja.SelectedIndex = 0;
                        dgTipNamestaja.ItemsSource = view3;
                        break;
                    case "Naziv":
                        dgTipNamestaja.ItemsSource = Projekat.Instance.TipoviNamestaja.OrderBy(a => a.Naziv);
                        break;
                    default:
                        break;
                }



            }


        }

        private void cmbSortiranjeUsluga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            var usluge = (string)cmbSortiranjeUsluga.SelectedItem;
            if (usluge != null)
            {
                switch (usluge)
                {
                    case "":
                        cmbPretragaUsluge.SelectedIndex = 0;
                        dgUsluge.ItemsSource = view4;
                        break;
                    case "Naziv":
                        dgUsluge.ItemsSource = Projekat.Instance.DodatnaUsluga1.OrderBy(a => a.Naziv);
                        break;
                    case "Cena":
                        dgUsluge.ItemsSource = Projekat.Instance.DodatnaUsluga1.OrderBy(a => a.Cena);
                        break;
                    default:
                        break;
                }



            }

        }
    }
}
