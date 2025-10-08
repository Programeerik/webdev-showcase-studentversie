FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app

COPY ShowcaseFrontend/. ShowcaseFrontend/
WORKDIR /app/ShowcaseFrontend
CMD ["dotnet", "watch", "run", "--no-launch-profile", "--urls", "http://+:80"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src/ShowcaseFrontend
COPY ShowcaseProject/ShowcaseFrontend/ShowcaseFrontend.csproj ./
RUN dotnet restore
COPY ShowcaseProject/ShowcaseFrontend/. ./
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShowcaseFrontend.dll"]
