#!/bin/bash
if [ "$TRAVIS_BRANCH" == "master" ]; then
	dotnet pack -c Release -o ./release
	dotnet nuget push ./src/DataCommand.Core/release/*.nupkg -k $NUGET_API_KEY -s https://www.nuget.org/api/v2/package
fi