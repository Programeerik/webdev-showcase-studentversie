FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app

COPY ./ShowcaseAPI ./ShowcaseAPI
WORKDIR /app/ShowcaseAPI
CMD ["dotnet", "watch", "run", "--no-launch-profile", "--urls", "http://+:80"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore ./ShowcaseAPI/ShowcaseAPI.csproj
WORKDIR /src/ShowcaseAPI
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShowcaseAPI.dll"]
