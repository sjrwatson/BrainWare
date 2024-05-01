namespace Api.Services;

using Api.DataAccess;
using Models;

public class OrderService : IOrderService
{
    private readonly IOrderDataAccess _orderDataAccess;
    private readonly IOrderProductDataAccess _orderProductDataAccess;

    public OrderService(IOrderDataAccess orderDataAccess, IOrderProductDataAccess orderProductDataAccess)
    {
        _orderDataAccess = orderDataAccess;
        _orderProductDataAccess = orderProductDataAccess;
    }

    public List<Order> GetOrdersForCompany(int CompanyId)
    {
        var orders = _orderDataAccess.GetOrders(CompanyId);
        var orderProducts = _orderProductDataAccess.GetOrderProducts();
        var companyOrders = PopulateOrderProducts(orders, orderProducts);

        return companyOrders;
    }

    private static List<Order> PopulateOrderProducts(List<Order> orders, List<OrderProduct> orderProducts)
    {
        return orders.Select(o =>
            new Order()
            {
                OrderId = o.OrderId,
                CompanyName = o.CompanyName,
                Description = o.Description,
                OrderProducts = orderProducts.Where(op => op.OrderId == o.OrderId).ToList(),
                OrderTotal = orderProducts.Where(op => op.OrderId == o.OrderId).Sum(op => op.Price * op.Quantity)
            }).ToList();
    }
}
