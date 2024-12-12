FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.20 AS base

# Update build environment
RUN apk -U upgrade

# Create another user for safety.
USER app
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

# Use dotnet build environment.
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.20 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN dotnet restore "./BackEngin.sln"
RUN dotnet build "./BackEngin.sln" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BackEngin.sln" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Leave last container with dll file and run.
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackEngin.dll"]