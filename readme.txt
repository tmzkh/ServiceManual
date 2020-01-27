Databasena käytin MySql:ää

asennetut nuget packaget:
	CsvHelper (dbseed.csv:n luku)
	Microsoft.EntityFrameworkCore.Design
	Microsoft.EntityFrameworkCore.Tools
	Pomelo.EntityFrameworkCore.MySql
	Z.EntityFrameworkCore.Extensions.EFCore

sovelluksen käynnistamiseksi:
	luo mysql:n käyttäjä:
	create user 'ServiceManualUser'@'localhost' IDENTIFIED with mysql_native_password by 'TooEasyToGuess';
	grant all on *.* to 'ServiceManualUser'@'localhost';

	tai muuta haluamasi tiedot EtteplanMORE.ServiceManual.Web/appsettings.json:iin

	Käynnistettäväksi projektiksi tulee valita EtteplanMORE.ServiceManual.Web ja Package Manager Consolesta EtteplanMORE.ServiceManual.ApplicationCore

	Package Manager Consolessa päivitä kanta:
	PM> Update-Database

	tai jos tämä ei syystä tai toisesta toimi, voi migraation luoda uudelleen, kun ensin manuaalisesti poistaa migraation 
	EtteplanMORE.ServiceManual.ApplicationCore\Migrations -kansiosta (tyhjennä kokonaan) ja luomalla ne uudestaan
	PM> Add-Migration
	PM> Update-Database

	apista kannattaa kutsua ensimmäisenä api/factorydevices/ GET:llä, jolloin db seedataan
	seedaus ks. FactoryDeviceService.GetAll()
		poista rivit
			var b = _context.FactoryDevices.Any();
			if (!b) {
				DbSeeder.Seed(_context);
			}
		jos et halua minun tekemää seedausta
	seedauksen takia ensimmäinen haku kestää kauan

api:n endpointit
api/factorydevices?pageNumber={num}&pageSize={size}
pageNumber ja pageSize eivät ole pakollisia, oletuksena on page=1 size=50, max pageSize on 100
 GET: palauttaa laitteet paginoituna

api/factorydevices/{id}
 GET: palauttaa yhden laitteen, jos sellainen löytyy id:n perusteella

api/maintenancetasks?pageNumber={num}&pageSize={size}
pageNumber ja pageSize eivät ole pakollisia, oletuksena on page=1 size=50, max pageSize on 100
 GET: palauttaa huoltotehtävät paginoituna
 esimerkki yhdestä huoltotehtävävastauksesta:
	{
        "id": 3010,
        "factoryDeviceId": 228,
        "time": "2020-01-20T20:00:00",
        "description": "test qwer",
        "criticality": "Critical",
        "done": true
	}
 POST: luo uuden huoltotehtävän
 pyynnön tulee sisältää FactoryDeviceId, Time, Description ja Criticality
 esimerkki requestin bodystä:
	{
	 	"factorydeviceid":228,
	 	"time":"2020-01-25 12:00:00",
	 	"description":"test",
	 	"criticality":0
	}
 FactoryDeviceId on laitteen Id, time on kirjauksen ajankohta, Description kuvaus ja 
 Criticality tehtävän kriittisyys (0 = critical, 1 = important, 2 = minor)
 palauttaa luodun tehtävän
 PUT: päivittää huoltotehtävän
 pyynnön tulee sisältää Id, FactoryDeviceId, Description, Criticality ja Done
 esimerkki requestin bodystä:
	{
		"id":3010,
		"factorydeviceid":228,
		"time":"2020-01-25 12:00:00",
		"description":"test qwer",
		"criticality":0,
		"done":true
	}
 done-arvo kertoo, onko tehtävä suoritettu vai ei
 palauttaa päivitetyn huoltotehtävän

api/maintenancetasks/{id}
 GET: palauttaa yhden huoltotehtävän, jos sellainen löytyy id:n perusteella
 DELETE: poistaa huoltotehtävän, mikäli id vastaa jonkin huoltotehtävän id:tä

api/maintenancetasks/bydevice/{id}?pageNumber={num}&pageSize={size}
 pageNumber ja pageSize eivät ole pakollisia, oletuksena on page=1 size=50, max pageSize on 100
 GET: palauttaa laitteen id:n perusteella kaikki laitteen huoltotehtävät


