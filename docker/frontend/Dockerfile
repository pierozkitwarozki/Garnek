FROM node:16.3.0-alpine AS frontend
WORKDIR /app
COPY . .

RUN npm ci && npm run build -- --configuration production

FROM nginx:alpine
COPY --from=frontend /app/dist/Frontend /usr/share/nginx/html
EXPOSE 80
