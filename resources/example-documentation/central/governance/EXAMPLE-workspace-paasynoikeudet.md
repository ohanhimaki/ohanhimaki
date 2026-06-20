# Governance: Fabric-workspacejen pääsynhallinta

> **Tyyppi:** Governance  
> **Versio:** 1.1  
> **Omistaja:** Data Platform -tiimi  
> **Päivitetty:** 2025-01-20

## Tarkoitus

Määrittelee säännöt sille, kenellä on oikeus mihinkin Fabric-workspaceen ja millä roolilla.
Tarkoitettu kaikille projektitiimeille ja Data Platform -tiimille.
Varmistaa että tuotantodata ei ole tarpeettomasti saavutettavissa kehittäjiltä.

## Roolit ja vastuut

| Rooli | Vastuu | Tiimi/Henkilö |
|---|---|---|
| Platform Owner | Workspacejen luonti, Admin-oikeuksien myöntäminen | Data Platform -tiimi / Mikko Virtanen |
| Data Engineer | Kehittää pipelineja, omistaa silver/gold-logiikan | Projektitiimi |
| Data Consumer | Lukee Gold-kerroksen dataa raporteissa | Liiketoiminta-analyytikot, Power BI -kehittäjät |
| Workspace Admin | Hallinnoi yksittäisen workspacen oikeuksia | Projektin tech lead |

## Säännöt ja rajoitteet

1. **Tuotantoworkspaceen (prod) ei ole suoraa kehittäjäpääsyä** – muutokset menevät aina CI/CD-putken kautta. Käsin tehtävät muutokset tuotantoon vaativat Platform Ownerin hyväksynnän.

2. **Bronze-workspaceen pääsy on rajoitettu** – vain Data Engineers ja Pipeline Service Principals. Ei liiketoimintakäyttäjille, ei Power BI -kehittäjille.

3. **Oikeudet myönnetään ryhmille, ei yksilöille** – Kaikki pääsynhallinta tehdään Entra ID -ryhmien kautta. Henkilökohtaisia oikeuksia ei myönnetä workspacetasolle.

4. **Service Principalit nimetään standardin mukaan** – `sp-<env>-<domain>-<käyttötarkoitus>`, esim. `sp-prod-sales-pipeline`.

5. **Oikeudet tarkistetaan neljännesvuosittain** – Platform Owner ajaa access review -kierroksen Q1, Q2, Q3, Q4.

## Roolimatriisi

| Workspace | Platform Owner | Data Engineer | BI Developer | Business User |
|---|---|---|---|---|
| prod-bronze | Admin | Contributor | Ei pääsyä | Ei pääsyä |
| prod-silver | Admin | Contributor | Viewer | Ei pääsyä |
| prod-gold | Admin | Contributor | Contributor | Viewer |
| dev-* | Admin | Admin | Contributor | Ei pääsyä |

## Poikkeusprosessi

1. Avaa tiketti ADO-boardille otsikolla `[ACCESS] <workspace> – <perustelu>`
2. Perustele tarve ja kesto (pysyvä / määräaikainen)
3. Hyväksyntä: Platform Owner + projektin tech lead
4. Poikkeus kirjataan workspace-kohtaiseen `access-exceptions.md`-tiedostoon
5. Määräaikaiset oikeudet poistetaan automaattisesti 90 päivän kuluttua

## Muutoshistoria

| Versio | Päivämäärä | Muutos | Tekijä |
|---|---|---|---|
| 1.0 | 2024-08-15 | Ensimmäinen versio | Mikko Virtanen |
| 1.1 | 2025-01-20 | Lisätty roolimatriisi, tarkennettu bronze-rajoite | Anna Korhonen |
