FROM node:20-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build

FROM nginx:1.27-alpine
COPY --from=build /app/dist/kritis-frontend /usr/share/nginx/html
EXPOSE 8080
RUN sed -i 's/listen\\s\\+80;/listen 8080;/' /etc/nginx/conf.d/default.conf \
  && addgroup -g 10001 app \
  && adduser -D -G app -u 10001 app \
  && chown -R app:app /usr/share/nginx/html /var/cache/nginx /var/run /etc/nginx/conf.d
USER app

