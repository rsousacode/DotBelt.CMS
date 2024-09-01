FROM node:22-alpine AS prepare
WORKDIR /app
RUN adduser -u 1500 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM node:22-alpine as build

WORKDIR /app

COPY ../../DotBelt.Frontend/package.json ../../DotBelt.Frontend/package-lock.json ./
RUN npm ci

COPY DotBelt.Frontend/ .

RUN npm run build-prod


FROM prepare AS final
COPY --chown=appuser --from=build /app/build .

CMD ["node", "./index.js"]
