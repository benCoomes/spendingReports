# use latest version of nodejs
FROM node:lts-alpine

# install aurelia-cli to build the app and http-server to serve static content
RUN npm i -g http-server
RUN npm i -g aurelia-cli

# set working directory to app
WORKDIR /app

# copy package.json related files first and install all required dependencies
COPY package*.json ./
RUN npm install

# copy the rest of th efiles and folders & install dependencies
COPY . ./
RUN npm run build

# by default http-server will server contents on port 8080
EXPOSE 8080

CMD ["http-server", "dist"]