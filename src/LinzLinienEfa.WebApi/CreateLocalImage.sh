#!/bin/bash

dotnet restore
dotnet publish -c Release -o out
docker build -t linz-linien-efa-webapi .
