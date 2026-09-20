# AGENTS.md

Tämä tiedosto on symlink `central-context/root-ai-files/AGENTS.md` -tiedostoon, ja se on
kaikkien tässä workflow-kansiossa toimivien AI-agenttien (Copilot, Claude, ym.) yhteinen
lähtökonteksti. Sisältöä muokataan **vain** central-context-repossa, ei symlinkin kautta.

## Projektikokonaisuus

Tämä on monirepo-kehitysympäristö, jossa useampi domain-repo (esim. `company-portal`,
`finance`) kehitetään rinnakkain. Jokaisella repolla on oma AGENTS.md/README omalle
domain-kontekstilleen — tämä tiedosto kuvaa vain koko ympäristöä koskevat yhteiset säännöt.

## Yleiset periaatteet

- Älä koskaan committaa salaisuuksia (tokenit, avaimet, connection stringit).
- Ennen muutosten tekoa toiseen repoon, tarkista sen oma AGENTS.md/README kontekstiksi.
- Cross-repo-muutokset (esim. rajapintamuutos, joka koskee montaa repoa): kuvaa muutos
  ensin `local-dev/tasks/`-kansioon ennen toteutusta.
- Käytä yhteisiä skillsejä `skills/`-kansiosta (symlinkattu `root-ai-files/skills/`) aina
  kun tehtävä osuu niiden kuvaukseen.

## Skillsit

- `skills/best-practices.md` — koodaus- ja arkkitehtuurikäytänteet koko ympäristölle.
- `skills/definition-of-done-and-next-steps.md` — milloin tehtävä on valmis ja miten
  raportoidaan seuraavat askeleet.

## Ympäristön rakenne

Katso `central-context/README.md` täydelliselle kansiorakenteen kuvaukselle ja
`central-context/setup.ps1` / `setup.sh` ympäristön alustusohjeille.
