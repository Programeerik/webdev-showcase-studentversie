# Base image for running the .NET API
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ShowcaseAPI/ShowcaseAPI.csproj", "ShowcaseAPI/"]
RUN dotnet restore "ShowcaseAPI/ShowcaseAPI.csproj"

# Copy and build source
COPY . .
WORKDIR "/src/ShowcaseAPI"
RUN dotnet build "ShowcaseAPI.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "ShowcaseAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShowcaseAPI.dll"]
