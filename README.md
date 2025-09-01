# SyncRestExercise – Orders → Inventory (REST síncrono, .NET 8 + Docker)

## Requisitos
- Docker + Docker Compose
- (Opcional) .NET 8 SDK si querés compilar/runnear sin Docker
- Visual Studio 2022/VS Code

## Ejecutar con Docker
```bash
docker compose up --build
```
- Inventory: http://localhost:5001/swagger
- Orders:    http://localhost:5002/swagger

### Pruebas
```bash
# Chequear stock en Inventory
curl http://localhost:5001/api/inventory/check/P001/3

# Crear pedido desde Orders (consulta + confirmación)
curl -X POST http://localhost:5002/api/orders   -H "Content-Type: application/json"   -d '{"productId":"P001","quantity":3}'
```

## Ejecutar en Visual Studio
- Abrí la carpeta `SyncRestExercise` con **Open Folder**.
- Seteá proyecto de inicio (Startup Project) a `InventoryService` y `OrdersService` en perfiles separados, o usá Docker para ambos.
