using Api.Models;

namespace Api.DataAccess
{
    public interface IOrderProductDataAccess
    {
        List<OrderProduct> GetOrderProducts();
    }
}