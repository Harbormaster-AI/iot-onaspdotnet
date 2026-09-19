FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY iotonaspdotnet/Api.csproj iotonaspdotnet/
RUN dotnet restore iotonaspdotnet/Api.csproj

COPY iotonaspdotnet/ iotonaspdotnet/

RUN dotnet publish iotonaspdotnet/Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Api.dll"]