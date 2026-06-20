# Integraatio: Salesforce → Bronze Lakehouse (Sales-tilaukset)

> **Tyyppi:** Integration Documentation  
> **Projekti:** Sales-domain Silver & Gold  
> **Päivitetty:** 2025-01-30  
> **Omistaja:** Sales-tiimi / Janne Mäkinen

---

## Yleiskuvaus

Haetaan myyntitilaukset ja niihin liittyvät asiakastiedot Salesforcesta
Microsoft Fabric Bronze Lakehouseen kerran vuorokaudessa.
Data on pohja Sales-domainin raportoinnille (pipeline, revenue, conversion).

## Lähdejärjestelmä

| Ominaisuus | Arvo |
|---|---|
| Järjestelmä | Salesforce Sales Cloud |
| Tyyppi | REST API (Salesforce Bulk API 2.0) |
| Omistaja | Sales Operations / Tiina Leppänen |
| Env | prod (yksi ympäristö, ei erillistä dev-instanssia) |
| Endpoint | `https://contoso.my.salesforce.com/services/data/v59.0/` |

## Kohdejärjestelmä

| Ominaisuus | Arvo |
|---|---|
| Järjestelmä | Microsoft Fabric Lakehouse |
| Kerros | Bronze |
| Workspace | `prod-sales-bronze` |
| Taulut | `brz_sales_orders`, `brz_sales_accounts`, `brz_sales_opportunities` |

## Dataflow

```
Salesforce                   Fabric Pipeline              Bronze Lakehouse
(Bulk API 2.0)               (Data Factory)
     │                             │                            │
     │── Query: Orders updated ──▶ │                            │
     │   since last run            │── Write Delta ──────────▶  │
     │                             │   + metadata               │
     │── Query: Accounts ────────▶ │                            │
     │── Query: Opportunities ───▶ │                            │
```

## Siirrettävä data

| Kohde / Taulu | Salesforce-objekti | Kuvaus | Päivityslogiikka |
|---|---|---|---|
| `brz_sales_orders` | `Order` | Myyntitilaukset | Incremental (`LastModifiedDate > last_run`) |
| `brz_sales_accounts` | `Account` | Asiakastiedot | Incremental (`LastModifiedDate > last_run`) |
| `brz_sales_opportunities` | `Opportunity` | Myyntiputki | Incremental (`LastModifiedDate > last_run`) |

**Tekninen metadata lisätään jokaiseen riviin:**
- `_ingested_at` – timestamp milloin rivi haettiin
- `_source_system` – `salesforce`
- `_pipeline_run_id` – Fabric Pipeline run ID jäljitettävyyttä varten

## Ajastus

| Ympäristö | Aikataulu | SLA |
|---|---|---|
| prod | Joka päivä klo 02:30 UTC | Valmis klo 06:00 UTC |
| test | Manuaalinen käynnistys | Ei SLA:ta |
| dev | Manuaalinen käynnistys | Ei SLA:ta |

## Autentikointi

> Älä lisää avaimia tai salasanoja tähän dokumenttiin.

- **Metodi:** OAuth 2.0 – Connected App + Service Principal
- **Client ID / Secret sijainti:** Azure Key Vault `kv-prod-sales`
  - Secret-nimi: `sf-connected-app-client-id`
  - Secret-nimi: `sf-connected-app-client-secret`
- **Token endpoint:** `https://contoso.my.salesforce.com/services/oauth2/token`

## Virheenkäsittely

- Salesforce API palauttaa 429 (rate limit) → pipeline odottaa 60s ja yrittää uudelleen, max 3 kertaa
- Salesforce API palauttaa 5xx → pipeline kaatuu, lähettää hälytyksen `#data-alerts`
- Yksittäisen rivin schema-poikkeama → rivi ohjataan `brz_sales_quarantine`-tauluun, pääajo jatkuu
- Pipeline ei valmistu klo 04:30 mennessä → automaattinen hälytys `#data-alerts`

## Tunnetut rajoitteet

- Salesforce Bulk API 2.0 rajoittaa 10 000 API-kutsua/vrk – normaalikäytössä käytetään ~400
- Salesforce ei tarjoa muutoslokia – käytetään `LastModifiedDate`-kenttää joka ei kata kaikkia muutoksia (esim. workflow-päivitykset eivät aina päivitä tätä kenttää)
- Ei erillistä Salesforce dev-ympäristöä → dev-testaus tehdään pienellä datamäärällä prod-Salesforcesta

## Viitteet

- [Salesforce Bulk API 2.0 dokumentaatio](https://developer.salesforce.com/docs/atlas.en-us.api_asynch.meta/api_asynch/)
- [Runbook: Pipeline epäonnistuu](../runbooks/EXAMPLE-sales-pipeline-epäonnistuu.md)
- [ADR-003: Silver-historian säilytys](../adr/EXAMPLE-ADR-003-silver-historia-scd2.md)
