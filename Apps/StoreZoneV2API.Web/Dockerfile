# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StoreZoneV2API.Web/StoreZoneV2API.Web.csproj", "StoreZoneV2API.Web/"]
COPY ["StoreZoneV2API.Application/StoreZoneV2API.Application.csproj", "StoreZoneV2API.Application/"]
COPY ["StoreZoneV2API.Domain/StoreZoneV2API.Domain.csproj", "StoreZoneV2API.Domain/"]
COPY ["StoreZoneV2API.Infrastructure/StoreZoneV2API.Infrastructure.csproj", "StoreZoneV2API.Infrastructure/"]
RUN dotnet restore "./StoreZoneV2API.Web/StoreZoneV2API.Web.csproj"
COPY . .
WORKDIR "/src/StoreZoneV2API.Web"
RUN dotnet build "./StoreZoneV2API.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StoreZoneV2API.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StoreZoneV2API.Web.dll"]