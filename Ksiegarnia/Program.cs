﻿using System.IO;

class Baza_Pracownikow{
    private List<Pracownik> Lista_Pracownicy = new List<Pracownik>();
    private Baza_Pracownicy;
    public List<Pracownik> GetPracownicy(){
        return Lista_Pracownicy;
    }
    public void Dodaj_Pracownik(Pracownik pracownik){
        Lista_Pracownicy.Add(pracownik);
    }
    public void Usun_Pracownik(int Id){
        Lista_Pracownicy.RemoveAt(Id);
    }
    public void ZapiszPracownicy(){
        System.IO.File.WriteAllLines("Baza_Pracownicy.txt",Lista_Pracownicy.All());
    }
    public Baza_Pracownikow(String Nazwa_Pliku){
        System.IO.File.ReadAllLines();

    }
}
class Pracownik{
    private string Imie;
    private string Nazwisko;
    private int Id;
    public string GetImie(){
        return Imie;
    }
    public string GetNazwisko(){
        return Nazwisko;
    }
    public int GetId(){
        return Id;
    }
    public Pracownik(String imie_pracownik,String nazwisko_pracownik,int id){
        this.Imie = imie_pracownik;
        this.Nazwisko = nazwisko_pracownik;
        this.Id = id;
    }
}
class Baza_Zamowien{
    private List<Zamowienie> Lista_Zamowien = new List<Zamowienie>();
    private Baza_Zamowienia;
    public List<Zamowienie> GetZamowienia(){
        return Lista_Zamowien;
    }
    public void Usun_Zamowienie(int Id){
        Lista_Zamowien.RemoveAt(Id);
    }
    public void Dodaj_Zamowienie(Zamowienie zamowienie){
        Lista_Zamowien.Add(zamowienie);
    }
    public Baza_Zamowien(String Nazwa_Pliku){

    }
}
class Zamowienie{
    private string Status;
    private int Id;
    private string Adres;
    private Koszyk koszyk;
    public string GetStatus(){
        return Status;
    }
    public string GetAdres(){
        return Adres;
    }
    public void Zmien_Status(int Id){

    }
    public Zamowienie(string Adres,Koszyk koszyk){

    }
}
class Koszyk{
    private List<Ksiazka> Ksiazki_Fizyczne = new List<Ksiazka>();
    private List<Ksiazka> Ksiazki_Cyfrowe = new List<Ksiazka>();
    public void Dodaj_Fizyczna(){

    }
    public void Usun_Fizyczna(){
        Ksiazki_Fizyczne.RemoveAt();
    }
    public void Dodaj_Cyfrowa(){

    }
    public void Usun_Cyfrowa(){
        Ksiazki_Cyfrowe.RemoveAt();
    }
    public Koszyk(){}
}
class Magazyn{
    private List<Ksiazka> Lista_Ksiazki = new List<Ksiazka>();
    public void Dodaj_Ksiazke_Cyfrowa(Ksiazka_Cyfrowa ksiazka_cyfrowa){
        
    }
    public void Dodaj_Ksiazke_Fizyczna(Ksiazka_Fizyczna ksiazka_fizyczna){

    }
    
}
class Ksiazka{
    private string Tytul;
    private string Autor;
    private string Kategoria;
    private double Cena;
    public string GetTytul(){
        return Tytul;
    }
    public string GetAutor(){
        return Autor;
    }
    public string GetKategoria(){
        return Kategoria;
    }
    public double GetCena(){
        return Cena;
    }
    public string OpiszKsiazke(){

    }
    public Ksiazka(string Tytul,string Autor,string Kategoria,double Cena){
        this.Autor=Autor;
        this.Cena=Cena;
        this.Tytul=Tytul;
        this.Kategoria=Kategoria;
    }
}
class Ksiazka_Fizyczna:Ksiazka{
    
    private string Oprawa;
    public string GetOprawa(){
        return Oprawa;
    }
    public Ksiazka_Fizyczna(string Tytul,string Autor,string Kategoria,double Cena):base(Tytul,Autor,Kategoria,Cena){

    }
}
class Ksiazka_Cyfrowa:Ksiazka{
    private string Link_do_Pobrania;
    private string Format;
    public string GetFormat(){
        return Format;
    }
    public string GetLink(){
        return Link_do_Pobrania;
    }
    public Ksiazka_Cyfrowa(string Tytul,string Autor,string Kategoria,double Cena,string Format):base(Tytul,Autor,Kategoria,Cena){
        
    }
}
class Klient{
    private Koszyk Koszyk_Klient;
    private int Id;
    private string Imie;
    private string Nazwisko;
    public int GetId(){
        return Id;
    }
    public string GetImie(){
        return Imie;
    }
    public string GetNazwisko(){
        return Nazwisko;
    }
    public Koszyk GetKoszyk(){
        return Koszyk_Klient;
    }
    public Klient(string Imie_klient,string Nazwisko_klient){
        this.Imie = Imie_klient;
        this.Nazwisko = Nazwisko_klient;
    }
}
class Baza_Klientow{
    private List<Klient> Lista_Klienci = new List<Klient>();
    private Baza_Klienci;
    public void Dodaj_Klienta(Klient klient){
        Lista_Klienci.Add(klient);
    }
    public void Usun_Klienta(int Id){
        Lista_Klienci.RemoveAt(Id);
    }
    public List<Klient> GetKlienci(){
        return Lista_Klienci;
    }
    public Baza_Klientow(string Nazwa_Pliku){

    }
    public void Zapisz_Klientow(List<Klient> Lista_Klienci){
        
    }
}