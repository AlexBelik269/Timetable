
Dieses Projekt benötigt SSMS, inkl. MSSQL. 

MSSQL kann man unter diesem link herunterladen (Wenn man runterscrollt, findet man eine Gratisversion für Entwickler):

https://www.microsoft.com/de-ch/sql-server/sql-server-downloads

SSMS unter diesem:

https://learn.microsoft.com/de-de/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16


Für den Datenbank zugriff ist folgendes nötig zum einstellen (Bei SSMS):

-SQL Server- und Windows-Authentifizierungsmodus

Tutorial unter:

https://www.dundas.com/support/learning/documentation/installation/how-to-enable-sql-server-authentication

-Login mit folgenden Angaben: Username: "MyLogin" Passwort: "123".
	Entsprechend kann man den User anpassen, jedoch muss man es auch im Code
	anpassen.

Tutorial unter:

https://www.guru99.com/sql-server-create-user.html


Wichtig ist: 
	-Username & Passwort
	-SQL Server Authentication
	-Serverrollen --> alle Adminrechte

Man muss entsprechend auch die Queries im Projekt selber ausführen.


Im Code selber funktionieren die INSERTs & SELECTs für die Datenbank wartung. Jedoch ist in diesem stand der Algorithmus
und Excel exporter nicht in einem funktionstüchtigen zustand.