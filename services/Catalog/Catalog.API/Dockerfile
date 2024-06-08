#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION= ASPNETCORE_ENVIRONMENT
WORKDIR /src
COPY ["services/Catalog/Catalog.API/Catalog.API.csproj", "services/Catalog/Catalog.API/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "./services/Catalog/Catalog.API/Catalog.API.csproj"
COPY . .
WORKDIR "/src/services/Catalog/Catalog.API"
RUN dotnet build "./Catalog.API.csproj" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Catalog.API.csproj"  -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]