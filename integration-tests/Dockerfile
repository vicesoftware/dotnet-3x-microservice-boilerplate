FROM node:12.10-alpine
RUN npm install -g nodemon 
WORKDIR /usr/src/app
COPY package.json ./package.json
RUN npm install
COPY . .
CMD npm test