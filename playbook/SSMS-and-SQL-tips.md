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
