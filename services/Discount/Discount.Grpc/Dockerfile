FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
USER app
WORKDIR /app
EXPOSE  8080
EXPOSE  8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src
COPY ["services/Discount/Discount.Grpc/Discount.Grpc.csproj", "services/Discount/Discount.Grpc/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "services/Discount/Discount.Grpc"
COPY .  .
WORKDIR "/src/services/Discount/Discount.Grpc"
RUN dotnet build "Discount.Grpc.csproj" -o /app/build

FROM build as publish
RUN  dotnet publish "Discount.Grpc.csproj" -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.Grpc.dll"]