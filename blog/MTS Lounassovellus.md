---
path: "MTSSovellus"
date: "2020-01-09"
title: "MTS Lounassovellus"
tags: ["Angular", "Express", "NodeJS"]
excert: "Mitäs tänään syötäisiin? Vihdoin tähän kysymykseen on ratkaisu, lounassovellus arpoo päivän lounaspaikan. Toteutettu NodeJS, express, Angular, Angular Material UI ja hostattu herokuun."
repo: "ohanhimaki/mita-tanaan-syotaisiin-web"
---

## MTS Lounassovellus

Sovellus hakee lounaslistoja netistä sovelluksen omaan kantaan. Päivittäin arvotaan lounas luettujen listojen ja käsinsyötettyjen ravintoloiden joukosta. Sovelluksella pystyy myös selaamaan lounaslistoja nimen tai päivän perusteella.

Kantana on herokusta helposti projektille liitettävä postgres. Kantaa pystyy helposti käsittelemään esimerkiksi Azure data studiolla. Yksi projektin kehitysideoista olisi pystyttää docker tarjoamaan kehityskannat.

Express vastaa sovelluksen kommunikoinnista kannan kanssa. Sovellukselle on toteutettu muutamia automaattitestejä Mochalla ja chailla.

Frontendi on toteutettu Angular + Angular Material UI:lla. Frontille on myös tehty muutamia testejä.

Sovellus koostuu kolmesta sivusta. Etusivulla on Päivänlounas ja lounashistoriaa.

Toinen lounasinfoa tarjoava sivu on Listat-sivu, täältä löytyy kaikki lounaslistat, niitä voidaan selata joko päivän tai ravintolan perusteella.

Kolmas sivu on Admin sivu, täältä pystyy lisäämään uusia ravintoloita ja muokkaamaan vanhoja, aloittamaan lounaslistojen haun, muokata käsinylläpidettäviä listoja ja muokata ravintoloiden genrejä.

Sovellukseen otetaan mielellään pullrequesteja ja Issueita vastaan. Sovellus on tarkoitettu käytettäväksi jollekkin porukalle, esim työyhteisölle. Jos haluat oman instanssin sovelluksesta neuvon sen pystytyksessä mielelläni.
