FROM node:10 AS builder

WORKDIR /app

COPY front/*.json ./
COPY front/*.conf ./
COPY front/src src

ARG env
#RUN mv /app/src/environments/environment.prod.ts /app/src/environments/environment.ts

RUN npm install && \
    npm run build

FROM nginx

COPY --from=builder /app/dist/* /usr/share/nginx/html/
COPY front/nginx-custom.conf /etc/nginx/conf.d/default.conf