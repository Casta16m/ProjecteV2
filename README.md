# Musify - Reproductor de Música

## Membres del grup
Grup:
- Yossef JM
- Aimar CJ
- Luis BM
- Sergi CM
- Guillem BP

## Descripció
Musify és una aplicació de reproductor de música desenvolupada en Kotlin per a dispositius Android. Aquest projecte va ser creat com a part d'una tasca acadèmica que involucra l'ús de Kotlin, tres APIs, un projecte WPF i sockets amb encriptació.

## Estructura del Codi Font

- `/ProjecteV2-Android`: Conté el codi font principal de l'aplicació Android.
- `/ProjecteV2-WPF`: Conté el projecte WPF.
- `/ProjecteV2-SocketsAndTests`: Conté la implementació dels sockets, encriptació i testing.
- `/ProjecteV2-APIMongoDB`: Conté les dues APIs de Entity Framework, C# amb una base de dades de MongoDB.
- `/ProjecteV2-APISql`: Conté la API de Entity Framework, C# amb una base de dades de SQL.

## Recursos Addicionals
- [Disseny inicial de l'app i WPF a Figma](https://www.figma.com/file/GYmp7HTY5R1BZO4fdGDj8U/Musify?type=design&node-id=0%3A1&mode=design&t=UJ10nMkF3RmuopXo-1)
- [Trello del Projecte](https://trello.com/b/3UyTBxkh/projectev2)

---

# Musify - Android

## Característiques
- Accés al storage per obtenir les cançons.
- Creació de playlists amb emmagatzematge intern utilitzant arxius JSON.
- Realització de peticions a una API per instal·lar i descarregar cançons.
- Possibilitat de pujar cançons a l'API per a la base de dades.

---

# Musify - WPF

## Característiques
- Realització del manteniment de les bases de dades mitjançant les APIs.
- Creació d'informes (.pdf) sobre la informació de les bases de dades mitjançant les APIs.
- Signatura dels informes amb un certificat.
- Paginació interna dels PDF utilitzant WPF.
- Configuració d'IPs a tots els projectes mitjançant un appconfig.

---

# Musify - API SQL Entity Framework

## Característiques
- Creació de la base de dades amb Entity Framework a partir de l'Entitat Relació demanada.
- Execució de totes les peticions necessàries amb controladors.
- Organització del codi mitjançant l'ús de serveis.

---

# Musify - API MONGO **Fitxers** Entity Framework
Esta a dins de la carpeta de `/ProjecteV2-APIMongoDB/DBMongo/FitxersAPI`:

## Característiques
- Creació de la base de dades amb Entity Framework.
- Emmagatzematge de l'àudio de una cançó amb l'ID de SQL.
- Utilització de GridFS per emmagatzemar els fitxers.
- Execució de totes les peticions necessàries amb controladors.
- Organització del codi mitjançant l'ús de serveis.

---

# Musify - API MONGO **Normal** Entity Framework
Esta a dins de la carpeta de `/ProjecteV2-APIMongoDB/DBMongo/MongoAPI`:

## Característiques
- Creació de la base de dades amb Entity Framework.
- Emmagatzematge de la lletra de una cançó amb l'ID de SQL.
- Emmagatzematge de l'historial de reproducció de una cançó amb l'ID de SQL.
- Execució de totes les peticions necessàries amb controladors.
- Organització del codi mitjançant l'ús de serveis.

---

# Musify - Sockets i Testing

## Característiques
- Desenvolupament d'un servidor de sockets per obtenir informació de les bases de dades mitjançant les APIs i generar informes (.pdf).
- Recepció d'una petició del client de sockets amb l'informe desitjat i la clau pública del certificat.
- Signatura de l'informe generat.
- Xifrat del PDF amb AES i la clau pública rebuda, enviant-ho tot en un objecte al client.
- Recepció del client del PDF, la clau AES xifrada i la desxifrat, amb la capacitat de guardar-ho.
- Implementació de tests per a les funcions principals.
- Configuració d'IPs a tots els projectes mitjançant un appconfig.
