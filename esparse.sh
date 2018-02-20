#!/usr/bin/env bash
[[ -e esparse.sh ]] || { echo >&2 "Please cd into the script location before running it."; exit 1; }
set -e
BINPATH=bin/Release/netcoreapp2.0/Esparse.dll
if [[ -e $BINPATH ]]; then
    dotnet "$BINPATH" $@
else
    echo>&2 Esparse release build not found. Did you forget to build?
    exit 1
fi
