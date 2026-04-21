<img width="1101" height="518" alt="image" src="https://github.com/user-attachments/assets/e24ac63a-300f-4218-b459-591008baa679" />

# Arkitektur
 
Diagrammet herover viser hvordan jeg ville designe løsningen til produktion. Jeg har valgt at samle API og sync worker i én service frem for to separate, da de deler database og forretningslogik — det ville bare tilføje kompleksitet uden nogen reel gevinst på nuværende tidspunkt.

Hvis løsningen voksede ville det give mening at splitte dem ad. Det er primært læseoperationerne fra API'et der vil være under pres ved høj trafik som Black Friday, mens sync workeren kører periodisk i baggrunden og ikke har samme skaleringsbehov. Ved at separere dem kunne man skalere API'et horisontalt uden at spin ekstra instanser af workeren op oveni.
 
I en produktionsklar løsning ville der sidde en **load balancer** foran servicen der fordeler trafik på tværs af flere instanser under høj belastning som Black Friday. API'et er designet stateless, så horisontal skalering kan fungere uden ændringer i koden. Cachen ville i produktion være **Redis** frem for in-process cache, så alle instanser deler samme cache-lag. Databasen ville hostes på en managed service som **Azure Database for PostgreSQL** der håndterer backup og failover automatisk.
 
Fejltolerance er overvejet ved at sync workeren fanger alle exceptions og logger dem uden at crashe applikationen — API'et forbliver tilgængeligt selvom ERP-systemet er nede. I produktion ville **Polly retry-logik** på ERP HTTP-klienten håndtere midlertidige netværksfejl automatisk.
 
---
 
# Deployment
 
## Lokalt med Docker
 
Til lokal udvikling har jeg lavet en `Dockerfile` til API'et og en `docker-compose.yml` der spinner både API og PostgreSQL database op sammen. Det gør det nemt at køre hele løsningen lokalt uden at have andet installeret end Docker, og det betyder at miljøet er ens uanset hvem der kører det.
 
```bash
docker-compose up --build
```
 
API'et er tilgængeligt på `http://localhost:8080`.
 
## Cloud deployment
 
I produktion ville en mulighed være **Azure App Service**. Fordi det er en managed platform — det betyder at man ikke selv skal tænke på servere, OS-opdateringer eller netværkskonfiguration. Man giver den sin Docker container og så sørger Azure for resten. Det gør det nemmere at fokusere på applikationen frem for infrastruktur.
 
Det der konkret taler for Azure App Service i denne case er:
 
**Automatisk skalering** — man kan konfigurere regler som "spin en ekstra instans op hvis CPU overstiger 70%". Det er præcis relevant til Black Friday hvor trafikken kan stige meget pludseligt og uforudsigeligt.
 
**Deployment slots** — Azure giver et staging-miljø ved siden af produktion. Man deployer den nye version til staging, verificerer at alt virker, og swapper derefter staging ind i produktion. Selve swappen tager sekunder og brugerne oplever ingen nedetid.
 
**Rollback** — hvis noget alligevel går galt efter en swap, kan man øjeblikkeligt swppe tilbage til den tidligere version. Det giver en sikkerhed der er svær at bygge selv.
 
Når der skal deployes er det godt med en CI/CD pipeline der automatisk bygger og deployer ved push til main. På den måde sikres det at koden altid er testet inden den rammer produktion, og deployment er ikke noget man skal huske at gøre manuelt.
