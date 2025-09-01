using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Models;

namespace OrdersService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OrdersController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var client = _httpClientFactory.CreateClient("inventory");

        // 1) Consultar stock (síncrono request/response)
        var check = await client.GetFromJsonAsync<StockCheckResponse>($"/api/inventory/check/{request.ProductId}/{request.Quantity}");
        if (check is null) return StatusCode(502, new { Error = "InventoryService no respondió" });

        if (!check.Available)
            return BadRequest(new { Success = false, Message = "Stock insuficiente" });

        // 2) Confirmar el pedido en InventoryService
        var resp = await client.PostAsJsonAsync("/api/inventory/order", new { request.ProductId, request.Quantity });
        var result = await resp.Content.ReadFromJsonAsync<OrderResult>();

        if (resp.IsSuccessStatusCode && result is not null && result.Success)
            return Created($"/api/orders/{Guid.NewGuid()}", new { Success = true, Message = "Pedido confirmado" });

        return StatusCode(502, new { Success = false, Message = result?.Message ?? "Error confirmando pedido" });
    }
}
