#!env bash
##
# Generate API documentation
##

# Preparation

# see https://dotnet.github.io/docfx/index.html

# dotnet tool update -f docfx
# docfx init --quiet --output docfx

dotnet build --configuration Release
docfx docs/docfx.json --serve

# end
