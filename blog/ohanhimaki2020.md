---
path: "ohanhimaki-2020"
date: "2020-01-24"
title: "Omat sivut uudella stackillä"
tags: ["GatsbyJS", "Tailwind", "Netlify"]
excert: "Omat sivut toteutettu uudella stackillä ja hostattu netlifyyn. Projektista on muodostunut hiekkalaatikko erilaisten apien testaukselle."
repo: "ohanhimaki/ohanhimaki2020"
---

## OHanhimaki2020

Omat sivut toteutettu uudelleen uudella stackillä. Mukana mm. GatsbyJS ja Tailwindcss.

GatsbyJS generoi stattisia sivuja cms:sistä, apeista tai vaikka Markdown-tiedostoista. Sivuilla sitä on hyödynnetty blogiosuuden generointiin. Postaukset lisätään repositoryyn ja buildausvaiheessa niistä muodostetaan staattinen sivu. Staattinen sivu muodostetaan myös "postaukset"-sivusta.

Tailwindcss on css-kirjasto jossa on paljon valmiiksi määriteltyjä luokkia. Luokat on yksinkertaisia, mutta niitä joutuu yleensä käyttämään useampaa saavuttaakseen haluamansa lopputuloksen.

Sivut hostataan Netlifyssä. Kun master branchiin pushataan uusi committi niin Netlify automaattisesti buildaa sivustosta uuden version.

Reactissa käytetään typescriptiä.

Buildatessa ajetaan purgecss, se poistaa käyttämättömiä luokkia. PurgeCSS on tailwindcss:sää käytettäessä tärkeää, sillä poistetaan projektista riippuen suuria määriä tailwindcss:n käyttämättömiä luokkia.
