using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private static readonly Dictionary<string, int> Stock = new()
    {
        ["P001"] = 100,
        ["P002"] = 50
    };

    [HttpGet("check/{productId}/{quantity}")]
    public IActionResult CheckStock(string productId, int quantity)
    {
        bool available = Stock.TryGetValue(productId, out var stockQty) && stockQty >= quantity;
        return Ok(new StockCheckResponse(productId, available));
    }

    [HttpPost("order")]
    public IActionResult CreateOrder([FromBody] OrderRequest request)
    {
        if (Stock.TryGetValue(request.ProductId, out var stockQty) && stockQty >= request.Quantity)
        {
            Stock[request.ProductId] -= request.Quantity;
            return Ok(new OrderResult(true, "Order created."));
        }
        return BadRequest(new OrderResult(false, "Insufficient stock."));
    }
}

public record OrderRequest(string ProductId, int Quantity);
public record OrderResult(bool Success, string Message);
public record StockCheckResponse(string ProductId, bool Available);
