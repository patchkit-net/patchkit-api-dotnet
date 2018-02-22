#!/bin/bash

./create-package.sh $1

# Remove previous packages stored in local feed
rm -r ~/.nuget_local/${1,,}

# Add new package to local feed
nuget add $1/*.nupkg -Source ~/.nuget_local

echo $1 has been published locally