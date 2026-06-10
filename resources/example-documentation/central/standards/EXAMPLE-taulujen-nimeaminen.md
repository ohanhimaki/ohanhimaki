# Standard: Fabric-taulujen nimeämiskäytäntö

> **Tyyppi:** Standard  
> **Versio:** 1.2  
> **Omistaja:** Data Platform -tiimi  
> **Päivitetty:** 2025-03-10  
> **Status:** Active

## Tarkoitus

Määrittelee yhtenäisen nimeämiskäytännön kaikille tauluille Microsoft Fabric -workspaceissa.
Yhtenäinen nimeäminen tekee datan löydettävyyden, hallinnan ja automaation helpommaksi
– sekä ihmisille että AI-työkaluille.

## Soveltamisala

Kaikki Fabric-workspacet (bronze, silver, gold) kaikissa ympäristöissä (dev, test, prod).
Koskee lakehouse-tauluja, warehouse-tauluja ja semantic model -entiteettejä.

## Standardi

### Nimeämiskäytäntö

```
<kerros>_<domain>_<kohde>[_<tyyppi>]
```

| Osa | Arvot | Kuvaus |
|---|---|---|
| `kerros` | `brz`, `slv`, `gld` | Bronze / Silver / Gold |
| `domain` | `sales`, `finance`, `hr`, `logistics` | Liiketoiminta-alue |
| `kohde` | snake_case, monikko | Mitä data kuvaa |
| `tyyppi` | `fact`, `dim`, `bridge`, `agg` | Vain Gold-kerroksessa |

### Esimerkit kaikille kerroksille

```
brz_sales_orders          ← bronze, raakadata
slv_sales_orders          ← silver, puhdistettu
gld_sales_orders_fact     ← gold, faktataulu
gld_sales_customers_dim   ← gold, dimensio
gld_sales_monthly_agg     ← gold, aggregaattitaulu
```

### Workspace- ja ympäristönimeäminen

```
<env>-<domain>-<kerros>
esim. dev-sales-silver
      prod-finance-gold
```

### Hyväksytyt lyhenteet

| Domain | Lyhenne |
|---|---|
| Sales | `sales` |
| Finance | `finance` |
| Human Resources | `hr` |
| Logistics | `logistics` |
| Shared / Common | `common` |

> Uudet domainit hyväksytetään Data Platform -tiimissä ennen käyttöönottoa.

## Poikkeukset standardista

Poikkeukset dokumentoidaan projektirepossa `deviations/`-kansiossa.
Katso malli: `project/deviations/TEMPLATE-deviation.md`

Hyväksyttyjä poikkeuksia tähän mennessä: [DEV-002 – Finance legacy taulunimet](../../project/deviations/EXAMPLE-DEV-002-finance-legacy-taulunimet.md)

## Esimerkit

### Oikein ✅

```
brz_sales_orders
slv_hr_employees
gld_finance_invoices_fact
gld_sales_customers_dim
prod-logistics-gold   (workspace)
```

### Väärin ❌

```
Sales_Orders          ← PascalCase, puuttuu kerros
bronze_sales_Order    ← kerros kirjoitettu kokonaan, yksikkö
ORDERS_FACT           ← kaikki isolla
gld_sales_order_facts ← tyyppi monikossa
dev_sales_silver      ← workspace käyttää alaviivaa ei viivaa
```

## Viitteet

- [Microsoft Fabric naming best practices](https://learn.microsoft.com/en-us/fabric/)
- [ADR-001 – Medallion-arkkitehtuurin käyttöönotto](../adr/EXAMPLE-ADR-001-medallion-arkkitehtuuri.md)

## Muutoshistoria

| Versio | Päivämäärä | Muutos | Tekijä |
|---|---|---|---|
| 1.0 | 2024-09-01 | Ensimmäinen versio | Mikko Virtanen |
| 1.1 | 2024-11-15 | Lisätty workspace-nimeäminen | Anna Korhonen |
| 1.2 | 2025-03-10 | Lisätty hyväksytyt lyhenteet, selvennetty gold-tyypit | Mikko Virtanen |
