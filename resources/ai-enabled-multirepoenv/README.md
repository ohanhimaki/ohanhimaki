# AI-kehitys monirepo ympäristössä

Tässä kansiorakenteessa yksi esimerkki siitä kuinka tiiminä voitaisiin toteuttaa tehokasta kehitystyötä multi-repo ympäristössä.

## Hyödyt

- Saadaan automaattisesti tietoa upstreamista tähän projektiin
- Voidaan välttää ai-kontekstin, best-practices ja skillsien monistaminen.

## Ongelmia

Mitä ongelmia tässä nyt sitte on, miksei voi vaan kloonata kaikkia repoja ja alkaa töihin?

Kontekstin entrypoint (AGENTS.md, CLAUDE.md) pitäis saada juureen, mutta silti versionhallintaan.
Ei välttämättä haluta kloonata repositoryjä toisten repositoryjen kansiorakenteen sisälle, koska se aiheutta omia ongelmiansa:
pitää joko lisätä kansio .gitignore-tiedostoon (rikkoo työkaluissa mm. hakutoiminnallisuuksia) tai lisätä submodulena (aiheuttaa ehkä ylimääräistä päänvaivaa).

## Ratkaisu

Lähdetään rakentamaan tiedostorakennetta esimerkiksi tähän malliin.

```
aidrivenworkflow/
├── local-dev/              # kehittäjän oma kansio, voi olla local git jos haluaa
│   └─ tasks/               # sisältönä localissa lyhyen elinkaaren tavaraa, esim taskien ideointia jne.
│
├── central-context/        # kehitystiimin yhteinen konteksti ai työkaluille
│   ├─ README.md            # Kuvaus projektista, kehitys envin setup ohjeet.
│   ├─ setup.ps1            # Scriptit root-ai-files tiedostojen shadowcopystä työkansion roottiin.
│   ├─ setup.sh             # setup scriptit voivat sisältää myös mm. ympäristön repojen kloonaamisen komennot.
│   └─ root-ai-files/       # Tänne tulee esim AGENTS.md, joka symlinkillä osoitetaan projektin juureen
│     ├─ skills/            # täällä on ympäristön yhteisiä skillsejä,
│     │  ├─── definition-of-done-and-next-steps.md            # kehitystiimin yhteisiin käytänteisiin ohjaavia ai-skillsejä
│     │  └─── best-practices.md
│     └───── AGENTS.md          # Tänne tulee esim AGENTS.md, joka symlinkillä osoitetaan projektin juureen
│
├── company-portal/         # Ympäristön lähdekoodeja
│   ├─ docs/                # sisältävät repokohtaisen dokumentaation
│   └─ AGENTS.md            # voi sisältää omia conteksti-filuja.
└── finance/                # Ympäristön lähdekoodeja, domainin monoliitti repo
```

Tämän jälkeen central-context/setup.ps1 suorittamalla, saadaan aidrivenworkflow kansion juureen AGENTS.md, sekä yhteiset skillsit.
