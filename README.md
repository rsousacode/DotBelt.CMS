# Boilerplate SSR Version

## Overview

This project is a Content Management System (CMS) that uses EditorJS for building content blocks.

## Backend

The backend is a .NET project using Minimal API with Identity pre-configured.

## Frontend

The frontend is built using the latest Svelte 5 framework.


## Development Setup

1. Add the following entry to your hosts file:

    ```plaintext
    127.0.0.1 boilerplate.com
    ```

2. Run the following command to bring up the necessary Docker containers and services:

```sh
just docker-up
```

This will:
- Instantiate a Postgres database
- Set up a reverse proxy
    - Frontend will be available at: [boilerplate.com](http://boilerplate.com)
    - API will be available at: [boilerplate.com/api](http://boilerplate.com/api)

# Getting started

1. Clone the repository.
2. Navigate to the project directory.
3. Run the required just commands as needed.

For example, to bring up Docker containers and run the frontend, you can run:


```sh
just docker-up
just frontend
```

4. Run the BoilerPlate.Api project to run the Api

## Justfile Overview

This project uses just for task management. Below are the available commands defined in the Justfile.

### Docker Commands

- `just docker-up`: Bring up Docker containers with build.
- `just docker-down`: Bring down Docker containers.
- `docker-preview`: Preview all Docker containers in production mode.


### Frontend Commands

- `just frontend`: Change directory to BoilerPlate.Frontend and run npm dev.
- `just generate-schema`: Generates the GraphQL schema used by the graphql codegen in the frontend project.
- `just frontend-codegen`: Change directory to BoilerPlate.Frontend and run npm run codegen to generate TypeScript types for GraphQL.
- `just graphql-update` Run both generate-schema and frontend-codegen to update the GraphQL schema and generate TypeScript types.

