using System;
using System.Collections.Generic;
using System.Linq;
using Api.Controllers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Tests.Controllers;

public class OrderControllerTests
{
    private readonly OrderController _controllerUnderTest;
    private readonly Mock<IOrderService> _orderServiceMock;
    private readonly Mock<ILogger<OrderController>> _loggerMock;

    public  OrderControllerTests()
    {

        var autoMock = new AutoMocker();
        _orderServiceMock = autoMock.GetMock<IOrderService>();
        _loggerMock = new Mock<ILogger<OrderController>>();

        _controllerUnderTest = new OrderController(_orderServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void OrderController_Calls_OrderService_Once()
    {
        //Arrange
        //Act
        var result = _controllerUnderTest.GetOrders();

        //Assert
        _orderServiceMock.Verify(o => o.GetOrdersForCompany(It.IsAny<int>()), Times.Once);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void OrderController_Returns_500_When_Service_Throws_Exception()
    {
        //Arrange
        _orderServiceMock
            .Setup(o => o.GetOrdersForCompany(It.IsAny<int>()))
            .Throws<Exception>();

        //Act
        var result = _controllerUnderTest.GetOrders();

        //Assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(((ObjectResult)result).StatusCode, StatusCodes.Status500InternalServerError);
    }

    [Fact]
    public void OrderController_Returns_List_From_Service()
    {
        //Arrange
        var orders = new List<Order>() {
            new() { CompanyName = "comp_1", Description = "order_1",  OrderId = 1, OrderProducts = null, OrderTotal = 100 },
            new() { CompanyName = "comp_1", Description = "order_2",  OrderId = 2, OrderProducts = null, OrderTotal = 200 },
            new() { CompanyName = "comp_1", Description = "order_3",  OrderId = 3, OrderProducts = null, OrderTotal = 300 },
        };
        _orderServiceMock
            .Setup(o =>  o.GetOrdersForCompany(It.IsAny<int>()))
            .Returns(orders);

        //Act
        var result = _controllerUnderTest.GetOrders() as ObjectResult;
        var resultList =  (List<Order>)result!.Value!;

        //Assert
        _orderServiceMock.Verify(o => o.GetOrdersForCompany(It.IsAny<int>()), Times.Once);
        Assert.Equal(3, resultList.Count);
        Assert.Equal(orders.ElementAt(0), resultList.ElementAt(0));
        Assert.Equal(orders.ElementAt(1), resultList.ElementAt(1));
        Assert.Equal(orders.ElementAt(2), resultList.ElementAt(2));
    }
}

