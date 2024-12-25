# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.20 AS base

# Update build environment and create a non-root user
RUN apk --no-cache -U upgrade && \
    adduser -D non-root

# Expose required ports
EXPOSE 8080

# Use dotnet SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.20 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all .csproj files and restore dependencies
COPY BackEngin/BackEngin.csproj ./BackEngin/.
COPY DataAccess/DataAccess.csproj ./DataAccess/.
COPY Models/Models.csproj ./Models/.
COPY BackEngin.Tests/BackEngin.Tests.csproj ./BackEngin.Tests/.
COPY BackEngin.sln . 
RUN dotnet restore

# Copy remaining files.
COPY BackEngin/* ./BackEngin/.
COPY DataAccess/* ./DataAccess/.
COPY Models/* ./Models/.
COPY BackEngin.Tests/* ./BackEngin.Tests/.
COPY BackEngin.sln .

# Build the application
RUN dotnet build \
-c $BUILD_CONFIGURATION \
-p:UseAppHost=false \
-p:RunAnalyzers=false \
--no-restore 

# Create the database migration.
FROM build AS migrations
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
RUN dotnet tool install --global dotnet-ef --version 8.0.10
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet-ef migrations bundle \
--configuration $BUILD_CONFIGURATION \
--project DataAccess \
--startup-project BackEngin \
--output /app/migration/db-migration \
--no-build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
RUN dotnet publish \
-c $BUILD_CONFIGURATION \
-p:UseAppHost=false \
-p:PublishDir=/app/publish \
--no-restore \
--no-build

# Final image with non-root user
FROM base AS final

# Set working directory and copy files
WORKDIR /app

COPY --from=publish /app/publish .
COPY --from=migrations /app/migration/db-migration .
COPY entrypoint.sh /app/entrypoint.sh

RUN chmod +x /app/entrypoint.sh /app/db-migration

# Set non-root user explicitly (redundant if already done in `base`)
USER non-root

# Set entry point
ENTRYPOINT ["./entrypoint.sh"]
