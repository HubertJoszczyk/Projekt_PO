using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Baza_pracownikow
{
    string sciezkaPliku = "Baza_pracownikow.txt";
    protected List<Tuple<int, string, string>> pracownicy = new List<Tuple<int, string, string>>();
    private int currentID = 0;

    public void DodajPracownika(string imie, string nazwisko)
    {
        pracownicy.Add(new Tuple<int, string, string>(currentID, imie, nazwisko));
        currentID++;
    }

    public void ZapiszDoPliku(string sciezkaPliku)
    {
        using (StreamWriter sw = new StreamWriter(sciezkaPliku))
        {
            foreach (var pracownik in pracownicy)
            {
                sw.WriteLine($"{pracownik.Item1},{pracownik.Item2},{pracownik.Item3}");
            }
        }
    }

    public void OdczytajZPliku(string sciezkaPliku)
    {
        using (StreamReader sr = new StreamReader(sciezkaPliku))
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
    public void UzupelnijDanePracownika(string imie, string nazwisko)
    {
        DodajPracownika(imie, nazwisko);
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
    }

    public void SortujPoNazwie()
    {
        produkty = produkty.OrderBy(p => p.Nazwa).ToList();
    }

    public void SortujPoKategorii()
    {
        produkty = produkty.OrderBy(p => p.Kategoria).ToList();
    }

    public void SortujPoAutorze()
    {
        produkty = produkty.OrderBy(p => p.Autor).ToList();
    }

    public void SortujPoCenie()
    {
        produkty = produkty.OrderBy(p => p.Cena).ToList();
    }

    public void ZapiszDoPliku(string sciezkaPliku)
    {
        using (StreamWriter sw = new StreamWriter(sciezkaPliku))
        {
            foreach (Produkt produkt in produkty)
            {
                sw.WriteLine(produkt.ToString());
            }
        }
    }

    public void OdczytajZPliku(string sciezkaPliku)
    {
        using (StreamReader sr = new StreamReader(sciezkaPliku))
        {
            string linia;
            while ((linia = sr.ReadLine()) != null)
            {
                var parts = linia.Split(',');
                string nazwa = parts[0];
                string kategoria = parts[1];
                string autor = parts[2];
                decimal cena = decimal.Parse(parts[3]);
                produkty.Add(new Produkt(nazwa, kategoria, autor, cena));
            }
        }
    }

    public List<Produkt> PobierzProdukty()
    {
        return produkty;
    }
}

class Realizacja
{
    public void RealizujZamowienie(Koszyk koszyk, Baza_pracownikow bazaPracownikow)
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
        miekkaOprawa = miekkaOprawa;
        twardaOprawa = twardaOprawa;
    }
}

class Baza_klientow
{
    string sciezkaPliku = "Baza_klientow.txt";
    protected List<Tuple<int, string, string>> klienci = new List<Tuple<int, string, string>>();
    private int currentID = 0;

    public void DodajKlienta(string imie, string nazwisko)
    {
        klienci.Add(new Tuple<int, string, string>(currentID, imie, nazwisko));
        currentID++;
    }

    public void ZapiszDoPliku(string sciezkaPliku)
    {
        using (StreamWriter sw = new StreamWriter(sciezkaPliku))
        {
            foreach (var klient in klienci)
            {
                sw.WriteLine($"{klient.Item1},{klient.Item2},{klient.Item3}");
            }
        }
    }

    public void OdczytajZPliku(string sciezkaPliku)
    {
        using (StreamReader sr = new StreamReader(sciezkaPliku))
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
}

class Klienci : Baza_klientow
{
    private Magazyn magazyn;

    public Klienci(Magazyn magazyn)
    {
        this.magazyn = magazyn;
    }

    public void PrzeszukajMagazyn(string filtr = "", decimal minCena = 0, decimal maxCena = decimal.MaxValue)
    {
        List<Produkt> znalezioneProdukty = new List<Produkt>();

        foreach (var produkt in magazyn.PobierzProdukty())
        {
            if ((produkt.Nazwa.Contains(filtr) || produkt.Kategoria.Contains(filtr) || produkt.Autor.Contains(filtr)) &&
                produkt.Cena >= minCena && produkt.Cena <= maxCena)
            {
                znalezioneProdukty.Add(produkt);
            }
        }

        Console.WriteLine($"Znalezione produkty w magazynie (Filtr: \"{filtr}\", Cena: od {minCena} do {maxCena}):");
        foreach (var produkt in znalezioneProdukty)
        {
            Console.WriteLine(produkt);
        }
    }
}