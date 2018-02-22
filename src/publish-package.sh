#!/bin/bash

read -rsp $'Are you sure? Publishing package to nuget.org cannot be undone. Press enter to continue...\n'

./create-package.sh $1

nuget push $1/*.nupkg -Source https://api.nuget.org/v3/index.json

echo $1 has been published to nuget.org