# Boilerplate SSR version

## Backend

Description: The backend is a .NET project using Minimal API with Identity pre-configured.



## Development

Add in the hosts file

`127.0.0.1 boilerplate.com`

Run docker compose:

`docker compose up --build`

OR

`npm run docker-up`

This does the following:
* Instantiate Postgres database
* Reverse proxy
  * Frontend will be in: boilerplate.com
  * API will be in: boilerplate.com/api


## Preview all containers running

Run docker compose:

`docker compose -f docker-compose.all.yml up  --build`

OR

`npm run docker-preview`