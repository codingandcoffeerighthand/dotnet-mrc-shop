FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["services/Basket/Basket.API/Basket.API.csproj", "services/Basket/Basket.API/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "services/Basket/Basket.API/Basket.API.csproj"
COPY . .
WORKDIR "/src/services/Basket/Basket.API"
RUN dotnet build "Basket.API.csproj" -o /app/build


FROM build AS publish
RUN dotnet publish "Basket.API.csproj" -o /app/publish /p:UseAppHost=false

FROM base as final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Basket.API.dll" ]
