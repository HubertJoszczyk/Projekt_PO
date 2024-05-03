//Klasa dla pracownika sklepu (zarzadzanie stanem magazynowym oraz obsluga zamowien)
class Pracownik_sklepu{
    private List<Ksiazka> dostepne_Ksiazki = new List<Ksiazka>();
    private List<Zamowienia> zamowienia = new List<Zamowienia>();   //tutaj trzeba będzie zrobić klase zamówienia gdzie będzie "tytuł książki/status wysłania"
    public void Dodaj_do_oferty(List<Ksiazka> Ksiazki){
        dostepne_Ksiazki.AddRange(Ksiazki);
    }
    public void Dodaj_zamowienie(Zamowienia zamowienia){
        zamowienia.Add(zamowienia);
        Console.WriteLine("Złożono zamówienie");
        
    }
}
//Klasa do przechowywania statusu zamowienia
class Zamowienia{
    private List<Ksiazka> Ksiazki = new List<Ksiazka>();
    private bool Czy_wyslane ;
}
//Klasa dla klienta (przegladanie oferty i skladanie zamowienia)
class Klient{

}  
//Klasa dla ksiazek
class Ksiazka{
    private string Tytul;
    private string Autor;
    private string Kategoria;
    private string Format;
    private double Cena;
    private bool Dostepnosc;
        public Ksiazka(){

        }
    }
interface Program
{
    
}