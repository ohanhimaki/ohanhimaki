# Ripgrep

Ripgrep(rg) työkalu etsii tiedostoista tekstiä.


## Parametrit

- `-i` parametrilla ei oteta huomioon kirjasinkokoa
- `-uuu` 1kpl: gitignore tiedostoja ei rajata pois, 2: näytetään myös piilotetut parametrilla näytetään gitignoretut tiedostot, ja 3: etsi myös symlinkeistä


## Esimerkkejä

Etsi tiedostonimellä, näytä vain tiedostot

`rg --files -g ".platform" <polku> ` 

