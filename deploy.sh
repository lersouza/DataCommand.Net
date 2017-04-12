#!/bin/bash
if [ "$TRAVIS_BRANCH" == "master" ]; then
	dotnet pack -c Release -o ./release
	nuget push ./src/DataCommand.Core/release/*.nupkg $NUGET_API_KEY -Source https://www.nuget.org/api/v2/package
fi