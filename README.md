
# 🏥 Web Aplikacija - Dom Zdravlja

Projekat iz predmeta **Primenjeno Softversko Inženjerstvo 2023/2024 – Web programiranje**

## 📋 Opis projekta

Ova aplikacija simulira informacioni sistem doma zdravlja sa podrškom za više korisničkih uloga (administrator, lekar, pacijent) i osnovnim funkcijama zakazivanja termina, vođenja terapija i upravljanja pacijentima. Projektovan je korišćenjem isključivo tehnologija definisanih u okviru kursa.

## 👥 Korisničke uloge

- **Neprijavljeni korisnik (NK)** – može da se prijavi u sistem
- **Pacijent (P)** – zakazuje termine, pregleda zakazane termine i terapije
- **Lekar (L)** – kreira termine, pregleda termine i prepisuje terapije
- **Administrator (A)** – upravlja pacijentima (kreira, ažurira, briše, pregleda)

## 📦 Entiteti

- **Pacijent**: korisničko ime, JMBG, šifra, ime, prezime, datum rođenja, e-mail, termini
- **Lekar**: korisničko ime, šifra, ime, prezime, datum rođenja, e-mail, termini
- **Administrator**: korisničko ime, šifra, ime, prezime, datum rođenja
- **Termin**: lekar, status (slobodan/zakazan), datum i vreme, opis terapije
- **Zdravstveni karton**: lista termina, pacijent

## ✅ Funkcionalnosti

### Za ocenu 6:

- Prijava na sistem (NK)
- Zakazivanje termina (P)
- Pregled termina (P, L)
- Kreiranje termina (L)
- Prepisivanje terapije (L)
- Pregled terapija (P, L)
- Kreiranje pacijenta (A)
- Ažuriranje pacijenta (A)
- Brisanje pacijenta (A)
- Pregled pacijenata (A)

### Dodatne funkcionalnosti za ocenu 7:

- Sve funkcionalnosti za ocenu 6
- Napredni pregled termina:
  - Pacijent: filtriranje i sortiranje po lekaru, datumu i vremenu
  - Lekar: filtriranje i sortiranje po JMBG-u, imenu i prezimenu pacijenta, statusu i datumu i vremenu
- Napredni pregled pacijenata:
  - Administrator: filtriranje i sortiranje po JMBG-u, imenu, prezimenu, datumu rođenja i e-mail adresi

## 💾 Skladištenje podataka

- Svi podaci se trajno čuvaju u **tekstualnim fajlovima**
- Dozvoljeni formati: `.json`, `.xml`, `.csv`, `.tsv` ili proizvoljan `.dsv` format

## 🎨 Dizajn i stil

- Stilizacija i dizajn interfejsa su obavezni i prepušteni studentu
- Korišćenje CSS-a je obavezno

