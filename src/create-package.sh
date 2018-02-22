#!/bin/bash

pushd $1
# Rebuild the project
msbuild $1.csproj /t:Build /p:Configuration=Release

# Remove previous packages
rm *.nupkg

# Create new package
nuget pack $1.csproj -Prop Configuration=Release -MsbuildPath /usr/lib/mono/msbuild/15.0/bin/
popd

echo $1 has been created