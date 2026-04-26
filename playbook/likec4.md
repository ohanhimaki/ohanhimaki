# Likec4 - arkkitehtuuri koodina

[gh:Likec4/likec4](https://github.com/likec4/likec4) 

Monet arkkitehtuuri kuvaukset joihin törmännyt ovat olleet ns. kertaluonteisia.
Se piirretään jossain vaiheessa ja on kuvaus mitä on luovutettu, tai
minkälaiseen toteutukseen projektissa pyritään. Sen jälkeen niihin ei palata,
ennenkö on tarvetta kuvalle ja tässä vaiheessa ympäristö kartoitetaan ja kuva
piirretään uudelleen.

Arkkitehtuurin kuvaus koodina mielestäni tekee siitä helpommin ylläpidettävän,
koska se voidaan asettaa lähelle lähdekoodeja ja sen muutoshistoria on
jäljitettävissä. Myös AI-työkalujen käyttöönottoon se sopii paremmin, koska tätä
koodia voidaan käyttää kuvaamaan isoa kuvaa kontekstissa erittäin pienillä
tokeni määrillä.

Likec4 malli on toteutettu c4model-ideologian pohjalta. c4modelissa ideana on
kuvata ympäristöä usealta eri tasolta. Context, jossa on ylätasolta käyttäjiä,
meidän järjestelmää ja ulkoisia järjestelmiä. Container taso on seuraava syvempi
taso, siinä eritellää systeemin sisältä esimerkiksi tietokannat ja
käyttöliittymät. Kolmas taso on context, siinä mennään taas astetta syvemmälle,
esitellen esimerkiksi sovelluksen autentikointi ja muita pieniä palasia omina
nodeinaan. Neljäs taso on code, joka on sitten jo ihan sovelluksen sisällä
erilaisten metodien suhdetta toisiinsa. c4modelissa on ok käyttää vain tiettyjä
tasoja. likec4 työkalu on rakennettu joustavaksi, ja se sallii useampia
porautumistasoja.   


## Esimerkki-toteutus 

Kansiossa [resources/likec4-example/](../resources/likec4-example/) on
esimerkki-toteutus, jossa olen kuvannut hyvin tyypillistä tilannetta. Ympäristöä
on toteutettu muutaman eri sukupolven toteutuksella, ja kuva kokonaisuudesta
alkaa olla vähän hähmäinen. 

Ulkoasullisesti pyrin toteuttamaan niin, että eri tyyppiset palaset ovat aina
tietyllä värillä, tämän lisäksi seuraan perinteistä tapaa merkitä kirjoitus ja
luku viivat joko yhtenäisellä- tai katkoviivalla.

### Iso kuva:

![index](../resources/likec4-example/images/index.png) 

### Cloud system 

![cloud_system](../resources/likec4-example/images/cloud_system.png) 

### Interaktiivinen dtan kulku ennustamisesta pilviympäristön raportointiin

![forecast to cloud reporting](../resources/likec4-example/images/old_forecast_to_cloud_reporting.png) 
