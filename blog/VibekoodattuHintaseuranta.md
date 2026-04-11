---
path: "Omat-sivut-uusiksi2019"
date: "2026-04-11"
title: "Vibekoodausta"
tags: ["ai", "scrapping"]
excert: "Vibekoodausta"
---


# Vibekoodausta - hintaseuranta 


## Työkalut: VsCode, Github Copilot

```powershell 
winget install --id Microsoft.VisualStudioCode

```


Vscode työkaluna helpommin lähestyttävä kuin cli työkalut. Käytetään projektissa
vscoden kautta github copilottia. 

## Projektin startti 

Määritellään mitä ollaan tekemässä


```
Toteutetaan hintaseuranta botti jossa käyttäjä määrittelee urlit joista käydään
katsomassa hintoja. Urlit voivat sisältää useampia tuotteita. Kirjataan ylös
hinnat ja tuotteen nimi/tunniste. Nämä säilötään tiedostoon. Suorituksen jälkeen
verrataan edelliseen tiedostoon ja ilmoitetaan mikäli on tuotteita joissa hinta
on laskenut.

Teknologiat: 

Käytetään playwrightcli:tä, sekä pythonia jos ei ole suoraa parempia ehdotuksia? 
```


Palloteltiin ajatuksia plan modessa toteutuksesta, päädyttiin siihen että
toteutetaan rakenne johon voidaan lisätä scrappereita/sivusto. Suoritus tapahtuu
ilman llm, mutta listään ohjeet kuinka uusia sivustoja varten voidaan lisätä
scrappereita llm:llä.


## Lopputulos

Saatiin toteutettua scripti jolla haetaan tuotteiden tietoa jsoniin, tämän
päälle vois dataa sitten mahdollisesti mallintaa haluamallaan tavalla.
Historian ajot jätetään talteen, viimeisimmässä ajossa esiintyneet uudet
alennukset tulostetaan scriptin suorituksen yhteydessä. 

Lopputuloksen löydät täältä: [https://github.com/ohanhimaki/Hintaseuranta](https://github.com/ohanhimaki/Hintaseuranta)

