## Git diff vinkit 

  Vain tiedostonimet:

   git diff --name-only main..feature-branch

  Tiedostot + muutostyyppi (Added/Modified/Deleted):

   git diff --name-status main..feature-branch

  Jos haluat rajata vain tiettyyn kansioon:

   git diff --name-only main..feature-branch -- repos/projecti-kansio/


git diff --name-only origin/ABC-1-testing..origin/prod -- ws/workspace-folder

## Git checkout tiedosto 

Hae toisesta branchistä tiedosto nykyiseen: 
```bash
git checkout toisen-branchin-nimi -- polku/tiedostoon.txt

```


Neovimiillä voidaan vielä yksinkertaistaa käyttäen hyödyksi % merkkiä, joka syöttää komentoon auki olevan tiedoston nimen:  
```bash
!git checkout toisen-branchin-nimi -- %

```
