# Build image (SDK)
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copy csproj(s) and restore first to leverage cache
COPY ["src/ApiGateways/Gateway.API/Gateway.API.csproj", "src/ApiGateways/Gateway.API/"]
RUN dotnet restore "src/ApiGateways/Gateway.API/Gateway.API.csproj"

# Copy rest of sources
COPY . .

WORKDIR "/src/src/ApiGateways/Gateway.API"
RUN dotnet build "Gateway.API.csproj" -c Release -o /app/build
RUN dotnet publish "Gateway.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image (smaller surface)
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS runtime
WORKDIR /app

# Create non-root user for better security
ARG DOTNET_USER=app
RUN addgroup --system $DOTNET_USER && adduser --system --ingroup $DOTNET_USER $DOTNET_USER

# Expose the container port (Kestrel will bind to this)
EXPOSE 80

# Copy published output
COPY --from=build /app/publish .

# Recommended environment variables for container behaviour
ENV ASPNETCORE_URLS="http://+:80" \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Set correct ownership and switch to non-root user
RUN chown -R $DOTNET_USER:$DOTNET_USER /app
USER $DOTNET_USER

# Health check (optional, adjust path)
HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
  CMD curl -f http://localhost/health || exit 1

ENTRYPOINT ["dotnet", "Gateway.API.dll"]
