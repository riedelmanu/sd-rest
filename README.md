
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
