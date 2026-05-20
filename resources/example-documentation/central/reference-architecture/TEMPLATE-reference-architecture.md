# Reference Architecture: [Arkkitehtuurin nimi]

> **Tyyppi:** Reference Architecture  
> **Versio:** 1.0  
> **Omistaja:** [Tiimi]  
> **Päivitetty:** YYYY-MM-DD

## Tarkoitus

Mitä tämä referenssiarkkitehtuuri kuvaa? Milloin sitä käytetään?

## Arkkitehtuurikuva

> Lisää tähän C4/LikeC4-kaavio tai Excalidraw-linkki.
> Vaihtoehtoisesti ASCII-kaavio nopeaan dokumentointiin.

```
[Lähde A] --> [Ingestion Layer] --> [Bronze] --> [Silver] --> [Gold] --> [Raportointi]
```

## Komponentit

| Komponentti | Teknologia | Vastuu |
|---|---|---|
| Ingestion | ... | Datan haku lähdejärjestelmistä |
| Bronze | ... | Raakadata, ei transformaatioita |
| Silver | ... | Puhdistettu, validoitu data |
| Gold | ... | Liiketoimintalogiikka, aggregaatiot |

## Dataflow

Kuvaa datan kulku vaihe vaiheelta.

1. **Ingestion** – Data haetaan lähdejärjestelmästä X ajastetusti / event-pohjaisesti.
2. **Bronze** – Data tallennetaan sellaisenaan, vain tekninen metadata lisätään.
3. **Silver** – Validointi, deduplikointi, schema enforcement.
4. **Gold** – Liiketoimintatransformaatiot, joinit, aggregaatiot.
5. **Serving** – Raportit ja dashboardit lukevat Gold-kerroksesta.

## Ympäristöt (Environments)

| Environment | Tarkoitus | Erityispiirteet |
|---|---|---|
| dev | Kehitys | Synteettinen tai maskeerattu data |
| test | Testaus / QA | Tuotantomainen data, rajattu |
| prod | Tuotanto | Oikea data, SLA voimassa |

> Huom: `stage` viittaa datakerrosmalliin (bronze/silver/gold), ei ympäristöön.

## Poikkeaminen tästä mallista

Jos projekti poikkeaa tästä referenssiarkkitehtuurista, dokumentoi syy:
`project/deviations/` -kansioon.

## Viitteet

- [Linkki teknologian dokumentaatioon]
- [Liittyvät ADR:t]
