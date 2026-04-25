# Neovim

Neovimmi on terminaalissa suoritettava teksti editori.Käytän neovimmiä nykyään
kirjoitteluun ja koodaamiseen. 

Neovimmissä on verrattaen pitkä oppimiskäyrä, itse olen aloittanut tämän matkan
käyttämällä ensin Jetbrainsin riderissä 
[IdeaVim](https://plugins.jetbrains.com/plugin/164-ideavim)
pluginia.

Neovimmi on hyvin customoitavissa, mun konffit löydät mun dotfiles reposta.


## Tips and tricks 

### Norm :norm :normal



#### Lisää jotain sanan loppuun kaikilla riveillä

```vim
:%norm A;
```
Lisää ; jokaisen rivin loppuun.

#### Lähetä esc painallus

(^[ = ESC, helpoin tapa on <C-v><Esc>)

Jos halutaan ajaa vain tietyille:

```vim

```


### substitute :s :substitute

:[range]s[ubstitute]/{pattern}/{string}/[flags] [count]

```vim
:%s/vanhateksti/uusiteksti/g
```
-> vaihtaa kaikki vanhateeksti esiintymät uusiteksti 

#### Hyväksy yksitellen (c loppuun)

```vim
'<,'>s/vaihtaa/uusiteksti/gc
```
-> tekee valitulle alueelle, kysyy jokaisen esiintymisen kohdalla

#### Erotin merkkinä voi käytätä myös vaikka . (case: pitää vaihtaa / merkit %)

```vim
'<,'>s./.%.g

```

-> Erottimena voi käyttää muitakin kuin / merkkiä, tässä erottimena . koska / vaihdetaan % merkiksi.
Oikeita caseja esim kansio polkujen korvaaminen


#### RegEx patternissa

```vim
-- rivin loppuun , merkki
:%s/$/,/g

```


```vim
-- John Smith

:%s/\(\w\+\) \(\w\+\)/\2 \1/

-- Smith John

```


```vim
--apple
--banana
--carrot

:%s/\(\w\+\)/\1 AS \1,/

--apple AS apple,
--banana AS banana,
--carrot AS carrot

```

### Sorttaus  

Vimmissä voi järjestää rivit `:sort` -komennolla . Järjestämisen voi tehdä koko tiedostolle, tai
valituille riveille. Lisäksi voit määritellä sorttaamaan tekstin sijaan sisällön
numeroja, ja jos numero ei ole ensimmäisenä rivillä, voi numeron valita
regexillä.

```vim
10 apple 
4 carrot 
2 orange
3 banana

:sort n 

2 orange
3 banana
4 carrot 
10 apple 

:sort

10 apple 
2 orange
3 banana
4 carrot 

-- Numero sorttaus toimii myös toisin päin

apple 10
carrot 4
orange 2
banana 3

:sort n 

orange 2
banana 3
carrot 4
apple 10

:sort
apple 10
banana 3
carrot 4
orange 2

-- sort n 3 saraketta? -> käyttää ensimmäistä esiintyvää

orange 2 1023
banana 3 211
carrot 4 0
apple 10 1


-- sort n ja Decimaalit?  -> Ei toimi 

banana 3.211
orange 3.1023
carrot 4.0
apple 10.1


banana 3,211
orange 3,1023
carrot 4,0
apple 10,1


```

Sorttaa toisen decimaalin perusteella (case nvim startuplog)

```vim
:%sort nr /\v^\d+\.\d+\s+\zs\d+\.\d+/
```

---

## 🔀 CodeDiff — Visuaalinen diff-työkalu

CodeDiff tarjoaa VSCode-tyylisen diff-näkymän suoraan Neovimiin: koko muuttunut rivi korostuu vaaleasti ja täsmälliset merkkimuutokset tummemmalla.

### Peruskomennot

| Komento | Toiminto |
|---------|----------|
| `:CodeDiff` | Vertaa nykyistä tiedostoa HEAD:iin |
| `:CodeDiff HEAD~3` | Vertaa 3 committia taaksepäin |
| `:CodeDiff main HEAD` | Vertaa kahta branchia |
| `:CodeDiff main...HEAD` | PR-tyylinen merge-base diff |
| `:CodeDiff tiedosto1.lua tiedosto2.lua` | Vertaa kahta tiedostoa |
| `:CodeDiff history` | Selaa nykyisen tiedoston commit-historia |
| `:CodeDiff history HEAD~10` | Rajaa historia viimeiseen 10 committiin |

### Näkymässä

| Näppäin | Toiminto |
|---------|----------|
| `t` | Vaihda side-by-side / inline-näkymän välillä |

### Esimerkkejä

```vim
" Katso mitä muuttui tässä tiedostossa viime PR:ssä
:CodeDiff main...HEAD

" Vertaa kahta eri versiota samasta tiedostosta
:CodeDiff HEAD~5 HEAD

" Selaa tiedoston koko historia
:CodeDiff history --reverse
```

---
