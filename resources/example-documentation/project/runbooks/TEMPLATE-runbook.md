# Runbook: [Toimenpiteen nimi]

> **Tyyppi:** Runbook  
> **Projekti:** [Projektin nimi]  
> **Kriittisyys:** Low | Medium | High | Critical  
> **Päivitetty:** YYYY-MM-DD  
> **Omistaja:** [Tiimi/Henkilö]

---

## Tilanne / Triggeri

Milloin tätä runbookia käytetään? Mikä on oire tai hälytys joka johtaa tänne?

Esim:
- Pipeline epäonnistuu virheellä `X`
- Raportti näyttää vanhaa dataa
- Schema drift -hälytys

## Vaikutus

Mitä tapahtuu jos tähän ei reagoida? Ketkä ovat vaikutusalueella?

## Esitietovaatimukset

- [ ] Pääsy ympäristöön [env]
- [ ] Oikeudet [järjestelmä/rooli]
- [ ] Tietämys [teknologia X]

---

## Toimenpiteet

### 1. Diagnoosi – mitä tapahtui?

```bash
# Esimerkki: tarkista pipeline-ajot
# az datafactory pipeline-run query-by-factory ...
```

Mitä tulosteesta pitää tarkistaa?

### 2. Korjaustoimenpide

> ⚠️ Huom: [varoitus jos jokin toimenpide on riskialtis]

```bash
# Esimerkki korjauskomento
```

Selitys siitä mitä komento tekee.

### 3. Triggeröi uudelleenajo

```bash
# Esimerkki: käynnistä pipeline uudelleen
```

### 4. Verifioi korjaus

Miten tarkistetaan että tilanne on korjattu?

- [ ] Pipeline ajaa onnistuneesti
- [ ] Data on päivittynyt
- [ ] Hälytys on poistunut

---

## Rollback

Jos korjaus ei toimi tai pahentaa tilannetta:

```bash
# rollback-komento
```

## Eskalointipisteet

| Tilanne | Eskalointi | Yhteystieto |
|---|---|---|
| Ei tiedetä syytä 30 min jälkeen | [Henkilö/tiimi] | ... |
| Datavaurio tuotannossa | [Henkilö/tiimi] | ... |

## Juurisyyanalyysi (Post-incident)

> Täytä tämä osio incidentin jälkeen.

**Juurisyy:**

**Estäisikö tämä toistumisen:**

**Action itemit:**
- [ ] ...

## Historia

| Päivämäärä | Tilanne | Tekijä | Huomiot |
|---|---|---|---|
| YYYY-MM-DD | ... | ... | ... |
