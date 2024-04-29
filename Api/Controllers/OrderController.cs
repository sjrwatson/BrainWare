namespace Api.Controllers;

using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;

[ApiController]
[Route("api")]
public class OrderController : ControllerBase
{
    private IOrderService _orderService;
    public OrderController(IOrderService orderService) 
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Route("order/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(IEnumerable<Order>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetOrders(int id = 1)
    {
          try
          {
            return Ok(_orderService.GetOrdersForCompany(id));
          }
          catch (Exception ex)
          {
              return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  "An unhandled error occurred.");
              //TODO: Log the error for us.
          }
    }
}
