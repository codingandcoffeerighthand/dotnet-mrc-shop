FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["services/Order/Order.API/Order.API.csproj", "services/Order/Order.API/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["Shared.Messaging/Shared.Messaging.csproj", "SharedMessaging/"]
RUN dotnet restore "services/Order/Order.API/Order.API.csproj"
COPY . .
WORKDIR "/src/services/Order/Order.API"
RUN dotnet build "Order.API.csproj" -o /app/build


FROM build AS publish
RUN dotnet publish "Order.API.csproj" -o /app/publish /p:UseAppHost=false

FROM base as final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Order.API.dll" ]
