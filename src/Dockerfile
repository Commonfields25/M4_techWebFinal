FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
ARG PROJECT_PATH
WORKDIR /src
COPY ["BuildingBlocks/Shared/M4Webapp.Shared.csproj", "BuildingBlocks/Shared/"]
COPY [$PROJECT_PATH, "Service/"]
RUN dotnet restore "Service/"
COPY . .
RUN dotnet build "Service/" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Service/" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.API.dll"] # This needs to be dynamic or one Dockerfile per service
