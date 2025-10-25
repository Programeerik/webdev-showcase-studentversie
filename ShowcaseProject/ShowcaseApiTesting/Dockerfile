FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ShowcaseApiTesting/ShowcaseApiTesting.csproj", "ShowcaseApiTesting/"]
COPY ["ShowcaseAPI/ShowcaseAPI.csproj", "ShowcaseAPI/"]
RUN dotnet restore "ShowcaseAPI/ShowcaseAPI.csproj"
RUN dotnet restore "ShowcaseApiTesting/ShowcaseApiTesting.csproj"

COPY . .
WORKDIR "/src/ShowcaseApiTesting"

RUN dotnet build "ShowcaseApiTesting.csproj" -c Release --no-restore

CMD ["dotnet", "test", "ShowcaseApiTesting.csproj", "-c", "Release", "--logger:trx;LogFileName=test_results.trx", "--results-directory", "./TestResults", "--verbosity", "normal"]
