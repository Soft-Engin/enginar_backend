# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.20 AS base

# Update build environment and create a non-root user
RUN apk -U upgrade && \
    adduser -D non-root

# Switch to the non-root user
USER non-root
WORKDIR /app

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
RUN dotnet restore "./BackEngin.sln"

# Copy remaining files.
COPY . .

# Build the application
RUN dotnet build "./BackEngin.sln" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BackEngin.sln" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image with non-root user
FROM base AS final

# Set working directory and copy files
WORKDIR /app
COPY --from=publish /app/publish .

# Set non-root user explicitly (redundant if already done in `base`)
USER non-root

# Set entry point
ENTRYPOINT ["dotnet", "BackEngin.dll"]
