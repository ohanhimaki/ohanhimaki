# Repository-rakenne: käytännön esimerkki

Tämä dokumentti kuvaa, miten dokumentaatio ja lähdekoodi jakautuvat eri repositoryihin
käytännön BI/Fabric-ympäristössä – ja miten nykytilannetta voisi kehittää.

---

## Nykytilanne (tyypillinen tilanne)

```
ADO / GitHub
│
├── standards-repo/              ← Standardit + business-kuvaukset (sekaisin)
│   ├── standards/
│   └── business-descriptions/
│
├── common-bronze-repo/          ← Kaikki bronze-työtilat yhdessä
│   └── src/
│
├── sales-workspace-repo/        ← Sales silver + gold
│   ├── main (prod)
│   ├── dev/sales-silver
│   ├── dev/sales-gold
│   ├── test/sales-silver
│   └── test/sales-gold
│
├── finance-workspace-repo/      ← Finance silver + gold
│   ├── main (prod)
│   ├── dev/finance-silver
│   └── dev/finance-gold
│
└── hr-workspace-repo/           ← HR silver + gold
    └── ...
```

### Ongelmat nykymallissa

| Ongelma | Vaikutus |
|---|---|
| Standardit ja business-kuvaukset samassa repossa | Epäselvä omistajuus, sekoittaa teknisen ja liiketoimintadokumentaation |
| Dev-ympäristöjen lähdekoodi hajallaan brancheissa eri repoissa | Vaikea nähdä kerralla kaikkia dev-työtiloja – esim. cross-workspace debuggaus työlästä |
| Ei selkeää "platform knowledge" -kotia | ADR:t, runbookit, governance joko puuttuvat tai ovat epäloogisessa paikassa |
| Bronze-repo ei sisällä dokumentaatiota | Operatiivinen tieto (runbookit, integraatiot) on jossain muualla tai ei ollenkaan |

---

## Suositeltu malli: Central repository + selkeä jako

```
ADO / GitHub
│
├── 📁 platform-central-repo/       ← UUSI: Kaikki platform-tason tieto
│   ├── governance/
│   ├── standards/
│   ├── adr/                        ← Yhteiset arkkitehtuuripäätökset
│   ├── reference-architecture/
│   └── AGENTS.md                   ← AI-context koko platformille
│
├── 📁 common-bronze-repo/          ← Ennallaan, mutta lisätään docs/
│   ├── src/
│   └── docs/
│       ├── adr/
│       ├── runbooks/
│       └── integrations/           ← Integraatiodokumentit lähellä koodia
│
├── 📁 sales-workspace-repo/        ← Sales silver + gold (ennallaan)
│   ├── src/
│   └── docs/
│       ├── adr/
│       ├── runbooks/
│       ├── integrations/
│       ├── deviations/
│       └── business-description.md ← Business-kuvaus TÄHÄN, ei platform-repoon
│
└── 📁 finance-workspace-repo/      ← Sama rakenne kuin sales
    ├── src/
    └── docs/
        └── ...
```

### Miksi erillinen `platform-central-repo`?

- **Yksi paikka** kaikelle yhteiselle tiedolle – standardit, governance, yhteiset ADR:t
- **Selkeä omistajuus** – platform-tiimi omistaa, projektitiimit viittaavat
- **AI-context** – `AGENTS.md` juuressa antaa AI-työkaluille kontekstin koko ekosysteemistä
- Projektirepot viittaavat central-repoon, eivät kopioi standardeja

---

## Branch-strategia ja dev-työtilat

### Nykymalli (ongelma)

```
sales-workspace-repo
  ├── main
  ├── dev/sales-silver      ← dev-silver
  ├── dev/sales-gold        ← dev-gold
  ├── test/sales-silver
  └── test/sales-gold
```

> **Ongelma:** Et voi helposti katsoa kaikkia dev-työtiloja kerralla,
> koska ne ovat eri brancheissa eri repoissa.

### Vaihtoehto A – Ympäristöbranchit (nykyinen, parannettu)

Pidä nykyinen branch-rakenne, mutta lisää **workspace-taso hakemistorakenteeseen**:

```
sales-workspace-repo / dev-branch
  ├── silver/
  │   └── src/
  └── gold/
      └── src/
```

Nyt `dev`-branchissa on molempien työtilalähdekoodi, ei erillisissä brancheissa.

> ✅ Yhdellä `git checkout dev` näet kaikki dev-työtilat  
> ✅ PR-flow selkeä: feature → dev → test → main  
> ❌ Vaatii muutoksen nykyiseen branch-rakenteeseen

### Vaihtoehto B – Environment-konfiguraatio koodissa (suositus)

Yksi branch per feature/fix, ympäristö määritellään **konfiguraatiolla tai pipeline-parametrilla**:

```
sales-workspace-repo
  ├── main
  ├── feature/xxx
  └── fix/yyy

Pipeline määrittelee target-env:
  - PR → deploy to dev
  - Merge to main → deploy to test → prod
```

```
silver/
  ├── src/
  └── config/
      ├── dev.json
      ├── test.json
      └── prod.json
```

> ✅ Yksi branch = yksi muutos, ei ympäristöhajautusta  
> ✅ Kaikki lähdekoodi näkyy kerralla  
> ✅ Vastaa moderneja DevOps-käytäntöjä  
> ❌ Vaatii enemmän pipeline-työtä jos ympäristöt ovat hyvin erilaisia

---

## Dokumentaation sijoittuminen: yhteenveto

| Dokumentti | Sijainti | Perustelu |
|---|---|---|
| Platform-standardit | `platform-central-repo/standards/` | Yksi paikka, kaikki viittaavat |
| Governance | `platform-central-repo/governance/` | Platform-tiimin omistama |
| Yhteiset ADR:t | `platform-central-repo/adr/` | Koskee kaikkia työtiloja |
| Reference architecture | `platform-central-repo/reference-architecture/` | Yhteinen pohja |
| Business-kuvaus | `<business-area>-workspace-repo/docs/business-description.md` | Lähellä koodia, tiimi omistaa |
| Projektin ADR:t | `<business-area>-workspace-repo/docs/adr/` | Projektikohtainen päätökset |
| Runbookit | `<business-area>-workspace-repo/docs/runbooks/` | Lähellä koodia jota operoidaan |
| Integraatiodokumentit | Integroivassa repossa, `docs/integrations/` | Lähellä integraatiokoodia |
| Poikkeukset standardista | `<business-area>-workspace-repo/docs/deviations/` | Projekti omistaa poikkeuksensa |
| Bronze-integraatiot | `common-bronze-repo/docs/integrations/` | Lähellä ingestioita |

---

## Muut työvälineet: mitä minne?

| Työkalu | Käyttötarkoitus | Mitä EI sinne |
|---|---|---|
| **Git-repo (tämä rakenne)** | Lähdekoodi + kaikki tekniset dokumentit | Salasanat, henkilödata |
| **Confluence / SharePoint** | Liiketoimintaviestintä, esitykset, kokouspöytäkirjat | Tekninen dokumentaatio (vanhenee) |
| **ADO Wiki** | Kevyt linkitys – "katso repo X tiedosto Y" | Ei synkronoidu koodin kanssa → vanhentuu |
| **Key Vault / Pipeline variables** | Secrets, yhteysmerkkijonot | Koskaan koodiin tai dokumentaatioon |
| **LikeC4 / Excalidraw** | Arkkitehtuurikaaviot (versionhallittuna repossa) | Confluence (ei versionhallittu) |

---

## Suositeltu ensiaskel nykytilasta

Jos et halua tehdä kaikkea kerralla, aloita tästä:

1. **Luo `platform-central-repo`** – siirrä standardit sinne
2. **Lisää `docs/`-kansio** jokaiseen olemassa olevaan repoon (ei tarvitse täyttää heti)
3. **Siirrä business-kuvaukset** standards-reposta → business-area-repoihin
4. **Kirjoita yksi runbook** ensimmäisestä oikeasta incidentistä → syntyy luonnostaan
5. **Päätä branch-strategiasta** – vaihtoehto A tai B – ja dokumentoi se ADR:nä
