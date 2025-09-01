namespace OrdersService.Models;

public record CreateOrderRequest(string ProductId, int Quantity);
public record StockCheckResponse(string ProductId, bool Available);
public record OrderResult(bool Success, string Message);
