#!/bin/bash
set -e
dotnet build
dotnet publish
pushd CluelessBackend/bin/Debug/net5.0/publish
sudo dotnet ./CluelessBackend.dll --urls="http://*:80"
popd
