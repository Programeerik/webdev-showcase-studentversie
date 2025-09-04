FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Dev (hot reload)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app
COPY ShowcaseProject/ShowcaseFrontend/ShowcaseFrontend.csproj ShowcaseProject/ShowcaseFrontend/
RUN dotnet restore ShowcaseProject/ShowcaseFrontend/ShowcaseFrontend.csproj
COPY ShowcaseProject/ShowcaseFrontend/. ShowcaseProject/ShowcaseFrontend/
WORKDIR /app/ShowcaseProject/ShowcaseFrontend
CMD ["dotnet","watch","run","--project","/app/ShowcaseProject/ShowcaseFrontend/ShowcaseFrontend.csproj","--no-launch-profile","--urls=http://0.0.0.0:80"]

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ShowcaseProject/ShowcaseFrontend/ShowcaseFrontend.csproj ShowcaseFrontend/
RUN dotnet restore ShowcaseFrontend/ShowcaseFrontend.csproj
COPY ShowcaseProject/ShowcaseFrontend/. ShowcaseFrontend/
WORKDIR /src/ShowcaseFrontend
RUN dotnet build ShowcaseFrontend.csproj -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish ShowcaseFrontend.csproj -c Release -o /app/publish /p:UseAppHost=false

# Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","ShowcaseFrontend.dll"]
