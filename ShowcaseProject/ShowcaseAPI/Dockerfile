# Base image for running the .NET API
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Development stage (voor dotnet watch)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app
COPY ./ShowcaseAPI ./ShowcaseAPI
WORKDIR /app/ShowcaseAPI
CMD ["dotnet", "watch", "run", "--no-launch-profile", "--urls", "http://+:80"]

# Build stage (voor productie)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ShowcaseAPI/ShowcaseAPI.csproj", "ShowcaseAPI/"]
RUN dotnet restore "ShowcaseAPI/ShowcaseAPI.csproj"

# Copy and build source
COPY . .
WORKDIR "/src/ShowcaseAPI"
RUN dotnet build "ShowcaseAPI.csproj" -c Release -o /app/build

# Publish (voor productie)
FROM build AS publish
RUN dotnet publish "ShowcaseAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final production stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Kopieer wait script
COPY ShowcaseAPI/wait-for-sql.sh .
RUN chmod +x wait-for-sql.sh

# Kopieer build output
COPY --from=publish /app/publish .

# Start script dat wacht op SQL Server
ENTRYPOINT ["./wait-for-sql.sh"]
