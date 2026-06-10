# Poikkeus standardista: DEV-002 – Finance legacy -taulunimet

> **Tyyppi:** Approved Deviation  
> **Projekti:** Finance-domain Silver & Gold  
> **Status:** Approved  
> **Päivämäärä:** 2024-11-08  
> **Hyväksyjä:** Data Platform -tiimi / Mikko Virtanen

---

## Standardi josta poiketaan

> `central/standards/EXAMPLE-taulujen-nimeaminen.md` – Fabric-taulujen nimeämiskäytäntö

Standardin mukaan bronze-taulut nimetään muodossa:
```
brz_<domain>_<kohde>
esim. brz_finance_invoices
```

## Poikkeama

Finance-domainin bronze-kerroksessa on 14 taulua joiden nimet ovat muotoa:
```
FIN_INV_MASTER
FIN_GL_ENTRIES
FIN_COST_CENTER
```

Nämä nimet tulevat suoraan lähdejärjestelmästä (SAP ECC) jossa ne on määritelty vuosikymmeniä sitten.
Pipeline kopioi taulut ja nimet sellaisenaan bronzeen.

## Perustelu

- **Tekninen rajoite:** SAP ECC -integraatio käyttää taulunnimiä hakuavaimena. Nimen muuttaminen vaatisi muutoksen integraatio-komponenttiin jota kolmas osapuoli ylläpitää (CGI).
- **Kustannus:** Integraatiomuutos on arvioitu 15 000 € ja 6 viikkoa – ei mahdu projektin budjettiin eikä aikatauluun.
- **Riski on rajattu:** Poikkeus koskee vain bronze-kerrosta. Silver- ja gold-taulut nimetään standardin mukaisesti.

## Riskit ja haitat

- Bronze-taulut eivät noudata yhtenäistä nimeämiskäytäntöä → vaikea löytää/tunnistaa
- Uusi tiimiläinen ei välttämättä ymmärrä SAP-taulunimiä ilman dokumentaatiota
- Jos standardi muuttuu tulevaisuudessa, Finance-bronze-taulujen päivittäminen vaatii SAP-integraatiomuutoksen

## Mitigaatio

- Tämä poikkeama dokumentoitu (tämä tiedosto) – uudet tiimiläiset löytävät selityksen
- Finance-bronzeen lisätty `README.md` joka selittää jokaisen taulunimen SAP-logiikan
- Silver-kerroksesta eteenpäin kaikki taulut noudattavat standardia

## Suunnitelma standardin noudattamiseksi

- [x] Tiketti avattu: [ADO #6103 – Finance SAP-integraation uusiminen](https://dev.azure.com/contoso/finance/_workitems/edit/6103)
- [ ] Arvioitu aikataulu: 2026 Q1 SAP S/4HANA -migraation yhteydessä
- [ ] Ei suunnitelmaa – pysyvä poikkeus

> Tavoite: SAP S/4HANA -siirtymässä otetaan käyttöön uusi integraatiokerros joka tukee standardin mukaista nimeämistä. Bronze-taulut voidaan tässä yhteydessä nimetä uudelleen.

## Viitteet

- [ADO #5944 – Finance-domain käyttöönotto](https://dev.azure.com/contoso/finance/_workitems/edit/5944)
- [Standard: Taulujen nimeäminen](../../central/standards/EXAMPLE-taulujen-nimeaminen.md)
- [Finance bronze README](../../../finance-workspace-repo/bronze/README.md)
