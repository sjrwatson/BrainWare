namespace Api.Controllers;

using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

[ApiController]
[Route("api")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderService orderService, ILogger<OrderController> logger) 
    {
        _orderService = orderService;
        _logger = logger;
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
            _logger.LogError(ex.Message, ex);

            return StatusCode(
                  StatusCodes.Status500InternalServerError,
                  "An unhandled error occurred.");
          }
    }
}
