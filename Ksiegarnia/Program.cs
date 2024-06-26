﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
class Baza_pracownikow
{
    string sciezkaPliku = "Baza_pracownikow.txt";
    private List<Tuple<int, string, string>> pracownicy = new List<Tuple<int, string, string>>();
    private int currentID = 1;

    public virtual void DodajPracownika(string imie, string nazwisko)
    {
        pracownicy.Add(new Tuple<int, string, string>(currentID, imie, nazwisko));
        currentID++;
    }

    public void ZapiszDoPliku(string sciezkaPliku)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(sciezkaPliku))
            {
                foreach (var pracownik in pracownicy)
                {
                    sw.WriteLine($"{pracownik.Item1},{pracownik.Item2},{pracownik.Item3}");
                }
            }
        }
        catch (Exception blad_zapisu_pracownikow)
        {
            Console.WriteLine("Nie udało się zapisać do pliku" + blad_zapisu_pracownikow.Message);
        }
    }

    public void OdczytajZPliku(string sciezkaPliku)
    {
        try
        {
            if (!File.Exists(sciezkaPliku))
            {
                Console.WriteLine("Plik nie istnieje.");
                return;
            }
            using (StreamReader sr = new StreamReader(sciezkaPliku, Encoding.UTF8))
            {
                string linia;
                while ((linia = sr.ReadLine()) != null)
                {
                    var parts = linia.Split(',');
                    int id = int.Parse(parts[0]);
                    string imie = parts[1];
                    string nazwisko = parts[2];
                    pracownicy.Add(new Tuple<int, string, string>(id, imie, nazwisko));
                    if (id >= currentID)
                    {
                        currentID = id + 1;
                    }
                }
            }
        }
        catch (Exception blad_odczytu_pracownikow)
        {
            Console.WriteLine("Nie udało się odczytać z pliku" + blad_odczytu_pracownikow.Message);
        }
    }
}

class Pracownicy : Baza_pracownikow
{
    private string Imie;
    private string Nazwisko;

    public string GetImie()
    {
        return Imie;
    }
    public string GetNazwisko()
    {
        return Nazwisko;
    }
    public Pracownicy(string imie, string nazwisko)
    {
        this.Imie = imie;
        this.Nazwisko = nazwisko;

    }
    public override void DodajPracownika(string imie, string nazwisko)
    {
        base.DodajPracownika(imie, nazwisko);
    }
}

class Koszyk
{
    private List<Produkt> produktyWKoszyku = new List<Produkt>();

    public void DodajProduktDoKoszyka(Produkt produkt)
    {
        produktyWKoszyku.Add(produkt);
        Console.WriteLine($"Produkt \"{produkt.Nazwa}\" został dodany do koszyka.");
    }
    public void WyswietlProduktyWKoszyku()
    {
        Console.WriteLine("Produkty w koszyku:");
        foreach (var produkt in produktyWKoszyku)
        {
            Console.WriteLine(produkt);
        }
    }
    public List<Produkt> PobierzProdukty()
    {
        return produktyWKoszyku;
    }
    public void UsunZKoszyka(Produkt produkt)
    {
        produktyWKoszyku.Remove(produkt);
    }
}

class Produkt
{
    public string Nazwa { get; set; }
    public string Kategoria { get; set; }
    public string Autor { get; set; }
    public decimal Cena { get; set; }

    public Produkt(string nazwa, string kategoria, string autor, decimal cena)
    {
        this.Nazwa = nazwa;
        this.Kategoria = kategoria;
        this.Autor = autor;
        this.Cena = cena;
    }

    public override string ToString()
    {
        return $"{Nazwa},{Kategoria},{Autor},{Cena}";
    }
}
class Magazyn
{
    string sciezkaPliku = "Magazyn.txt";
    private List<Produkt> produkty = new List<Produkt>();

    public void DodajProdukt(Produkt produkt)
    {
        produkty.Add(produkt);
        ZapiszProduktDoPliku(produkt);
    }

    private void ZapiszProduktDoPliku(Produkt produkt)
    {
        using (StreamWriter sw = new StreamWriter(sciezkaPliku, true, Encoding.UTF8))
        {
            sw.WriteLine(produkt.ToString());
        }
    }
    public void OdczytajZPliku()
    {
        try
        {
            using (StreamReader sr = new StreamReader(sciezkaPliku, Encoding.UTF8))
            {
                string linia;
                while ((linia = sr.ReadLine()) != null)
                {
                    var parts = linia.Split(',');
                    string nazwa = parts[0];
                    string kategoria = parts[1];
                    string autor = parts[2];
                    decimal cena = decimal.Parse(parts[3]);

                    if (parts.Length == 6) // Cyfrowa
                    {
                        bool jestEbookiem = bool.Parse(parts[4]);
                        bool jestAudiobookiem = bool.Parse(parts[5]);
                        produkty.Add(new Cyfrowa(nazwa, kategoria, autor, cena, jestEbookiem, jestAudiobookiem));
                    }
                    else if (parts.Length == 5) // Fizyczna
                    {
                        bool miekkaOprawa = bool.Parse(parts[4]);
                        produkty.Add(new Fizyczna(nazwa, kategoria, autor, cena, miekkaOprawa, !miekkaOprawa));
                    }
                }
            }
        }
        catch (Exception blad_odczytu_magazynu)
        {
            Console.WriteLine("Nie udało się odczytać z pliku" + blad_odczytu_magazynu.Message);
        }
    }

    public List<Produkt> PobierzProdukty()
    {
        return produkty;
    }
}
class Realizacja
{
    public void RealizujZamowienie(Koszyk koszyk)
    {
        var produkty = koszyk.PobierzProdukty();
        if (produkty.Count == 0)
        {
            Console.WriteLine("Koszyk jest pusty. Nie można zrealizować zamówienia.");
            return;
        }
        Console.WriteLine("Realizowanie zamówienia:");
        foreach (var produkt in produkty)
        {
            Console.WriteLine(produkt);
        }
        Console.WriteLine("Zamówienie zostało zrealizowane.");
    }
}

class Cyfrowa : Produkt
{
    public bool JestEbookiem { get; set; }
    public bool JestAudiobookiem { get; set; }

    public Cyfrowa(string nazwa, string kategoria, string autor, decimal cena, bool jestEbookiem, bool jestAudiobookiem)
        : base(nazwa, kategoria, autor, cena)
    {
        JestEbookiem = jestEbookiem;
        JestAudiobookiem = jestAudiobookiem;
    }
}

class Fizyczna : Produkt
{
    public bool miekkaOprawa { get; set; }
    public bool twardaOprawa { get; set; }

    public Fizyczna(string nazwa, string kategoria, string autor, decimal cena, bool miekkaOprawa, bool twardaOprawa)
        : base(nazwa, kategoria, autor, cena)
    {
        this.miekkaOprawa = miekkaOprawa;
        this.twardaOprawa = twardaOprawa;
    }
}

class Baza_klientow
{
    string sciezkaPliku = "Baza_klientow.txt";
    protected List<Tuple<int, string, string>> klienci = new List<Tuple<int, string, string>>();
    private int currentID = 1;

    public virtual void DodajKlienta(string imie, string nazwisko)
    {
        klienci.Add(new Tuple<int, string, string>(currentID, imie, nazwisko));
        currentID++;
    }

    public void ZapiszDoPliku(string sciezkaPliku)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(sciezkaPliku))
            {
                foreach (var klient in klienci)
                {
                    sw.WriteLine($"{klient.Item1},{klient.Item2},{klient.Item3}");
                }
            }
        }
        catch (Exception blad_zapisu_klientow)
        {
            Console.WriteLine("Nie udało się zapisać do pliku" + blad_zapisu_klientow.Message);
        }
    }
    public void OdczytajZPliku(string sciezkaPliku)
    {
        try
        {
            using (StreamReader sr = new StreamReader(sciezkaPliku, Encoding.UTF8))
            {
                string linia;
                while ((linia = sr.ReadLine()) != null)
                {
                    var parts = linia.Split(',');
                    int id = int.Parse(parts[0]);
                    string imie = parts[1];
                    string nazwisko = parts[2];
                    klienci.Add(new Tuple<int, string, string>(id, imie, nazwisko));
                    if (id >= currentID)
                    {
                        currentID = id + 1;
                    }
                }
            }
        }
        catch (Exception blad_odczytu_klientow)
        {
            Console.WriteLine("Nie udało się odczytać z pliku" + blad_odczytu_klientow.Message);
        }
    }
}

class Klienci : Baza_klientow
{
    private string Imie;
    private string Nazwisko;
    private Koszyk koszyk = new Koszyk();
    private Magazyn magazyn;
    public string GetImie()
    {
        return Imie;
    }
    public string GetNazwisko()
    {
        return Nazwisko;
    }
    public Klienci(string imie, string nazwisko, Magazyn magazyn)
    {
        this.Imie = imie;
        this.Nazwisko = nazwisko;
        this.magazyn = magazyn;
    }
    public override void DodajKlienta(string imie, string nazwisko)
    {
        base.DodajKlienta(imie, nazwisko);
        Console.WriteLine($"Klient {imie} {nazwisko} został dodany.");
    }
    public void PrzeszukajMagazynPoNazwie(string nazwa)
    {
        using (StreamReader sr = new StreamReader("Magazyn.txt"))
        {
            string linia;
            while ((linia = sr.ReadLine()) != null)
            {
                if (linia.Contains(nazwa))
                {
                    Console.WriteLine($"znaleziono: {linia}");
                }
            }
        }
    }

    public void PrzeszukajMagazynPoKategorii(string kategoria)
    {
        using (StreamReader sr = new StreamReader("Magazyn.txt"))
        {
            string linia;
            while ((linia = sr.ReadLine()) != null)
            {
                if (linia.Contains(kategoria))
                {
                    Console.WriteLine($"znaleziono: {linia}");
                }
            }
        }
    }
    public void PrzeszukajMagazynPoAutorze(string autor)
    {
        using (StreamReader sr = new StreamReader("Magazyn.txt"))
        {
            string linia;
            while ((linia = sr.ReadLine()) != null)
            {
                if (linia.Contains(autor))
                {
                    Console.WriteLine($"znaleziono: {linia}");
                }
            }
        }
    }
    public void ZamowKsiazke(string nazwaKsiazki)
    {
        var produkty = magazyn.PobierzProdukty().Where(p => p.Nazwa.Equals(nazwaKsiazki, StringComparison.OrdinalIgnoreCase)).ToList();
        if (produkty.Count == 0)
        {
            Console.WriteLine("Nie znaleziono książki o podanej nazwie.");
            return;
        }

        Console.WriteLine("Dostępne produkty:");
        for (int i = 0; i < produkty.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {produkty[i]}");
        }
        Console.Write("Wybierz produkt (podaj numer): ");
        int wybor = int.Parse(Console.ReadLine()) - 1;

        if (wybor < 0 || wybor >= produkty.Count)
        {
            Console.WriteLine("Niepoprawny wybór.");
            return;
        }

        koszyk.DodajProduktDoKoszyka(produkty[wybor]);
    }

    public void WyswietlProduktyWKoszyku()
    {
        koszyk.WyswietlProduktyWKoszyku();
    }

    public Koszyk PobierzKoszyk()
    {
        return koszyk;
    }

    public void WyswietlWszystkieKsiazki()
    {
        var produkty = magazyn.PobierzProdukty();
        if (produkty.Count == 0)
        {
            Console.WriteLine("Brak dostępnych produktów.");
            return;
        }

        Console.WriteLine("Wszystkie dostępne produkty:");
        foreach (var produkt in produkty)
        {
            Console.WriteLine(produkt);
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Baza_pracownikow bazaPracownikow = new Baza_pracownikow();
        Baza_klientow bazaKlientow = new Baza_klientow();
        Magazyn magazyn = new Magazyn();
        magazyn.OdczytajZPliku();
        bazaPracownikow.OdczytajZPliku("Baza_pracownikow.txt");
        bazaKlientow.OdczytajZPliku("Baza_klientow.txt");



        bool running = true;

        while (running)
        {
            Console.WriteLine("1. Pracownik");
            Console.WriteLine("2. Klient");
            Console.WriteLine("3. Wyjście");
            Console.Write("Wybierz rolę: ");
            string rola = Console.ReadLine();

            switch (rola)
            {
                case "1":
                    PracownikMenu(bazaPracownikow, magazyn);
                    break;
                case "2":
                    KlientMenu(bazaKlientow, magazyn);
                    break;
                case "3":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Niepoprawny wybór.");
                    break;
            }
        }
    }
    static void PracownikMenu(Baza_pracownikow bazaPracownikow, Magazyn magazyn)
    {
        while (true)
        {
            Console.WriteLine("1. Dodaj pracownika");
            Console.WriteLine("2. Dodaj produkt do magazynu");
            Console.WriteLine("3. Wyświetl produkty w magazynie");
            Console.WriteLine("4. Wyświetl pracowników");
            Console.WriteLine("5. Baza klientów");
            Console.WriteLine("6. Wróć do głównego menu");
            Console.Write("Wybierz opcję: ");
            string opcja = Console.ReadLine();

            switch (opcja)
            {
                case "1":
                    Console.Write("Podaj imię pracownika: ");
                    string imie = Console.ReadLine();
                    Console.Write("Podaj nazwisko pracownika: ");
                    string nazwisko = Console.ReadLine();
                    bazaPracownikow.DodajPracownika(imie, nazwisko);
                    bazaPracownikow.ZapiszDoPliku("Baza_pracownikow.txt");
                    break;
                case "2":
                    Console.Write("Podaj nazwę produktu: ");
                    string nazwa = Console.ReadLine();
                    Console.Write("Podaj kategorię produktu: ");
                    string kategoria = Console.ReadLine();
                    Console.Write("Podaj autora produktu: ");
                    string autor = Console.ReadLine();
                    Console.Write("Podaj cenę produktu: ");
                    decimal cena = decimal.Parse(Console.ReadLine());
                    Console.Write("Czy jest to produkt cyfrowy (tak/nie): ");
                    string czyCyfrowy = Console.ReadLine();
                    Produkt produkt;
                    if (czyCyfrowy.ToLower() == "tak")
                    {
                        Console.Write("Czy jest to ebook (tak/nie): ");
                        bool jestEbookiem = Console.ReadLine().ToLower() == "tak";
                        Console.Write("Czy jest to audiobook (tak/nie): ");
                        bool jestAudiobookiem = Console.ReadLine().ToLower() == "tak";
                        produkt = new Cyfrowa(nazwa, kategoria, autor, cena, jestEbookiem, jestAudiobookiem);
                    }
                    else
                    {
                        Console.Write("Czy ma miękką oprawę (tak/nie): ");
                        bool miekkaOprawa = Console.ReadLine().ToLower() == "tak";
                        bool twardaOprawa = !miekkaOprawa;
                        produkt = new Fizyczna(nazwa, kategoria, autor, cena, miekkaOprawa, twardaOprawa);
                    }
                    magazyn.DodajProdukt(produkt);
                    break;
                case "3":
                    string ksiazkiplik = File.ReadAllText("Magazyn.txt");
                    Console.WriteLine(ksiazkiplik);
                    break;
                case "4":
                    string pracownikplik = File.ReadAllText("Baza_pracownikow.txt");
                    Console.WriteLine(pracownikplik);
                    break;
                case "5":
                    string klientplik = File.ReadAllText("Baza_klientow.txt");
                    Console.WriteLine(klientplik);
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Niepoprawny wybór.");
                    break;
            }
        }
    }

    static void KlientMenu(Baza_klientow bazaKlientow, Magazyn magazyn)
    {
        Console.Write("Podaj swoje imię: ");
        string imie = Console.ReadLine();
        Console.Write("Podaj swoje nazwisko: ");
        string nazwisko = Console.ReadLine();
        Klienci klient = new Klienci(imie, nazwisko, magazyn);
        bazaKlientow.DodajKlienta(imie, nazwisko);
        bazaKlientow.ZapiszDoPliku("Baza_klientow.txt");

        while (true)
        {
            Console.WriteLine("1. Przeszukaj sklep po nazwie");
            Console.WriteLine("2. Przeszukaj sklep po kategorii");
            Console.WriteLine("3. Przeszukaj sklep po autorze");
            Console.WriteLine("4. Zamów książkę");
            Console.WriteLine("5. Wyświetl produkty w koszyku");
            Console.WriteLine("6. Złóż zamówienie");
            Console.WriteLine("7. Pokaż wszystkie książki");
            Console.WriteLine("8. Wróć do głównego menu");
            Console.Write("Wybierz opcję: ");
            string opcja = Console.ReadLine();

            switch (opcja)
            {
                case "1":
                    Console.Write("Podaj nazwę: ");
                    string nazwa = Console.ReadLine();
                    klient.PrzeszukajMagazynPoNazwie(nazwa);
                    break;
                case "2":
                    Console.Write("Podaj kategorię: ");
                    string kategoria = Console.ReadLine();
                    klient.PrzeszukajMagazynPoKategorii(kategoria);
                    break;
                case "3":
                    Console.Write("Podaj autora: ");
                    string autor = Console.ReadLine();
                    klient.PrzeszukajMagazynPoAutorze(autor);
                    break;
                case "4":
                    Console.Write("Podaj nazwę książki: ");
                    string nazwaKsiazki = Console.ReadLine();
                    klient.ZamowKsiazke(nazwaKsiazki);
                    break;
                case "5":
                    klient.WyswietlProduktyWKoszyku();
                    break;
                case "6":
                    Realizacja realizacja = new Realizacja();
                    realizacja.RealizujZamowienie(klient.PobierzKoszyk());
                    break;
                case "7":
                    string ksiazkiplik = File.ReadAllText("Magazyn.txt");
                    Console.WriteLine(ksiazkiplik);
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Niepoprawny wybór.");
                    break;
            }
        }
    }
}
