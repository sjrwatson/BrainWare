using Api.Models;

namespace Api.DataAccess
{
    public interface IOrderDataAccess
    {
        List<Order> GetOrders(int CompanyId);
    }
}