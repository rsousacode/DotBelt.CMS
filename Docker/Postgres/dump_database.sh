#!/bin/bash

# Check if both arguments are provided
if [ $# -ne 2 ]; then
  echo "Usage: $0 <container_id> <database_name>"
  exit 1
fi

CONTAINER_ID=$1
DB_NAME=$2

# Execute the docker command
docker exec -t $CONTAINER_ID pg_dump -d postgres://postgres@localhost:5432/$DB_NAME --no-owner > init.sql