To run: 

* in powershell at top level of repository
** docker volume create <volume name>
** docker build -t <your tag> .
** docker run -d -p 8080:80 --mount source=<volume name>,target=/app/appdata --name <your container name> <your tag>
