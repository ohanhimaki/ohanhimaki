---
name: best-practices
description: Tiimin yhteiset koodaus- ja arkkitehtuurikäytänteet monirepo-ympäristössä. Käytä kun kirjoitat, muokkaat tai reviewaat koodia missä tahansa ympäristön repossa.
---

# Skill: Best Practices

Käytä tätä skilliä kun kirjoitat tai muokkaat koodia missä tahansa tämän workflow-ympäristön
repossa. Sisältö on tiimin yhteinen taso — repokohtaiset lisäykset kuuluvat kunkin repon
omaan AGENTS.md/docs-kansioon, ei tänne.

## Milloin käyttää

- Aina kun teet koodimuutoksen (uusi ominaisuus, bugikorjaus, refaktorointi).
- Kun arvioit toisen tekemää muutosta (code review).

## Käytänteet

1. **Pienet, kohdennetut muutokset.** Älä muokkaa koodia joka ei liity tehtävään.
2. **Testit ennen valmiiksi merkintää.** Aja olemassa olevat testit, älä lisää uutta
   test-frameworkia ellei se ole tarpeen.
3. **Cross-repo-muutokset.** Jos muutos koskee rajapintaa, jota toinen repo kuluttaa,
   mainitse se PR-kuvauksessa ja linkkaa toiseen repoon jos mahdollista.
4. **Dokumentaatio.** Päivitä README/docs samassa PR:ssä jos käyttäytyminen muuttuu.
5. **Salaisuudet.** Älä koskaan kirjoita tokeneita/avaimia koodiin tai committeihin.
