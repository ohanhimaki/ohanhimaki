# Integraatio: [Lähdejärjestelmä] → [Kohdejärjestelmä]

> **Tyyppi:** Integration Documentation  
> **Projekti:** [Projektin nimi]  
> **Päivitetty:** YYYY-MM-DD  
> **Omistaja:** [Tiimi/Henkilö]

---

## Yleiskuvaus

Lyhyt kuvaus integraatiosta: mitä dataa siirretään, mistä, minne ja miksi.

## Lähdejärjestelmä

| Ominaisuus | Arvo |
|---|---|
| Järjestelmä | [Nimi] |
| Tyyppi | REST API / SFTP / Database / Event / ... |
| Omistaja | [Tiimi] |
| Env | dev / test / prod |
| Endpoint / Yhteys | `https://...` tai `server/db` |

## Kohdejärjestelmä

| Ominaisuus | Arvo |
|---|---|
| Järjestelmä | [Nimi] |
| Kerros | Bronze / Silver / Gold |
| Sijainti | [workspace/container/taulun nimi] |

## Dataflow

```
[Lähde] --(protokolla/formaatti)--> [Ingestion] --> [Bronze taulut]
```

## Siirrettävä data

| Kohde / Taulu | Lähde | Kuvaus | Päivityslogiikka |
|---|---|---|---|
| `bronze_orders` | `orders`-taulusta | Tilaukset | Full load / Incremental (updated_at) |

## Ajastus

| Ympäristö | Aikataulu | Huomiot |
|---|---|---|
| prod | Joka päivä klo 03:00 UTC | |
| test | Manuaalinen | |

## Autentikointi

> Älä dokumentoi salasanoja tai avaimia tähän. Viittaa Key Vaultiin tai secrets-hallintaan.

- **Metodi:** Service Principal / API Key / OAuth / ...
- **Secrets sijainti:** [Key Vault / ympäristömuuttuja / ...]

## Virheenkäsittely

Mitä tapahtuu epäonnistumisen yhteydessä? Uudelleenyritykset, hälytykset.

## Tunnetut rajoitteet

- Rajoite 1 (esim. API rate limit 1000 req/min)
- Rajoite 2

## Viitteet

- [Lähdejärjestelmän API-dokumentaatio]
- [Liittyvä runbook](../runbooks/)
- [Liittyvä ADR](../adr/)
