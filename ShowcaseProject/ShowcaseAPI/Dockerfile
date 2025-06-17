FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app
COPY ./ShowcaseAPI ./ShowcaseAPI
WORKDIR /app/ShowcaseAPI
CMD ["dotnet", "watch", "run", "--no-launch-profile", "--urls", "http://+:80"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ShowcaseAPI/ShowcaseAPI.csproj", "ShowcaseAPI/"]
RUN dotnet restore "ShowcaseAPI/ShowcaseAPI.csproj"

COPY . .
WORKDIR "/src/ShowcaseAPI"
RUN dotnet build "ShowcaseAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShowcaseAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# https://github.com/palfrey/wait-for-db
COPY ShowcaseAPI/wait-for-sql.sh .
RUN chmod +x wait-for-sql.sh

COPY --from=publish /app/publish .

ENTRYPOINT ["./wait-for-sql.sh"]
