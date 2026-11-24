# La Cantina e Forum Star Wars

Forum francophone responsive servi par l'API ASP.NET Core. Sans MySQL, l'interface reste utilisable en apercu local : discussions, reponses et reactions sont conservees dans le navigateur. Quand l'API lit la base, le fil affiche les discussions MySQL et les comptes authentifies peuvent publier et repondre.

## Demarrage local sans base

Prerequis : .NET 9 SDK.

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet run --project .\Binks_Forum_API.csproj
```

Ouvrir l'URL affichee par `dotnet run`. L'apercu est disponible sans MySQL. `/health/live` verifie le processus et `/health/ready` verifie l'acces a MySQL. Swagger est disponible sur `/swagger` en developpement.

## Configuration avant deploiement

En environnement `Production`, l'application refuse de demarrer si l'une de ces valeurs manque :

```text
ConnectionStrings__DefaultConnection=Server=...;Port=3306;Database=...;User ID=...;Password=...;
Jwt__SigningKey=<secret aleatoire d'au moins 32 octets>
```

Generer une cle avec PowerShell :

```powershell
[Convert]::ToBase64String([Security.Cryptography.RandomNumberGenerator]::GetBytes(48))
```

Definir les secrets dans le gestionnaire de secrets de l'hebergeur, jamais dans `appsettings.json`. Construire le paquet :

```powershell
dotnet publish .\Binks_Forum_API.csproj -c Release -o .\publish
```

Le schema MySQL doit correspondre aux entites definies dans `Data/ApplicationDatabaseContext.cs`. Le depot ne contient pas encore de migration EF Core ni de script d'initialisation. Il faudra verifier le schema et la presence des rangs et factions de reference quand la base sera fournie.

## Routes principales

- `POST /api/Users/signup` et `POST /api/Users/login` : compte et jeton Bearer.
- `GET /api/Topic` et `GET /api/Topic/{id}` : lecture des discussions.
- `GET /health/live` et `/health/ready` : sondes d'hebergement.

Dans l'apercu, les publications restent locales. Avec MySQL configure et un jeton obtenu par connexion, les routes de creation de discussion et de reponse ecrivent dans la base. Les roles administrateur et moderateur doivent etre attribues cote base, jamais depuis une requete publique.
