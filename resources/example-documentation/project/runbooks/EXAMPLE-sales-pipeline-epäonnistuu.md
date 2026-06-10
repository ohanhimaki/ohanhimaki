# Runbook: Sales-pipeline epäonnistuu yöllisessä ajossa

> **Tyyppi:** Runbook  
> **Projekti:** Sales-domain Silver & Gold  
> **Kriittisyys:** High  
> **Päivitetty:** 2025-02-28  
> **Omistaja:** Sales-tiimi / Janne Mäkinen

---

## Tilanne / Triggeri

Tätä runbookia käytetään kun:
- Teams-kanava `#data-alerts` ilmoittaa: `Pipeline FAILED: sales_bronze_ingestion`
- Raportti "Sales Morning Dashboard" näyttää eilistä dataa (ei tänään klo 07:00 jälkeen)
- Fabric-portaalissa pipeline-status on `Failed` tai `Cancelled`

## Vaikutus

Myyntitiimi ei saa päivittäisiä tilauslukuja aamupalaveriinsa (klo 08:30).
Vaikutusalue: Sales-johto, Key Account -tiimi, laskutus.
Jos pipeline ei ole valmis klo 08:00, lähde eskalointiin.

## Esitietovaatimukset

- [x] Pääsy Fabric-portaaliin (prod-workspace)
- [x] Oikeudet Fabric Monitoring Hubiin
- [x] Pääsy Azure Key Vaultiin (tarvitaan vain jos yhteystietoja pitää tarkistaa)
- [x] Teams-yhteys Sales-tiimiin

---

## Toimenpiteet

### 1. Diagnoosi – mitä tapahtui?

Avaa Fabric-portaali → **Monitoring Hub** → etsi `sales_bronze_ingestion` → avaa epäonnistunut ajo.

Tarkista virheilmoitus:
- `Connection timeout` → ongelma Salesforce-yhteydessä, katso kohta 2a
- `Schema mismatch` → Salesforceen on lisätty/poistettu kenttä, katso kohta 2b
- `Capacity exceeded` → Fabric-kapasiteetti täynnä, katso kohta 2c
- `File not found` → SFTP-tiedosto puuttuu, katso kohta 2d

### 2. Korjaustoimenpide

#### 2a – Connection timeout (Salesforce)

Tarkista Salesforce API -status: https://status.salesforce.com

Jos Salesforce on alas → odota ja aja pipeline manuaalisesti kun palautuu (kohta 3).

Jos Salesforce toimii → tarkista Service Principal:
```
Key Vault: kv-prod-sales
Secret: sp-prod-sales-pipeline-secret
```
Varmista että secret ei ole vanhentunut (expiry date Key Vaultissa).

#### 2b – Schema mismatch

> ⚠️ Älä aja pipeline uudelleen ennen kuin schema on päivitetty – uudelleenajo kaatuu samaan virheeseen.

1. Tarkista Salesforce-muutos: kysy Sales-tiimin Salesforce-adminilta mitä kenttää muutettiin
2. Päivitä bronze-taulun schema Fabric Notebookissa:
```python
# Lisää puuttuva sarake
spark.sql("""
  ALTER TABLE brz_sales_orders
  ADD COLUMN new_field_name STRING
""")
```
3. Tee PR sales-workspace-repoon, merge pipelinekonfiguraatioon
4. Aja pipeline manuaalisesti (kohta 3)

#### 2c – Capacity exceeded

Tarkista Fabric Admin Portal → Capacity Metrics.
Jos kapasiteetti on >90% → ilmoita Platform Ownerille (Mikko Virtanen) välittömästi.
Ei yritetä ajaa uudelleen ennen kuin kapasiteetti vapautuu.

#### 2d – SFTP-tiedosto puuttuu

Tarkista SFTP-serveri (yhteystiedot: `kv-prod-sales` → `sftp-sales-host`).
Kysy lähteen omistajalta (logistics-tiimi) miksi tiedosto puuttuu.
Tiedosto voidaan ladata manuaalisesti SFTP:lle jos se on saatavilla muualta.

### 3. Triggeröi uudelleenajo

Fabric-portaali → **Data Factory** → `sales_bronze_ingestion` → **Run now**

Tai Azure CLI:llä:
```bash
az datafactory pipeline create-run \
  --factory-name adf-prod-sales \
  --name sales_bronze_ingestion \
  --resource-group rg-prod-data
```

Seuraa ajoa Monitoring Hubissa. Kokonaisaika normaalisti 12–18 min.

### 4. Verifioi korjaus

- [x] Pipeline-status Monitoring Hubissa: `Succeeded`
- [x] Tarkista rivi `brz_sales_orders`: `SELECT MAX(_ingested_at) FROM brz_sales_orders` → pitäisi olla tänään
- [x] Tarkista silver ja gold valmistuvat (silver-pipeline käynnistyy automaattisesti bronzen jälkeen)
- [x] Sales Morning Dashboard päivittyy – tarkista luku "Tänään" vs "Eilen"
- [x] `#data-alerts` -kanavalla ei uusia hälytyksiä 30 min jälkeen

---

## Rollback

Jos uudelleenajo epäonnistuu eikä syytä löydy 30 minuutissa → hae edellinen onnistunut tila Delta time travelilla:

```python
# Tarkista versiohistoria
display(spark.sql("DESCRIBE HISTORY brz_sales_orders LIMIT 10"))

# Palauta edellinen versio (korvaa VERSION_NUMBER oikealla)
spark.sql("""
  RESTORE TABLE brz_sales_orders TO VERSION AS OF <VERSION_NUMBER>
""")
```

> **Huom:** Rollback palauttaa bronzen, mutta silver ja gold pitää ajaa manuaalisesti sen jälkeen.

## Eskalointipisteet

| Tilanne | Eskalointi | Yhteystieto |
|---|---|---|
| Syytä ei löydy 30 min sisällä | Platform Owner | Mikko Virtanen – Teams / +358 40 123 4567 |
| Salesforce ei vastaa yli 2h | Salesforce-admin | Tiina Leppänen – Teams |
| Kapasiteettiongelma | Platform Owner + Azure-tuki | Mikko Virtanen |
| Datavaurio tuotannossa | Platform Owner + tech lead | Pikakokous Teams |

## Juurisyyanalyysi (Post-incident)

> Täytä tämä osio incidentin jälkeen ennen tiketin sulkemista.

**Juurisyy:** *(esim. 2025-02-14: Salesforce lisäsi pakollisen kentän `opportunity_stage_v2` ilman etukäteisilmoitusta)*

**Estäisikö tämä toistumisen:** *(esim. Sovittiin Salesforce-tiimin kanssa muutoksista ilmoittaminen viikkoa etukäteen)*

**Action itemit:**
- [ ] Lisää schema drift -tarkistus pipeline-alkuun joka hälyttää ennen kaatumista
- [ ] Ota Salesforce muutosnotifikaatiot käyttöön

## Historia

| Päivämäärä | Tilanne | Tekijä | Huomiot |
|---|---|---|---|
| 2025-02-14 | Schema mismatch – Salesforce lisäsi kentän | Janne Mäkinen | Korjaus 45 min, dashboard myöhässä 1h |
| 2024-12-03 | Connection timeout – Salesforce maintenance | Anna Korhonen | Odotettiin 2h, pipeline ajettiin manuaalisesti |
