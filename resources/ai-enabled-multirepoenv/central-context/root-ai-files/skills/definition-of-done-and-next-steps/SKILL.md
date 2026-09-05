---
name: definition-of-done-and-next-steps
description: Milloin tehtävä lasketaan valmiiksi ja miten raportoidaan seuraavat askeleet. Käytä ennen tehtävän/PR:n merkitsemistä valmiiksi tai kun raportoit tilannetta.
---

# Skill: Definition of Done and Next Steps

Käytä tätä skilliä kun päätät tehtävän valmiiksi tai raportoit tilannetta käyttäjälle/tiimille.

## Milloin käyttää

- Ennen kuin merkitset tehtävän/PR:n valmiiksi.
- Kun tehtävä jää kesken ja pitää kuvata mitä puuttuu.

## Definition of Done

Tehtävä on valmis kun:

- [ ] Koodi toteuttaa pyydetyn muutoksen kokonaan (ei osittaisratkaisua).
- [ ] Olemassa olevat testit/lintit ajettu ja menevät läpi.
- [ ] Muutos ei riko toisten repojen tunnettuja integraatioita (jos cross-repo-vaikutus,
      mainittu PR:ssä).
- [ ] Dokumentaatio (README/AGENTS.md/docs) päivitetty jos käyttäytyminen muuttui.
- [ ] Ei jäänyt debug-koodia, kommentoituja koodinpätkiä tai TODO:ita ilman selitystä.

## Next steps -raportointi

Kun tehtävä valmis tai keskeytyy, kerro lyhyesti:

1. Mitä tehtiin (1-2 lausetta).
2. Mitä jäi tekemättä / mitä pitää seurata (jos jotain).
3. Vaatiiko joku toinen repo/tiimi toimenpiteitä tämän muutoksen takia.
