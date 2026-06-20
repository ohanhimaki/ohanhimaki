# AI-enabled-workflow

Miten muokataan nykyistä työkulkua ai-työkalujen tehokkaasen käyttöön osana kehitystä?

Yksinkertaistettuna omat näkemykseni projektin vaatimuksille:

- Kaikki koodi saataville versionhallintaan
- Kaikki oleellinen dokumentaatio koneen luettavaan muotoon ai-työkalun saataville versionhallintaan.

Nämä asiat on sellaisia jotka lähtökohtaisesti tekee jokaisesta projektista ylläpidettävämmän ja selkeämmän.

AI työkalun käyttöönottoa kun miettii voikin esittää itselle kysymyksen: 

*voiko uusi tiimin jälsen ymmärtää tämän ratkaisun ilman, että alkuperäinen kehittäjä on paikalla?* 

## Koodit versionhallintaan 

LLM työkalut ymmärtää koodia tehokkaasti ja pärjää sillä jo monessa taskissa. 
Jos kaikki projektin koodit ei ole versionhallinnassa niin tässä taas yks hyvä syy ne sinne hoitaa. 

## Oleelliset dokumentaatiot versionhallintaan koneen luettavaan muotoon 

Dokumentaatioita on monenlaisia, ja projekteissa syntyy dokumentaatiota joka ei välttämättä kuulu tekniseen kokonaisuuteen. 
Näitä tulee eritellä ja pitää huoli että ei lähdetä liian agressiivisesti siirtämään dokumentaatiota vain versionhallintaan
, sekä että dokumentaation joka sisältää salaisuuksia ei kuulu ikinä mennä versionhallintaan.
Kuitenkin jos dokumentaation ylläpidon vastuu on kehitystiimillä, se todennäköisesti voisi olla järkevää dokumentoida osaksi versionhallintaa. 
Hyvä kuitenkin myös muistaa, että kaikkia asioita ei voi laittaa versionhallintaan. 

Monenlaisia toteutuksia dokumentaatiosta uran aikana nähneenä koen, että versionhallinnan sisällä oleva dokumentaatio on paras ratkaisu muutenkin. Dokumentaation omistajuus on selkeämpää, se on lähellä koodia joka helpottaa käyttöä ja päivitystä usein.

Ympäristön kuvaukset c4 mallilla kannattaa toteuttaa architecture as code työkalulla.
Esimerkkinä tästä : [likec4](https://likec4.dev) josta oon tehnyt esimerkkiä ja kirjoitusta [playbookkiin](../playbook/likec4.md)

Saman kaltainen dokumentaatio tukee käyttäjiä ja ai-työkalua. Useissa toteutuksissa dokumentaatio on kausittaista, tehdään joskus joku dokumentti eikä enää ylläpidetä sitä. Tämä ajaa siihen että dokumentaatiosta ei ole kehityksen tueksi niin vahvaa apua, ja kehittäjät adaptoi käytännön jossa ympäristön kehitys nojautuu pitkästi vahvaan henkilöosaamiseen. 


## Muodostetaan ympäristö specifyistä tyyleistä skillsejä

Skillsit voi ajatella valmiina prompteina ai-työkalulle. Niitä voidaan toteuttaa esimerkiksi mallista miten transformaatiot tulee suorittaa silver kerrokseen. Näihin laitetaan lyhyt kuvaus ja työkalu sen jälkeen osaa tunnistaa koska tätä taitoa tulisi käyttää.

Tarkemmat formaatit ja tallennuspolut projektikohtaisille skillseille voivat vaihdella käytettävän työkalun mukaan, mutta `/.agents/skills/<skillname>/SKILL.md` on yksi malli jota monet työkalut tukee. 



