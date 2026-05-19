# RDP 

MS Sql server kehitystä tehdään usein työskentelemällä asiakkaan kehityskoneella
rdpn yli. 

## Settings

Pari juttua: Asiakasyrityksen värit windows accent coloriksi (tärkeä)

Työkalut alhaalla samaan järkkään ku omalla koneella 
1. File Explorer
2. Terminaali
3. Selain 
4. Tästäeteenpäin vaihtelevia työvälineitä, nämä 3 tärkeimmät win+<num> bindit
   itellä.


## Intra sovellukset

Pitää konffia kahteen kohtaan:
1. Windows Credentials manageriin määritetään mitä tunnuksia käytetään. 
2. Internet optionsiin määritellään mihin saa syöttää tallennettuja credentialeja.

### Credential manager 

Control Panel\User Accounts\Credential Manager

Windows Credentials > Add a Windows credential.

Lisättä tänne tarvittavat osoitteet/palvelimet & tunnukset ja tallenna. 

### Internet Options 

Control Panel\Network and Internet 

Internet options 

Security 

-> Local intranet -> Sites -> Advanced 
Lisää tarvittavat osoitteet/palvelimet ja tallenna.

## RDP - yhteys katkeilee 

Asetus pitää tehdä omalla koneella, englanniksi polku on: 
Administrative Templates -> 
Windows Components -> 
Remote Desktop Connection Client -> 
Turn Off UDP On Client (tämä vaihdetaan siis enabled)

Tämän jälkeen kun yhdistää rdp:llä, pitäisi olla toi udp teksti pois yhteyden tiedoista.
