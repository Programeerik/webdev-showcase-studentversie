# Base image with only runtime (for production)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Development stage with full .NET SDK for hot reload
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app

# Copy only the project files first to leverage Docker cache
COPY ShowcaseFrontend/ShowcaseFrontend.csproj ShowcaseFrontend/
RUN dotnet restore ShowcaseFrontend/ShowcaseFrontend.csproj

# Copy the rest of the source code
COPY ShowcaseFrontend/. ShowcaseFrontend/

WORKDIR /app/ShowcaseFrontend
CMD ["dotnet", "watch", "run", "--project", "/app/ShowcaseFrontend/ShowcaseFrontend.csproj", "--no-launch-profile", "--urls=http://0.0.0.0:80"]

# Build stage for production
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ShowcaseFrontend/ShowcaseFrontend.csproj ShowcaseFrontend/
RUN dotnet restore "./ShowcaseFrontend/ShowcaseFrontend.csproj"
COPY . . 
WORKDIR "/src/ShowcaseFrontend"
RUN dotnet build "./ShowcaseFrontend.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "./ShowcaseFrontend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final production stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShowcaseFrontend.dll"]
