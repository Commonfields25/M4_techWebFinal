# M4Webapp Version 2.0 (Microservices)

## Architecture
- **8 Microservices** (Identity, Catalog, Taxonomy, Engagement, Inquiry, Notification, Certification, Reporting)
- **API Gateway**: YARP based
- **Frontend**: Blazor WebAssembly (with i18n support)
- **Communication**: gRPC (synchronous) & MassTransit/RabbitMQ (asynchronous)
- **Database**: PostgreSQL (per service)
- **Monitoring**: Grafana LGTM Stack (Loki, Grafana, Tempo, Mimir) + OpenTelemetry

## Quick Start
1. Ensure Docker is installed.
2. Run `docker-compose up --build`.
3. Access the Gateway at http://localhost:5000.
4. Access Grafana at http://localhost:3000.

## Structure
- `src/Services`: Microservices APIs
- `src/ApiGateways`: YARP Gateway
- `src/Web`: Blazor WASM Frontend
- `src/BuildingBlocks`: Shared libraries, gRPC Protos, and Integration Events.
