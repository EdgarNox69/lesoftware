
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS with-node
RUN apt-get update
RUN apt-get install curl
RUN curl -sL https://deb.nodesource.com/setup_20.x | bash
RUN apt-get -y install nodejs

# Esta fase se usa para compilar el proyecto de servicio
FROM with-node AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["lesoftware.client/nuget.config", "lesoftware.client/"]
COPY ["lesoftware.Server/lesoftware.Server.csproj", "lesoftware.Server/"]
COPY ["lesoftware.client/lesoftware.client.esproj", "lesoftware.client/"]
RUN dotnet restore "./lesoftware.Server/lesoftware.Server.csproj"
COPY . .
WORKDIR "/src/lesoftware.Server"
RUN dotnet build "./lesoftware.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./lesoftware.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lesoftware.Server.dll"]