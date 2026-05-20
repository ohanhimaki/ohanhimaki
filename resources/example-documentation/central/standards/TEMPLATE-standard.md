# Standard: [Standardin nimi]

> **Tyyppi:** Standard  
> **Versio:** 1.0  
> **Omistaja:** [Tiimi]  
> **Päivitetty:** YYYY-MM-DD  
> **Status:** Draft | Active | Deprecated

## Tarkoitus

Mitä tämä standardi määrittelee ja miksi se on olemassa.

## Soveltamisala

Mihin tämä standardi pätee (esim. kaikki Fabric-workspacet, kaikki pipelines, jne.).

## Standardi

### Nimeämiskäytännöt

```
<ympäristö>_<domain>_<kohde>_<tyyppi>
esim. prod_sales_orders_fact
```

### Rakenne / Malli

Kuvaa odotettu rakenne, formaatti tai toimintamalli.

```yaml
# esimerkki konfiguraatiosta tai rakenteesta
key: value
```

### Hyväksytyt vaihtoehdot

| Valinta | Käyttötilanne |
|---|---|
| Vaihtoehto A | Kun X |
| Vaihtoehto B | Kun Y |

## Poikkeukset standardista

Poikkeukset dokumentoidaan projektirepossa `deviations/`-kansiossa.
Katso malli: `project/deviations/TEMPLATE-deviation.md`

## Esimerkit

### Oikein ✅

```
esimerkki oikeasta toteutuksesta
```

### Väärin ❌

```
esimerkki virheellisestä toteutuksesta
```

## Viitteet

- [Linkki teknologian dokumentaatioon]
- [Linkki liittyvään ADR:ään]

## Muutoshistoria

| Versio | Päivämäärä | Muutos | Tekijä |
|---|---|---|---|
| 1.0 | YYYY-MM-DD | Ensimmäinen versio | ... |
