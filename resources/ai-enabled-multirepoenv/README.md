# AI-kehitys monirepo ympäristössä

Tässä kansiorakenteessa yksi esimerkki siitä kuinka tiiminä voitaisiin toteuttaa tehokasta kehitystyötä multi-repo ympäristössä. 

## Hyödyt monirepo mallin kehittämisessä

- Saadaan automaattisesti tietoa upstreamista tähän projektiin
- Voidaan välttää ai-kontekstin, best-practices ja skillsien monistaminen.  


## Ongelmia 

Mitä ongelmia tässä nyt sitte on? ota vaan kaikki ja press play?

Kontekstin entrypoint (AGENTS.md, CLAUDE.md) pitäis saada juureen, mutta silti versionhallintaan. 
Ei välttämättä haluta kloonata repositoryjä toisten repositoryjen kansiorakenteen sisälle, koska se aiheutta omia ongelmiansa:
pitää joko lisätä kansio .gitignore-tiedostoon (rikkoo työkaluissa mm. hakutoiminnallisuuksia) tai lisätä submodulena (aiheuttaa ehkä ylimääräistä päänvaivaa)

## Ratkaisu


```
aidrivenworkflow/
├── local-dev/              # kehittäjän oma kansio, voi olla local git jos haluaa
├───── tasks/               # sisältönä localissa lyhyen elinkaaren tavaraa, esim taskien ideointia jne. 
├── central-context/        # kehitystiimin yhteinen konteksti ai työkaluille
├───── root-ai-files/       # Tänne tulee esim AGENTS.md, joka symlinkillä osoitetaan projektin juureen 
└── finance/                # Itse tuotteen lähdekoodit
```
