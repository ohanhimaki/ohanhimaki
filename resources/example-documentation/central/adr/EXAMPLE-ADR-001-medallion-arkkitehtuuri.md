# ADR-001: Medallion-arkkitehtuurin käyttöönotto Microsoft Fabricissa

> **Tyyppi:** Architecture Decision Record  
> **Status:** Accepted  
> **Päivämäärä:** 2024-08-20  
> **Päättäjät:** Data Platform -tiimi, Arkkitehtiryhmä

---

## Konteksti

Siirryimme Microsoft Fabric -alustalle syksyllä 2024. Tarvitsimme selkeän datakerrosmallin
joka ohjaa kaikkia projekteja yhdenmukaisesti. Ilman yhteistä mallia jokainen tiimi rakentaisi
oman logiikkansa, mikä johtaisi päällekkäiseen työhön ja vaikeasti ylläpidettävään kokonaisuuteen.

Rajoitteita:
- Fabric on uusi alusta tiimille – oppimiskäyrä huomioitava
- Olemassa olevat lähdejärjestelmät ovat heterogeenisia (SQL Server, REST API, SFTP)
- Tietoturvavaatimukset edellyttävät selkeää kerroserottelua (raakadata ei suoraan liiketoiminnalle)

## Päätös

**Valitsimme: Medallion-arkkitehtuurin (Bronze → Silver → Gold) Microsoft Fabric Lakehousessa.**

Kaikki dataprojektit noudattavat kolmikerroksista mallia. Kukin kerros on oma Fabric Lakehouse -workspacensa per domain.

## Perustelut

- Medallion on de facto -standardi modernissa data engineering -työssä – laaja yhteisötuki ja dokumentaatio
- Selkeä kerroserottelu mahdollistaa eri tiimien itsenäisen kehityksen ilman riippuvuusongelmia
- Fabric Lakehouse tukee Delta-formaattia natiivisti → time travel ja schema evolution ilman lisätyötä
- Microsoft itse suosittelee tätä mallia Fabric-ympäristössä

## Harkitut vaihtoehdot

### Vaihtoehto A – Medallion (Bronze / Silver / Gold) (valittu)

- ✅ Selkeä vastuunjako kerrosten välillä
- ✅ Raakadata aina tallessa bronzessa – helppo reprocessoida
- ✅ Gold-kerros optimoitu liiketoimintakäyttöön
- ❌ Kolme kerrosta lisää latenssia verrattuna suoraan ingestointiin
- ❌ Alkuinvestointi suurempi kuin yksinkertaisempi malli

### Vaihtoehto B – Two-tier (Raw + Curated) (hylätty)

- ✅ Yksinkertaisempi rakenne, nopeampi aloittaa
- ❌ Ei erotu liiketoimintalogiikka teknisestä puhdistuksesta
- ❌ Ei skaalaudu kun domaineja tulee lisää

### Vaihtoehto C – Ei tehdä mitään (kukin tiimi päättää itse)

- ❌ Hajanainen arkkitehtuuri, ylläpito kallistuu ajan myötä
- ❌ Tiedon löydettävyys heikkenee
- ❌ Governance mahdotonta

## Seuraukset

- Kaikki uudet dataprojektit luovat kolme Lakehouse-workspacea per domain (bronze, silver, gold)
- Nimeämiskäytäntö on määritelty erillisessä standardissa: `standards/EXAMPLE-taulujen-nimeaminen.md`
- Bronze-kerrokseen ei tehdä transformaatioita – vain tekninen metadata
- Jokaisella kerroksella on oma Service Principal CI/CD-putkea varten
- **Tradeoff:** Yksinkertaisissa käyttötapauksissa kolme kerrosta voi tuntua ylimitoitetulta → projektikohtainen poikkeus mahdollinen (dokumentoitava deviation)

## Viitteet

- [ADO Epic #4421 – Fabric-alustan käyttöönotto](https://dev.azure.com/contoso/dataplatform/_workitems/edit/4421)
- [Microsoft Fabric Medallion Architecture](https://learn.microsoft.com/en-us/azure/databricks/lakehouse/medallion)
- [Standard: Taulujen nimeäminen](../standards/EXAMPLE-taulujen-nimeaminen.md)
