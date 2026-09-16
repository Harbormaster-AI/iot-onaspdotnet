FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY iotonaspdotnet.Api/iotonaspdotnet.csproj iotonaspdotnet.Api/
RUN dotnet restore iotonaspdotnet.Api/iotonaspdotnet.csproj
COPY iotonaspdotnet.Api/ iotonaspdotnet.Api/
RUN dotnet publish iotonaspdotnet.Api/iotonaspdotnet.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENV ConnectionStrings__DefaultConnection="Server=host.docker.internal;Port=3307;Database=iotonaspdotnet;User=root;Password=root;"
EXPOSE 8080
ENTRYPOINT ["dotnet", "iotonaspdotnet.dll"]
