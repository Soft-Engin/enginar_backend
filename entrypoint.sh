#!/bin/sh
set -e

if [ "$MIGRATE_DATABASE" = "true" ]; then
    echo "MIGRATE_DATABASE is set to true. Running database migration..."
    /app/db-migration
else
    echo "MIGRATE_DATABASE is not set or false. Skipping database migration."
fi

echo "Starting the application..."
exec dotnet /app/BackEngin.dll
