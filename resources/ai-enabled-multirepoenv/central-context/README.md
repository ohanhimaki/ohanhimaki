# Central Context

Tämä kansio on tiimin yhteinen AI-konteksti multirepo-ympäristölle. Sisältö on
versionhallinnassa (oma repo tai osa emorepoa), ja `setup.ps1`/`setup.sh` levittää
sen `aidrivenworkflow/`-juureen symlinkkeinä.

## Kansiorakenne

```
central-context/
├── README.md            # tämä tiedosto
├── setup.ps1            # Windows/PowerShell setup-skripti
├── setup.sh             # Linux/macOS/bash setup-skripti
├── repos.json           # valinnainen lista kloonattavista repoista (name + url)
└── root-ai-files/
    ├── AGENTS.md         # symlinkataan aidrivenworkflow/AGENTS.md
    └── skills/           # symlinkataan aidrivenworkflow/skills/
        ├── best-practices/
        │   └── SKILL.md
        └── definition-of-done-and-next-steps/
            └── SKILL.md
```

## Käyttöönotto

Ensimmäinen kertaalleen kloonaus:

```powershell
git clone <central-context-repo-url> aidrivenworkflow/central-context
cd aidrivenworkflow/central-context
sudo pwsh -File ./setup.ps1 -CloneRepos   # symlinkit + repos.json:in kloonaus
```

```bash
git clone <central-context-repo-url> aidrivenworkflow/central-context
cd aidrivenworkflow/central-context
./setup.sh --clone
```

Pelkkä AGENTS.md/skills-symlinkkien päivitys (esim. skillsejä on lisätty):

```powershell
sudo pwsh -File ./setup.ps1 -Force
```

## Huomioita

- **Windows-symlinkit** vaativat joko admin-oikeudet tai Developer Mode -asetuksen päällä
  (`Settings > Update & Security > For developers`). Helpoin tapa: aja skripti
  `sudo pwsh -File ./setup.ps1` (Windows 11:n sisäänrakennettu `sudo`). Jos symlink silti
  epäonnistuu, skripti fallbackaa kopiointiin — kopio ei tällöin päivity automaattisesti
  central-contextin muutoksista, joten `-Force`-uudelleenajo tarvitaan päivityksiin.
- **Suhteellinen sijainti**: symlinkit osoittavat central-context-kansion sisään, joten
  central-context on aina oltava suoraan `aidrivenworkflow/`-juuren alla.
- **repos.json** on esimerkki — muokkaa oman tiimin repoilla, tai jätä `-CloneRepos`/`--clone`
  pois jos repot kloonataan jotain muuta kautta.
