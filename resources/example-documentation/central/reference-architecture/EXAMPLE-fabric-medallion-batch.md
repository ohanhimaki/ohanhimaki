# Reference Architecture: Fabric Medallion – Batch Ingestion

> **Tyyppi:** Reference Architecture  
> **Versio:** 1.0  
> **Omistaja:** Data Platform -tiimi  
> **Päivitetty:** 2024-10-05

## Tarkoitus

Kuvaa standardin tavan ingestoida dataa ulkoisista lähdejärjestelmistä
Microsoft Fabric Lakehouseen Medallion-mallin mukaisesti.
Käytetään aina kun lähde on batch-pohjainen (ei reaaliaikainen).

## Arkkitehtuurikuva

```
┌─────────────────┐     ┌──────────────┐     ┌────────────────────────────────────────┐
│  Lähdejärjestelmä│     │   Fabric     │     │           Fabric Lakehouse             │
│                 │     │   Pipeline   │     │                                        │
│  SQL Server     │────▶│  (Data       │────▶│  Bronze          Silver        Gold    │
│  REST API       │     │   Factory)   │     │  ─────────       ──────        ─────── │
│  SFTP / CSV     │     │              │     │  Raakadata  ──▶  Puhdas   ──▶  Biz-    │
│                 │     │  Ajastettu   │     │  Delta-fmt       logiikka      logiikka │
└─────────────────┘     │  tai trigger │     │                                        │
                        └──────────────┘     └────────────────────────────────────────┘
                                                              │
                                                    ┌─────────▼──────────┐
                                                    │  Power BI Semantic  │
                                                    │  Model / Reports    │
                                                    └────────────────────┘
```

## Komponentit

| Komponentti | Teknologia | Vastuu |
|---|---|---|
| Ingestion Pipeline | Fabric Data Pipeline (Data Factory) | Datan haku lähdejärjestelmästä, virheenkäsittely |
| Bronze Lakehouse | Fabric Lakehouse + Delta | Raakadatan tallennus sellaisenaan, tekninen metadata |
| Silver Notebook | Fabric Notebook (PySpark) | Validointi, deduplikointi, tyyppimuunnokset |
| Gold Notebook | Fabric Notebook (PySpark/SQL) | Liiketoimintalogiikka, joinit, aggregaatiot |
| Semantic Model | Power BI Dataset (Direct Lake) | Raportointi Gold-kerroksesta |
| Key Vault | Azure Key Vault | Kaikki salaisuudet ja yhteystiedot |

## Dataflow

1. **Ajastin / trigger** – Fabric Pipeline käynnistyy aikataulun (esim. 03:00 UTC) tai eventin perusteella.
2. **Haku lähdejärjestelmästä** – Pipeline lukee datan REST API:sta / SQL Serveriltä / SFTP:ltä. Credentials Key Vaultista.
3. **Bronze-kirjoitus** – Data kirjoitetaan sellaisenaan Delta-tauluun. Lisätään tekninen metadata: `_ingested_at`, `_source_system`, `_pipeline_run_id`.
4. **Silver-prosessointi** – Notebook lukee bronzesta, ajaa validoinnit (null-tarkistukset, tyypit, duplikaatit), kirjoittaa silveriin. Schema enforcement päällä.
5. **Gold-prosessointi** – Notebook lukee silveristä, rakentaa fakta- ja dimensiotaulut liiketoimintalogiikan mukaan.
6. **Semantic Model refresh** – Power BI Direct Lake päivittyy automaattisesti Gold-muutosten jälkeen.

## Ympäristöt (Environments)

| Environment | Tarkoitus | Erityispiirteet |
|---|---|---|
| dev | Kehitys ja testaus | Maskeerattu tai synteettinen data, pipeline ajetaan manuaalisesti |
| test | Integraatiotestaus | Tuotantomainen data (anonymisoitu), ajastettu ajastus päällä |
| prod | Tuotanto | Oikea data, SLA: pipeline valmis klo 06:00 UTC, hälytykset päällä |

> `stage` = datakerros (bronze/silver/gold). `env` = ympäristö (dev/test/prod). Ei sekoiteta.

## Virheenkäsittely

- Pipeline-virhe → Teams-kanava `#data-alerts` saa ilmoituksen
- Bronze-kirjoitus epäonnistuu → data jää lähdejärjestelmään, uudelleenajo seuraavana yönä
- Silver-validointivirhe → virheelliset rivit ohjataan `slv_<domain>_quarantine`-tauluun, ei kaadu pipeline
- Gold-virhe → edellinen onnistunut versio jää voimaan (Delta time travel), hälytetään

## Poikkeaminen tästä mallista

Jos projekti poikkeaa tästä referenssiarkkitehtuurista, dokumentoi syy:
`project/deviations/`-kansioon.

Hyväksytty poikkeus: [DEV-001 – Sales-domain käyttää streaming-ingestointia tilausdatalle](../../project/deviations/EXAMPLE-DEV-001-sales-streaming.md)

## Viitteet

- [ADR-001 – Medallion-arkkitehtuurin valinta](../adr/EXAMPLE-ADR-001-medallion-arkkitehtuuri.md)
- [Standard: Taulujen nimeäminen](../standards/EXAMPLE-taulujen-nimeaminen.md)
- [Microsoft Fabric Pipeline documentation](https://learn.microsoft.com/en-us/fabric/data-factory/)
