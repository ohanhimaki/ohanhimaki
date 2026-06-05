# SSMS vinkit

Ylhäältä view -> Object Explorer details kautta avattavan näkymän avulla saa
esimerkiksi useita drop and create scriptejä otettua kerralla.


## SSMS settingsit

### Rivinumerot & wordwrap

Options -> Text Editor -> All Languages TAI Transact-SQL

[x] Line numbers

Kaikki ei välttämätty tykkää, mutta Word wrap on myös mahdollista kytkeä päälle
täältä.

### Scrollbar map mode

Scrollbarista voi näin tarkastella väreistä yms kuinka kaukana tietyt koodit on
jne. Esimerkiksi ainakin pitkät kommentit ja välit on helppo tunnistaa. Tämän
lisäksi Show preview tooltip asetuksella voi scrollbaria osoittaa hiirellä ja se
avaa pienen preview ikkunan, josta voi tarkastaa vaikka  kuinka se where ehto
oli toisaalla rakennettu jne. 

Options -> Text Editor -> All Languages TAI Transact-SQL -> Scroll bars 

[x] Use map mode for vertical scroll bar 
[x] Show Preview Tooltip

### Gridistä kopiointi 

Ongelma: Kun ajat kyselyn ja kopioit tulosruudukosta (Grid) pitkää tekstiä (kuten VARCHAR(MAX) tai XML-dataa), SSMS katkaisee tekstin oletuksena tietyn merkkimäärän jälkeen. Lisäksi solun sisällä olevat rivinvaihdot (CR/LF) muuttuvat usein välilyönneiksi kopioitaessa, mikä rikkoo esimerkiksi JSON- tai koodipätkät.

Ratkaisu ja polku asetuksiin:
Mene ylävalikosta: Tools -> Options
Navigoi vasemmalta: Query Results -> SQL Server -> Results to Grid
Muuta seuraavat asetukset:
Max Characters Retrieved -> Non-XML data: Nosta tätä suuremmaksi (esim. 65535 tai enemmän tilanteen mukaan, maksimi on 2 MB luokkaa).
Max Characters Retrieved -> XML data: Vaihda tilalle Unlimited (rajoittamaton).
Ruksi kohtaan: Retain CR/LF on copy or save (Tämä on kriittinen, jotta rivinvaihdot säilyvät tekstiä kopioitessa!)

💡 Huom: Nämä muutokset tulevat voimaan vasta uusiin kyselyikkunoihin (Query Tabs), jotka avaat asetuksen muuttamisen jälkeen.

### Taulusta Create Script jättää tärkeitä asioita tuomatta 

Asetuksista Scripting -> 

Script indexes : true 



# SQL server kyselyitä

## Etsi esiintymiä näkymistä

SSMS:n references on joskus mut pettänyt(ja ei toimi esim dynaamisissa kyselyissä) 
SSMS:llä voi myös ottaa kaikki luontiscriptit ja sitte vaan ctrl+f mutta se on
hidasta.

```SQL

declare @table_name nvarchar(100) = 'example_table'

SELECT *
from INFORMATION_SCHEMA.VIEWS
where VIEW_DEFINITION like @table_name


select  *
from INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_DEFINITION LIKE @table_name
```


## Etsi agent jobien stepeistä

```SQL
SELECT s.name, js.*
from msdb.dbo.sysjobsteps  js
left join msdb.dbo.sysjobs s on s.job_id = js.job_id
where js.command like '%<searchphrase>%'

```
