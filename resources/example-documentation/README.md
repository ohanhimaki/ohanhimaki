# BI-ympäristön dokumentaatio – esimerkkirakenne

Tämä kansio demonstroi, miltä hyvä BI/DE/Fabric-ympäristön dokumentaatio voisi näyttää.

## Miksi tämä rakenne?

Moderni BI-dokumentaatio ei ole pelkästään ihmisille tarkoitettua wiki-sisältöä.
Se on myös **AI-agenteille ja kehitystyökaluille tarkoitettua kontekstia**.

Hyvä dokumentaatio on:
- **Versionhallittua** – muutoshistoria näkyy Gitissä
- **Plaintext/Markdown** – koneluettavaa, hakukelpoista
- **Rakenteista** – selkeät tyypit ja mallipohjat
- **Lähellä toteutusta** – projektikohtainen tieto elää repossa
- **Helposti löydettävää** – tiedostonimet ja kansiorakenne kertovat sisällön

---

## Dokumentaatiotyypit

| Tyyppi | Tarkoitus | Sijainti |
|---|---|---|
| **ADR** (Architecture Decision Record) | Miksi päätös tehtiin, tradeoffit | Central + projekti |
| **Standard** | Mikä on oletettu toimintamalli | Central |
| **Developer Guide** | Miten kehitetään | Central + projekti |
| **Runbook** | Miten operoidaan, miten korjataan | Projekti |
| **Architecture diagram** | Miten järjestelmä rakentuu | Central + projekti |
| **Deviation** | Poikkeus yhteisestä standardista | Projekti |
| **Integration doc** | Rajapinnat, lähdejärjestelmät | Projekti |

---

## Kaksitasoinen malli

```
central/          ← Platform knowledge, yhteinen tieto
  governance/     ← Governance-säännöt, roolit, vastuut
  standards/      ← Teknologiastandarterit, naming conventions
  adr/            ← Yhteiset arkkitehtuuripäätökset
  reference-architecture/  ← Referenssiarkkitehtuurit

project/          ← Projektikohtainen tieto (elää omassa repossa)
  adr/            ← Projektin omat arkkitehtuuripäätökset
  runbooks/       ← Operatiiviset toimintaohjeet
  integrations/   ← Integraatiokuvaukset
  deviations/     ← Poikkeukset yhteisistä standardeista
```

**Central** sisältää sen, mitä kaikki projektit noudattavat.
**Project** sisältää vain projektikohtaisen tiedon ja poikkeukset – ei kopioida standardeja.

---

## Terminologiasta

- Käytä `env` / `environment` ympäristöistä (dev, test, prod)
- Käytä `stage` / `staging` pipeline- tai datakerrosmerkityksiin (bronze, silver, gold)

Tämä vähentää ambiguiteettia sekä ihmisille että AI-työkaluille.

---

## Tavoitetila: AI-enabled engineering workflow

```
Code
+ Infra
+ Architecture (LikeC4 / C4 Model)
+ ADRs
+ Runbooks
+ Standards
+ Operational knowledge
= yksi versionhallittava, AI-readable kokonaisuus
```
