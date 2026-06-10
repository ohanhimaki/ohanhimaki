# ADR-003: Silver-kerroksen historian säilytysstrategia

> **Tyyppi:** Architecture Decision Record (projektikohtainen)  
> **Projekti:** Sales-domain Silver & Gold  
> **Status:** Accepted  
> **Päivämäärä:** 2025-01-14  
> **Päättäjät:** Sales-tiimi tech lead (Janne Mäkinen), Data Platform -tiimi

---

## Konteksti

Sales-silver-kerroksessa on tilausdataa joka muuttuu jälkikäteen (tilauksen status, laskutettu summa).
Lähdejärjestelmä (Salesforce) ei tarjoa muutoslokia – vain nykyisen tilan.

Ongelma: jos silver ylikirjoittaa aina uusimmalla datalla, menetetään tieto siitä
miten tilauksen tila on muuttunut. Raportointi vaatii historiaa (esim. "montako tilauusta
muuttui tilasta 'pending' → 'cancelled' viime viikolla").

Viittaa central ADR-001: Medallion-arkkitehtuurin käyttöönotto.

## Päätös

**Valitsimme: SCD Type 2 (Slowly Changing Dimension, historiarivi) silver-kerroksessa tilausdatalle.**

Jokaisesta muuttuneesta rivistä luodaan uusi rivi aiemman rinnalle. Aktiivinen rivi merkitään
`is_current = true`, päättyneelle asetetaan `valid_to`-päivämäärä.

## Perustelut

- Liiketoiminta tarvitsee selvästi muutoshistorian raportointiin
- Delta Lake tukee SCD2-logiikkaa tehokkaasti `MERGE`-operaatiolla
- Bronze säilyttää raakadatan, joten SCD2 silverissä ei lisää riskiä datan häviämisestä

## Harkitut vaihtoehdot

### Vaihtoehto A – SCD Type 2 silverissä (valittu)
- ✅ Täysi muutoshistoria saatavilla
- ✅ Gold voi rakentaa snapshotteja tarpeen mukaan
- ❌ Taulun koko kasvaa merkittävästi ajan myötä → partitionointi pakollinen

### Vaihtoehto B – Overwrite (vain nykyinen tila)
- ✅ Yksinkertainen toteutus
- ❌ Historia menetetään – liiketoimintavaatimus ei täyty

### Vaihtoehto C – Snapshot-taulu erillään (päivittäinen tilannekuva)
- ✅ Helpompi kyselläi
- ❌ Kasvaa nopeasti (koko taulun kopio joka päivä)
- ❌ Ei tue tietyn muutoksen tarkkaa päivämäärää

## Seuraukset

- `slv_sales_orders` -taulussa on sarakkeet: `valid_from`, `valid_to`, `is_current`
- Partitionointi `valid_from`-sarakkeen vuoden mukaan – tarkistetaan vuosittain
- Gold-kerros suodattaa aina `WHERE is_current = true` ellei historia tarvita
- Pipeline-ajoaika kasvaa ~20% MERGE-logiikan vuoksi → hyväksytty

## Suhde platform-standardeihin

- [x] Noudattaa platform-standardia taulujen nimeämisessä
- [ ] Poikkeaa standardista → SCD2-sarakkeet (`valid_from`, `valid_to`, `is_current`) eivät ole platformstandardissa määritelty → lisätään standardiin v1.3

## Viitteet

- [ADO Story #5812 – Sales historiaraportointi](https://dev.azure.com/contoso/sales/_workitems/edit/5812)
- [Central ADR-001 – Medallion-arkkitehtuuri](../../central/adr/EXAMPLE-ADR-001-medallion-arkkitehtuuri.md)
