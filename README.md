# Dokumentation för TCP/UDP-applikation

### Innehållsförteckning
1. Introduktion
2. Komma igång
   * Bygga koden
   * Konfigurera och köra applikationen
3. Funktioner
4. Arkitektur
5. Tekniska detaljer
6. Felsökning
***
### 1. Introduktion
Denna dokumentation tillhandahåller grundläggande information om TCP/UDP-applikationen, dess syfte och hur den används effektivt.

### 2. Komma igång
#### 2.1 Bygga koden
  För att bygga applikationen, följ dessa steg:
  1. Klona projektet från [denna länk](https://github.com/iceotop/TCPUDPAssignment).
  2. Öppna projektet i din föredragna utvecklingsmiljö (t.ex. Visual Studio).
  3. Kompilera koden utifrån .sln-filen.
  4. Lös eventuella beroenden eller saknade referenser om det uppmanas.
#### 2.2 Konfigurera och köra applikationen
  För att konfigurera och köra applikationen behöver du:
  1. Se till att du kompilerat koden.
  2. Leta upp .exe-filen i projektkatalogens "bin"-mapp.
  3. Kör igång minst två instanser av applikationen - en som agerar som server och en som agerar som klient.

### 3. Funktioner
Applikationen tillåter användare:
  * Agera som TCP-server för inkommande TCP-meddelanden.
  * Agera som UDP-server för inkommande UDP-meddelanden.
  * Skicka TCP-meddelanden till bestämd port.
  * Skicka UDP-meddelanden till bestämd port.

### 4. Arkitektur
Applikationen stöder möjligheten att starta antingen en TCP- eller UDP-server. Den stöder även att skicka meddelanden till en server genom någon av protokollen. Alltså är både server- och klientprogrammet del av samma kodbas. Meddelandena är också hårdkodade så att de endast skickas inom samma enhet.

### 5. Tekniska detaljer
När användaren väljer att starta en TCP-server eller en UDP-server, initieras en server på localhost med de fördefinierade portnumren (TCP-port 4568 och UDP-port 1239).

Servern lyssnar på de angivna portarna och väntar på inkommande meddelanden.

När användaren väljer att skicka ett meddelande via TCP eller via UDP, skapar klienten en anslutning till localhost (loopback) med de motsvarande portarna.

Klienten konverterar meddelandet till byte-data och skickar det till den angivna servern via antingen TCP eller UDP, beroende på användarens val.

Servern tar emot meddelandet och visar det i konsolfönstret.

Både klienten och servern stänger sina nätverksanslutningar efter att meddelandet har skickats och mottagits.

### 6. Felsökning
<ins>Om ett TCP-meddelande skickas men servern inte hittas:</ins> klienten skickar en anslutningsförfrågan men anslutningsförsöket misslyckas, vilket leder till en "unhandled exception" och resulterar i att applikationen kraschar.
