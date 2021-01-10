FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY ./webApi/webApi.csproj ./webApi/webApi.csproj
RUN dotnet restore ./webApi/webApi.csproj

COPY . ./
RUN dotnet publish -c Release -o out ./webApi/webApi.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "webApi.dll"]