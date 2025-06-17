FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app

COPY ShowcaseFrontend/ShowcaseFrontend.csproj ShowcaseFrontend/
RUN dotnet restore ShowcaseFrontend/ShowcaseFrontend.csproj

COPY ShowcaseFrontend/. ShowcaseFrontend/

WORKDIR /app/ShowcaseFrontend
CMD ["dotnet", "watch", "run", "--project", "/app/ShowcaseFrontend/ShowcaseFrontend.csproj", "--no-launch-profile", "--urls=http://0.0.0.0:80"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ShowcaseFrontend/ShowcaseFrontend.csproj", "ShowcaseFrontend/"]
COPY ["ShowcaseAPI/ShowcaseAPI.csproj", "ShowcaseAPI/"]
RUN dotnet restore "./ShowcaseFrontend/ShowcaseFrontend.csproj"
COPY . . 
WORKDIR "/src/ShowcaseFrontend"
RUN dotnet build "./ShowcaseFrontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./ShowcaseFrontend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShowcaseFrontend.dll"]
