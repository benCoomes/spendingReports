FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY ./web/web.csproj ./web/web.csproj
RUN dotnet restore ./web/web.csproj

COPY . ./
RUN dotnet publish -c Release -o out ./web/web.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "web.dll"]