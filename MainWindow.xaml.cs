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
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using ZTP.Budowniczy;
using ZTP.Dekorator;
using ZTP.Pizza;

namespace ZTP
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Hand = "hand";
        public string RozmiarPizzy = "zaden";
        public bool RobionaPizza = false;
        public bool PokrojonaPizza = false;
        public decimal ZAROBKI = 0;
        public DispatcherTimer TIMERPIEC;
        public DispatcherTimer TIMERZAMOWIENIE;
        public TimeSpan CZASZAMOWIENIE;
        public TimeSpan CZASPIEC;
        public Line LINE;
        public PizzaBazowa pizza;
        public PizzaBazowa zamowienie;

        IBudowniczy budowniczyMalaPizza = new BudowniczyMalaPizza();
        IBudowniczy budowniczySredniaPizza = new BudowniczySredniaPizza();
        IBudowniczy budowniczyDuzaPizza = new BudowniczyDuzaPizza();

        Gra dyrektor = new Gra();

        #region FUNKCJE POMOCNICZE

        public MainWindow()
        {
            InitializeComponent();
            CZASZAMOWIENIE = TimeSpan.FromSeconds(59);
            TIMERZAMOWIENIE = new DispatcherTimer(new TimeSpan(0,0,1), DispatcherPriority.Normal, TIMERZAMOWIENIE_TICK, Application.Current.Dispatcher);
            TIMERZAMOWIENIE.Start();
            WyczyscStanowisko();
            Cursor = Cursors.None;
            GenerujZamowienie();
        }
        void TIMERZAMOWIENIE_TICK(object sender, EventArgs e) /* funckja zarzadzajaca glownym czasem */
        {
            string czasText = CZASZAMOWIENIE.Minutes.ToString() + ":";
            if (CZASZAMOWIENIE.Seconds < 10) czasText += "0";
            TimerGraLabel.Content = czasText + CZASZAMOWIENIE.Seconds.ToString();
            if (CZASZAMOWIENIE == TimeSpan.Zero)
            {
                Hand = "hand";
                ZmienKursor(70, 100, "hand");
                PodsumowanieOpis.Content = "     Znow pracowales zbyt wolno!";
                PodsumowanieWypiek.Content = "Klient zrezygnowal z zamowienia!";
                PodsumowanieCiecie.Content = "         Wez sie wreszcie do roboty!";
                PodsumowanieCena.Content = "Pracuj dalej";
                Podsumowanie.Visibility = Visibility.Visible;
                TIMERZAMOWIENIE.Stop();
            }
            CZASZAMOWIENIE = CZASZAMOWIENIE.Add(TimeSpan.FromSeconds(-1));
        }
        void TIMERPIEC_TICK(object sender, EventArgs e) /* funkcja zarzadzajaca czasem pieca */
        {
            string czasText = CZASPIEC.Minutes.ToString() + ":";
            if (CZASPIEC.Seconds < 10) czasText += "0";
            TimerPiecaLabel.Content = czasText + CZASPIEC.Seconds.ToString();
            CZASPIEC = CZASPIEC.Add(TimeSpan.FromSeconds(1));
        }
        private void GenerujZamowienie() /* funkcja generujaca zamowienie */
        {
            Random rnd = new Random();
            switch(rnd.Next(3))
            {
                case 0: dyrektor.StworzPizze(budowniczyMalaPizza); break;
                case 1: dyrektor.StworzPizze(budowniczySredniaPizza); break;
                case 2: dyrektor.StworzPizze(budowniczyDuzaPizza); break;
            }
            zamowienie = dyrektor.GetPizza();

            zamowienie.Pieczenie(new TimeSpan(0, 0, rnd.Next(10, 12)));
            zamowienie.Pieczenie(new TimeSpan(0, 0, rnd.Next(10, 12)));

            for (int i = 0; i < 5; i++) switch (rnd.Next(4))
            {
                case 0: if (!zamowienie.GetOpis().Contains("sos")) zamowienie = new Sos(zamowienie); break;
                case 1: if (!zamowienie.GetOpis().Contains("ser")) zamowienie = new Ser(zamowienie); break;
                case 2: if (!zamowienie.GetOpis().Contains("pieczarki")) zamowienie = new Pieczarki(zamowienie); break;
                case 3: if (!zamowienie.GetOpis().Contains("salami")) zamowienie = new Salami(zamowienie); break;
            }

            ZamowienieLabel.Text = "Zamowienie:    (" + zamowienie.GetCena() + "$)     " + zamowienie.GetWypiek() + " " + zamowienie.GetOpis();
        }
        private void WyczyscStanowisko() /* funkcja czyszczaca stanowisko pracy */
        {
            RobionaPizza = false;
            RozmiarPizzy = "zaden";
            Pizza.Children.Clear();
            noz.Visibility = Visibility.Visible;
            Ciecia.Visibility = Visibility.Hidden;
            walek.Visibility = Visibility.Visible;
            Wymiary.Visibility = Visibility.Hidden;
            Pizza.Visibility = Visibility.Hidden;
            PizzaBorder.Visibility = Visibility.Hidden;
            Stolik.Visibility = Visibility.Hidden;
            TimerPiecaLabel.Visibility = Visibility.Hidden;
            AktualnyOpis.Text = "";
            Stolik.Width = 100;
            Stolik.Height = 100;
            ZmienStopienWypieczenia("ciasto_0");
            CZASPIEC = TimeSpan.FromSeconds(2);
            TimerPiecaLabel.Content = "0:01";
        }
        private void ZmienKursor(int W, int H, string name) /* funkcja zmieniajaca grafike kursora */
        {
            BitmapImage bi3 = new BitmapImage(new Uri("Images/" + name + ".png", UriKind.Relative));
            customPointer.Stretch = Stretch.Fill;
            customPointer.Source = bi3;
            customPointer.Width = W;
            customPointer.Height = H;
            Hand = name;
        }
        private void ZmienStopienWypieczenia(string name) /* funkcja zmieniajaca grafike upieczenia */
        {
            BitmapImage bi3 = new BitmapImage(new Uri("Images/" + name + ".png", UriKind.Relative));
            Stolik.Stretch = Stretch.Fill;
            Stolik.Source = bi3;
        }
        private void UstawWidokPizzy() /* funkcja realizujaca widok pizzy po wziaciu ciasta */
        {
            Pizza.Visibility = Visibility.Visible;
            PizzaBorder.Visibility = Visibility.Visible;
            PizzaBorder.Width = Stolik.Width-60;
            PizzaBorder.Height = Stolik.Height-60;
        }
        private bool PizzaJest(bool tak = true) /* funkcja sprawdzajaca poprawnosc warunkow */
        {
            if (tak) { if (RozmiarPizzy != "zaden" && Hand == "hand" && Stolik.Visibility == Visibility.Visible) return true; }
            else { if (RozmiarPizzy == "zaden" && Hand == "hand" && Stolik.Visibility == Visibility.Visible) return true; }
            return false;
        }
        private void OK(object sender, RoutedEventArgs e) /* funkcja zatwierdzajaca zrozumienie powiadomienia */
        {
            GenerujZamowienie();
            TimerGraLabel.Content = "1:00";
            CZASZAMOWIENIE = TimeSpan.FromSeconds(59);
            TIMERZAMOWIENIE = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, TIMERZAMOWIENIE_TICK, Application.Current.Dispatcher);
            TIMERZAMOWIENIE.Start();
            Podsumowanie.Visibility = Visibility.Hidden;
            ZarobkiLabel.Content = ZAROBKI + " $";
            WyczyscStanowisko();
        }

        #endregion

        #region PODNOSZENIE I ODKLADANIE PRZEDMIOTÓW

        private void WezSosPomidorowy(object sender, RoutedEventArgs e)
        {
            if (PizzaJest()) { ZmienKursor(51, 160, "sos"); UstawWidokPizzy(); }
        }
        private void WezSalami(object sender, RoutedEventArgs e)
        {
            if (PizzaJest()) { ZmienKursor(60, 60, "salami"); UstawWidokPizzy(); }
        }
        private void WezPieczarki(object sender, RoutedEventArgs e)
        {
            if (PizzaJest()) { ZmienKursor(60, 60, "pieczarka"); UstawWidokPizzy(); }
        }
        private void WezSer(object sender, RoutedEventArgs e)
        {
            if (PizzaJest()) { ZmienKursor(60, 60, "ser"); UstawWidokPizzy(); }
        }
        private void WezNoz(object sender, RoutedEventArgs e)
        {
            if (PizzaJest() && Pizza.Visibility == Visibility.Visible) { ZmienKursor(20, 160, "noz"); noz.Visibility = Visibility.Hidden; Ciecia.Visibility = Visibility.Visible; }
            else if (Hand == "noz") { ZmienKursor(70, 100, "hand"); PokrojonaPizza = true; noz.Visibility = Visibility.Visible; Ciecia.Visibility = Visibility.Hidden; }
        }
        private void WezWalek(object sender, RoutedEventArgs e)
        {
            if(PizzaJest(false)) { ZmienKursor(400, 56, "walek"); walek.Visibility = Visibility.Hidden; Wymiary.Visibility = Visibility.Visible; }
            else if(Hand == "walek")
            {
                ZmienKursor(70, 100, "hand");
                walek.Visibility = Visibility.Visible;
                Wymiary.Visibility = Visibility.Hidden;

                if (Stolik.Width == 100) RozmiarPizzy = "zaden";
                else if (Stolik.Width < 185)
                {
                    RozmiarPizzy = "mala";
                    dyrektor.StworzPizze(budowniczyMalaPizza);
                    pizza = dyrektor.GetPizza();
                    AktualnyOpis.Text = pizza.GetWypiek()+ " " + pizza.GetOpis() + " " + pizza.GetCena();
                }
                else if (Stolik.Width < 245)
                {
                    RozmiarPizzy = "srednia";
                    dyrektor.StworzPizze(budowniczySredniaPizza);
                    pizza = dyrektor.GetPizza();
                     AktualnyOpis.Text = pizza.GetWypiek()+ " " + pizza.GetOpis() + " " + pizza.GetCena();
                }
                else if (Stolik.Width < 305)
                {
                    RozmiarPizzy = "duza";
                    dyrektor.StworzPizze(budowniczyDuzaPizza);
                    pizza = dyrektor.GetPizza();
                     AktualnyOpis.Text = pizza.GetWypiek()+ " " + pizza.GetOpis() + " " + pizza.GetCena();
                }
            }
        }
        private void WezCiasto(object sender, RoutedEventArgs e)
        {
            Button button = (sender as Button);
            if(Hand == "hand" && !RobionaPizza && Stolik.Visibility == Visibility.Hidden)
            {
                button.Visibility = Visibility.Hidden;
                Stolik.Visibility = Visibility.Visible;
                RobionaPizza = true;
            }
        }
        private void Kosz(object sender, RoutedEventArgs e)
        {
            if (Hand == "pieczarka" || Hand == "sos" || Hand == "ser" || Hand == "salami")
            { ZmienKursor(70, 100, "hand"); PizzaBorder.Visibility = Visibility.Hidden; }
        }

        #endregion

        #region WYKONYWANIE CZYNNOSCI

        private void Upiecz(object sender, RoutedEventArgs e)
        {

            if (piec.Content.ToString() == "Upiecz" && Stolik.Visibility == Visibility.Visible && RozmiarPizzy != "zaden")
            {
                piec.Content = "Wyjmij";
                Stolik.Visibility = Visibility.Hidden;
                Pizza.Visibility = Visibility.Hidden;
                TimerPiecaLabel.Visibility = Visibility.Visible;

                if(CZASPIEC.Seconds == 2)
                    TIMERPIEC = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, TIMERPIEC_TICK, Application.Current.Dispatcher);
                TIMERPIEC.Start();
            }
            else if (piec.Content.ToString() == "Wyjmij" && Stolik.Visibility == Visibility.Hidden)
            {
                piec.Content = "Upiecz";
                Stolik.Visibility = Visibility.Visible;
                Pizza.Visibility = Visibility.Visible;
                TIMERPIEC.Stop();

                if (CZASPIEC.Seconds <= 5)
                {
                    ZmienStopienWypieczenia("ciasto_0");
                    AktualnyOpis.Text = pizza.GetWypiek()+ " " + pizza.GetOpis() + " " + pizza.GetCena();
                }
                else if (CZASPIEC.Seconds <= 10)
                {
                    ZmienStopienWypieczenia("ciasto_1");
                    pizza.Pieczenie(CZASPIEC);
                    AktualnyOpis.Text = pizza.GetWypiek() + " " + pizza.GetOpis() + " " + pizza.GetCena();
                }
                else if (CZASPIEC.Seconds <= 15)
                {
                    ZmienStopienWypieczenia("ciasto_2");
                    pizza.Pieczenie(CZASPIEC);
                    pizza.Pieczenie(CZASPIEC);
                    AktualnyOpis.Text = pizza.GetWypiek() + " " + pizza.GetOpis() + " " + pizza.GetCena();
                }
                else
                {
                    ZmienStopienWypieczenia("ciasto_3");
                    pizza.Pieczenie(CZASPIEC);
                    pizza.Pieczenie(CZASPIEC);
                    pizza.Pieczenie(CZASPIEC);
                    AktualnyOpis.Text = pizza.GetWypiek() + " " + pizza.GetOpis() + " " + pizza.GetCena();
                }
            }
        }
        private void Wyrzuc(object sender, RoutedEventArgs e)
        {
            if (Hand == "hand") WyczyscStanowisko();
        }
        private void Sprzedaj(object sender, RoutedEventArgs e)
        {
            if (Hand == "hand" && RobionaPizza && RozmiarPizzy != "zaden" && Stolik.Visibility == Visibility.Visible && pizza.GetOpis().Length > 7)
            {
                string opistext = "Super!", wypiektext = "Super!", ciecietext = "Super!";
                decimal rachunek = pizza.GetCena();

                if (zamowienie.GetOpis() != pizza.GetOpis()) { rachunek *= 0.5M; opistext = "Kicha!"; }
                if (zamowienie.GetWypiek() != pizza.GetWypiek()) { rachunek *= 0.8M; wypiektext = "Kicha!"; }
                if (!pizza.GetCzesci()) { rachunek *= 0.9M; ciecietext = "Kicha!"; }

                Podsumowanie.Visibility = Visibility.Visible;
                PodsumowanieOpis.Content = "1. Rozmiar i składniki: " + opistext;
                PodsumowanieWypiek.Content = "2. Upieczone: " + wypiektext;
                PodsumowanieCiecie.Content = "3. Pokrojone: " + ciecietext;
                PodsumowanieCena.Content = "CENA: " + Decimal.Round(rachunek, 2) + "$";

                ZAROBKI += Decimal.Round(rachunek, 2);
                TIMERZAMOWIENIE.Stop();
            }
        }
        private void PolozNaPizze(object sender, MouseButtonEventArgs e)
        {
            Image dodatek = new Image();
            BitmapImage bi3;
            if (Hand == "sos") bi3 = new BitmapImage(new Uri("Images/soss.png", UriKind.Relative));
            else bi3 = new BitmapImage(new Uri("Images/" + Hand + ".png", UriKind.Relative));

            dodatek.Source = bi3;
            dodatek.Stretch = Stretch.Fill;
            Point p1 = e.GetPosition(Pizza);
            if (Hand == "ser")
            {
                dodatek.Width = PizzaBorder.Width + 40;
                dodatek.Height = PizzaBorder.Height + 40;
                Canvas.SetTop(dodatek, (280 - PizzaBorder.Width) / 2);
                Canvas.SetLeft(dodatek, (325 - PizzaBorder.Height) / 2);
            }
            else
            {
                Canvas.SetTop(dodatek, p1.Y + 5);
                Canvas.SetLeft(dodatek, p1.X + 5);
            }

            switch (Hand)
            {
                case "sos":
                    if (!pizza.GetOpis().Contains("sos"))
                    {
                        pizza = new Sos(pizza);
                        AktualnyOpis.Text = pizza.GetWypiek()+ " " + pizza.GetOpis() + " " + pizza.GetCena();
                    }
                    break;
                case "ser":
                    if (!pizza.GetOpis().Contains("ser"))
                    {
                        pizza = new Ser(pizza);
                        AktualnyOpis.Text = pizza.GetWypiek()+ " " + pizza.GetOpis() + " " + pizza.GetCena();
                    }
                    break;
                case "pieczarka":
                    if (!pizza.GetOpis().Contains("pieczarki"))
                    {
                        pizza = new Pieczarki(pizza);
                        AktualnyOpis.Text = pizza.GetWypiek()+ " " + pizza.GetOpis() + " " + pizza.GetCena();
                    }
                    break;
                case "salami":
                    if (!pizza.GetOpis().Contains("salami"))
                    {
                        pizza = new Salami(pizza);
                        AktualnyOpis.Text = pizza.GetWypiek()+ " " + pizza.GetOpis() + " " + pizza.GetCena();
                    }
                    break;
            }

            Pizza.Children.Add(dodatek);
        }
        private void SmarujSosem(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && Hand == "sos")
            {
                if (!pizza.GetOpis().Contains("sos"))
                {
                    pizza = new Sos(pizza);
                    AktualnyOpis.Text = pizza.GetWypiek()+ " " + pizza.GetOpis() + " " + pizza.GetCena();
                }

                Image dodatek = new Image();
                BitmapImage bi3 = new BitmapImage(new Uri("Images/soss.png", UriKind.Relative));
                Point p1 = e.GetPosition(Pizza);
                dodatek.Source = bi3;
                dodatek.Stretch = Stretch.Fill;
                Canvas.SetTop(dodatek, p1.Y);
                Canvas.SetLeft(dodatek, p1.X);
                Pizza.Children.Add(dodatek);
            }
        }
        private void Pizza_Ciecie_Start(object sender, MouseButtonEventArgs e)
        {
            if (Hand == "noz")
            {
                LINE = new Line() { Stroke = Brushes.Brown, StrokeThickness = 5 };
                Point START = e.GetPosition(Pizza);
                LINE.X1 = START.X + 12;
                LINE.Y1 = START.Y;
                LINE.X2 = START.X + 12;
                LINE.Y2 = START.Y;
                Pizza.Children.Add(LINE);
            }
        }
        private void Pizza_Ciecie_Stop(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            if (Hand == "noz")
            {
                pizza.Krojenie();
                AktualnyOpis.Text = pizza.GetWypiek() + " " + pizza.GetOpis() + " " + pizza.GetCena() + " " + pizza.GetCzesci();
            }
        }
        private void WalkowanieOrazCiecie(object sender, MouseEventArgs e)
        {
            Point p1 = e.GetPosition(canvas);
            Canvas.SetTop(customPointer, p1.Y + 1);
            Canvas.SetLeft(customPointer, p1.X + 1);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (Hand == "walek" && p1.X > 250 && p1.X < 450 && p1.Y > 300 && p1.Y < 650 && Stolik.Width < 300)
                {
                    Stolik.Width += 0.1;
                    Stolik.Height += 0.1;
                    Canvas.SetTop(customPointer, p1.Y + 1);
                    Canvas.SetLeft(customPointer, p1.X + 1);
                }
                else if (Hand == "noz" && LINE != null && p1.X > 310 && p1.X < 670 && p1.Y > 316 && p1.Y < 630)
                {
                    Mouse.Capture(Pizza);
                    Point p = e.GetPosition(Pizza);
                    LINE.X2 = p.X + 10;
                    LINE.Y2 = p.Y + 12;
                }
            }
        }

        #endregion
    }
}