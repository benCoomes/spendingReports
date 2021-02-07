docker stop sr-api
docker rm sr-api
docker image rm sr-api
docker build -t sr-api .
docker run -d -p 8080:80 --mount source=sr-vol,target=/app/appdata --name sr-api sr-api