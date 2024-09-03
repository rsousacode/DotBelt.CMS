# Justfile

# Bring up Docker containers with build
docker-up:
    docker compose up --build

# Bring up Docker containers with build
docker-up-detached:
    docker compose up --build -d

# Bring down Docker containers
docker-down:
    docker compose down

# Change directory to Frontend and run npm dev
frontend:
    cd DotBelt.Frontend && npm run dev

dotnet: 
    cd DotBelt/DotBelt.CMS.API && dotnet run

# Preview Docker containers with specific compose file, build, and remove orphans
docker-preview:
    docker compose -f docker-compose.all.yml up --build --remove-orphans
    
# Generate GraphQL schema
generate-schema:
    dotnet run --project DotBelt/DotBelt.CMS.API -- schema export --output ../../DotBelt.Frontend/schema.graphql

# Codegen Typescript types (GraphQL)
frontend-codegen:
    cd DotBelt.Frontend && npm run codegen

# Update GraphQL schema and generate typescript types
graphql-update:
    just generate-schema
    just frontend-codegen

frontend-dev:
    just docker-up-detached
    just dotnet