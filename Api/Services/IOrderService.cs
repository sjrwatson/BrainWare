using Api.Models;

namespace Api.Services
{
    public interface IOrderService
    {
        List<Order> GetOrdersForCompany(int CompanyId);
    }
}