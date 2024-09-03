# ivrit font
##############################################################
#   
#     _____                _                 _                  
#    |  ___| __ ___  _ __ | |_ ___ _ __   __| |                 
#    | |_ | '__/ _ \| '_ \| __/ _ \ '_ \ / _` |                 
#    |  _|| | | (_) | | | | ||  __/ | | | (_| |                 
#    |_|  |_|  \___/|_| |_|\__\___|_| |_|\__,_|                 
#                     _   _                _                  _ 
#      __ _ _ __   __| | | |__   __ _  ___| | _____ _ __   __| |
#     / _` | '_ \ / _` | | '_ \ / _` |/ __| |/ / _ \ '_ \ / _` |
#    | (_| | | | | (_| | | |_) | (_| | (__|   <  __/ | | | (_| |
#     \__,_|_| |_|\__,_| |_.__/ \__,_|\___|_|\_\___|_| |_|\__,_|
#                                                               
#   
##############################################################

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
    


#        _    ___ 
#       / \  |_ _|
#      / _ \  | | 
#     / ___ \ | | 
#    /_/   \_\___|
#                 

mistral_model_url := "https://huggingface.co/TheBloke/Mistral-7B-Instruct-v0.2-GGUF/resolve/main/mistral-7b-instruct-v0.2.Q8_0.gguf"
mistral_file_name := "mistral-7b-instruct-v0.2.Q8_0.gguf"

download-mistral-model:
    cd AI/models/7B && curl -L {{mistral_model_url}} --output {{mistral_file_name}}
    
run-mistral:
    docker run -v .AI/models/7B:/models -p 8000:8000 ghcr.io/ggerganov/llama.cpp:server -m /models/7B/{{mistral_file_name}} --port 8000 --host 0.0.0.0 -n 512

docker-up-ai:
    docker-compose -f docker-compose.ai.yml