---
path: "Omat-sivut-uusiksi2019"
date: "2019-01-18"
title: "Omat sivut uusiksi 2019"
tags: ["css", "wordpress"]
excert: "Päivitetään omien sivujen ulkonäköä css treeniksi"
---

## Sivun ulkonäön päivitys. Uusi teema vanilla css:llä.

Päätin tänään ehostaa omia sivuja ja kehittää omaa frontend osaamista. Ollut pitkään jo tarkoitus tehdä jotain ulkoasua ilman frameworkkia. Itsellä ei tähän mennessä löytynyt oikeastaan minkäänlaista näyttöä css-osaamisesta. Projekteissa on toki joutunut tekemään aina pieniä muutoksia sinne tänne, mutta päätin tehdä nyt pitkästä aikaa alusta loppuun itse, varsinkin kun olen kuullut css:n uusista ominaisuuksista niin paljon hyvää.

### WordPress kehitysympäristö

Alkuvuodesta näin ilmoituksen jossa haettiin WordPress-kehittäjää Seinäjoelle. Sen innoittamana kävin itselleni läpi mitä osaan ja mitä en osaa aiheeseen liittyen. Muistin että kehitysympäristöä ei ole itsellä ennen ollut, vaan sivuja on muokattu ihan livenä. Näin tässä tilaisuuden nyt käyttää ja opetella dockeria. Docker mahdollistaa konttien(container) eli pakettien rakentamisen, paketeissa on sovellukset kaikkien riippuvuuksien kanssa valmiina. Näistä konteista sitten rakennellaan itselle tarvittava alusta. Worpdressiä varten otin käyttöön WordPress-kontin, ja tietokannaksi mariadb-kontin. Mariadb:lle pitää antaa tietokannan nimi ja kirjautumisasetukset. Tämän jälkeen avaan WordPress-kontin osoitteen ja siellä wordpress kyselee asetuksia. Ajan asetukset läpi ja tuon sinne omilta sivuilta artikkelit, jotta ei tarvi lorem ipsumeilla testata.

### Ulkoasun suunnittelu

Yleensä projektini ovat menneet toiminnallisuudet edellä ja ulkoasu rakennettu sitten siihen kylkeen. Sivuilla joissa ei ole mitään työkalua tai muuta vastaavaa, vaan pitää saada info helposti ja houkuttelevan näköisesti esille, ulkonäöstä nouseekin tärkein ominaisuus. Ulkoasun suunnittelu on aina jäänyt vähän taka-alalle mutta nyt olen tutustunut Figma ja Adobe XD sovelluksiin. Näistä päädyin käyttämään Figmaa ja lähdin sillä suunnittelmaan sivuston ulkoasua. Sivuston sisältö vastaa edellistä, siihen tuli etusivu, artikkeli-lista ja yksittäinen artikkeli. Halusin teemaan valkoista tekstiä tummalla taustalla, etusivulle ison kuvan ja artikkeli-listaan useamman uutisen vierekkäin.

[Linkki figmalla tehtyihin luonnoksiin](https://www.figma.com/proto/00mXremn2Onhsj0KLgc9xeAt/Oma-wp-teema-Csstreeni?node-id=1%3A3&scaling=scale-down-width&redirected=1)

### Toteutus

Suunnitelmaa alettiin toteuttaa, otin pohjalle MD:llä aiemmin toteuttaman teemani jättäen lähinnä php-kohdat jäljelle. Työ sujui sujuvasti eikä missään vaiheessa jääty pahemmin mihinkään jumiin. Etusivun ison kuvan kanssa poikettiin vähän suunnitelmista ja olen tyytyväinen lopputulokseen. Myös dockerilla toteutettu kehitysympäristö palveli ongelmitta, ja sivustojen siirrossa tuotantoon ei ilmennyt ongelmia.

### Loppu pohdintaa

Uudesta ilmeestä tuli mielestäni hyvännäköinen. Tarkemmin kun koko projektia miettii niin kyllähän tämä jäi vähän yksinkertaiseksi vielä, ja jotain maustetta olisi voinut lisätä esitelläkseen paremmin omia frontend-taitoja. Tässä projektissa huomasin että ulkoasun suunnittelussa olisi itsellä vielä paljon kehityttävää, ja mikäli alalle päätyisi töihin niin jonkinlainen kokoelma ulkoasun erilaisista vaihtoehdoista auttamaan suunnittelussa voisi olla tehokas apu.
