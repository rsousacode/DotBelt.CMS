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


#########################################
# Bring up Docker containers
#########################################

docker-up:
    docker compose up --build
    
#########################################
# Bring up Docker containers (Dettached)
#########################################    

docker-up-detached:
    docker compose up --build -d


#########################################
# Bring down Docker containers
#########################################    

docker-down:
    docker compose down


#########################################
# Run frontend development
#########################################    

frontend:
    cd DotBelt.Frontend && npm run dev
    

#########################################
# Run dotnet API 
#########################################    

dotnet: 
    cd DotBelt/DotBelt.CMS.API && dotnet run

#########################################
# Update database
#########################################    

database-update: 
    cd DotBelt/DotBelt.CMS.Shared && dotnet ef database update


#########################################
# Preview all containers
#########################################    

docker-preview:
    docker compose -f docker-compose.all.yml up --build --remove-orphans
    

#########################################
# Generate schema.graphql and copy to
# frontend directory
#########################################    

generate-schema:
    dotnet run --project DotBelt/DotBelt.CMS.API -- schema export --output ../../DotBelt.Frontend/schema.graphql


#########################################
# Generate TS types based on the GraphQL
# schema
#########################################    

frontend-codegen:
    cd DotBelt.Frontend && npm run codegen

#########################################
# Updates GraphQL schema and TS types
#########################################    

graphql-update:
    just generate-schema
    just frontend-codegen

#########################################
# Runs everything necessary for frontend
# development
#########################################    

frontend-dev:
    just docker-up-detached
    just dotnet

#########################################
# Gets swagger.json
#########################################        

gen-swagger-json: 
    cd DotBelt/DotBelt.CMS.API && dotnet run swagger --output ../../DotBelt.Frontend/swagger.json



#########################################
# Gen TS types based on swagger.json
#########################################        

gen-swagger-ts:
    cd DotBelt/DotBelt.Frontend && swagger-typescript-api -p ./swagger.json -o ./src/lib/Swagger/generated --module-name-first-tag  --route-types --modular --unwrap-response-data --single-http-client


#########################################
# Gets swagger.json & Gen TS types based 
# on swagger.json
#########################################        

swagger:
   just gen-swagger-json
   just gen-swagger-ts


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
