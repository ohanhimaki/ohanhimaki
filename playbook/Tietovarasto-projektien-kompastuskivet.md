# Tietovarasto - Projektien kompastuskivet 

## Lähde vaihtuu

Lähteen vaihtuminen voi olla joko erpistä toiseen, tai integraatiovälineen vaihtuminen excelistä apiin. Samanlaiset ongelmat voivat riivata. Järjestelmän vaihtumisessa yleensä varaudutaan muutokseen paremmin, mutta mikäli integraatiossa tulee vain muutos excelin sijaan apista, voidaan se arvioida paljon kevyemmäksi operaatioksi kuin mitä se lopulta on.

### Mitä tulee ottaa huomioon:
- Puuttuvat tiedot. 
    - Ylätasolla voi vaikuttaa että samat tiedot tarjotaan kuin aiemminkin, todellisuudessa historiaa on voitu karsia, jotain kenttiä jopa poistaa jne.
- Uudet tiedot 
    - Hyvä kartoittaa aikaisessa vaiheessa mitä näille tulisi tehdä.
- Historia, haetaanko kaikki tapahtumat uudesta lähteestä vai vain x-päivästä eteenpäin 
    - Voi olla isoja vaikutuksia, varsinkin yhdistettynä kahteen edelliseen kohtaan. 
    - Usein voidaan helpottaa rajaamalla -> Tuodaan uudesta lähteestä muutokset vain tietystä hetkestä eteenpäin.

