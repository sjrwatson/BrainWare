using Api.Models;

namespace Api.Infrastructure
{
    public interface IOrderService
    {
        List<Order> GetOrdersForCompany(int CompanyId);
    }
}