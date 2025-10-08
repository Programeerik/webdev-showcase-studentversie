FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app

COPY ShowcaseFrontend/. ShowcaseFrontend/
WORKDIR /app/ShowcaseFrontend
CMD ["dotnet", "watch", "run", "--no-launch-profile", "--urls", "http://+:80"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ShowcaseFrontend/ShowcaseFrontend.csproj ShowcaseFrontend/
RUN dotnet restore ./ShowcaseFrontend/ShowcaseFrontend.csproj
COPY ShowcaseFrontend/. ShowcaseFrontend/
WORKDIR /src/ShowcaseFrontend
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShowcaseFrontend.dll"]
