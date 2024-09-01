# Justfile

# Bring up Docker containers with build
docker-up:
    docker compose up --build

# Bring down Docker containers
docker-down:
    docker compose down

# Change directory to Frontend and run npm dev
frontend:
    cd DotBelt.Frontend && npm run dev

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
