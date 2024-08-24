#!/bin/bash
echo -n "Type 'a' to use container ID or type 'b' to use image name: "
read choice
echo -n "Type image name or container ID: "
read name


if [[ "$choice" == "a" ]]; then
    docker exec -t $name pg_dump -d postgres://postgres@localhost:5432/Meteo --no-owner  > init.sql
else 
    docker exec -t $(docker ps | grep $name | awk '{ print $1 }') pg_dump -d postgres://postgres@localhost:5432/Meteo --no-owner   > init.sql
fi

