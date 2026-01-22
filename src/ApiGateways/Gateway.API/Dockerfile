FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src
COPY ["src/ApiGateways/Gateway.API/Gateway.API.csproj", "src/ApiGateways/Gateway.API/"]
RUN dotnet restore "src/ApiGateways/Gateway.API/Gateway.API.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/Gateway.API"
RUN dotnet build "Gateway.API.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Gateway.API.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.API.dll"]
