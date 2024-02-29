namespace Api.Controllers
{
  using Infrastructure;
  using Microsoft.AspNetCore.Mvc;
  using Models;

  [ApiController]
  [Route("api")]
  public class OrderController : ControllerBase
  {
    [HttpGet]
    [Route("order/{id}")]

    public IEnumerable<Order> GetOrders(int id = 1)
    {
      var data = new OrderService();

      return data.GetOrdersForCompany(id);
    }
  }
}
