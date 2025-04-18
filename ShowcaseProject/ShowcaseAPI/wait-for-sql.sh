#!/bin/bash
SQLSERVER_HOST=${SQLSERVER_HOST:-sqlserver}
SQLSERVER_PORT=${SQLSERVER_PORT:-1433}

echo "Wachten op SQL Server op $SQLSERVER_HOST:$SQLSERVER_PORT..."

while ! echo > /dev/tcp/$SQLSERVER_HOST/$SQLSERVER_PORT 2>/dev/null; do
  echo "SQL Server nog niet bereikbaar..."
  sleep 2
done

echo "SQL Server is beschikbaar, start API"
exec dotnet ShowcaseAPI.dll
