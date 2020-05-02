FROM node:12.10-alpine
RUN npm install -g nodemon 
RUN npm install -g jasmine
WORKDIR /app
COPY package.json ./package.json
COPY package-lock.json ./package-lock.json
RUN npm install
COPY . .
CMD npm test