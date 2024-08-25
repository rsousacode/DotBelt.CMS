# Boilerplate SSR version

## Backend

Description: The backend is a .NET project using Minimal API with Identity pre-configured.


## Development

Add in the hosts file

`127.0.0.1 boilerplate.com`

Run docker compose:

`docker compose up --build`

It will:
* Instantiate Postgres database
* Reverse proxy
  * Frontend will be in: boilerplate.com
  * API will be in: boilerplate.com/api


## All containers running


`docker compose -f docker-compose.all.yml up  --build`