using System.Collections.Generic;
using Api.DataAccess;
using Api.Models;
using Api.Services;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Tests.Services;

public class OrderServiceTests
{
    private readonly OrderService _serviceUnderTest;
    private readonly Mock<IOrderDataAccess> _orderDataAccessMock;
    private readonly Mock<IOrderProductDataAccess> _orderProductDataAccessMock;

    public OrderServiceTests()
    {
        var autoMock = new AutoMocker();
        _orderDataAccessMock = autoMock.GetMock<IOrderDataAccess>();
        _orderProductDataAccessMock = autoMock.GetMock<IOrderProductDataAccess>();

        _serviceUnderTest = new OrderService(_orderDataAccessMock.Object, _orderProductDataAccessMock.Object);
    }

    [Fact]
    public void OrderService_Calls_OrderData_And_OrderProductData()
    {
        //Arrange
        var orders = new List<Order>();
        var orderProducts = new List<OrderProduct>();

        _orderDataAccessMock
            .Setup(o => o.GetOrders(It.IsAny<int>()))
            .Returns(orders);
        _orderProductDataAccessMock
            .Setup(op => op.GetOrderProducts())
            .Returns(orderProducts);

        //Act
        var result = _serviceUnderTest.GetOrdersForCompany(1);

        //Assert
        _orderDataAccessMock.Verify(o => o.GetOrders(It.IsAny<int>()), Times.Once);
        _orderProductDataAccessMock.Verify(op => op.GetOrderProducts(), Times.Once);
        Assert.Empty(result);
    }

    [Fact]
    public void OrderService_Pairs_OrderProducts_With_Order()
    {
        //Arrange
        var orders = new List<Order>()
        {
            new() { CompanyName = "comp_1", Description = "company numero uno", OrderId = 1 },
            new() { CompanyName = "comp_2", Description = "company 2", OrderId = 2 },
            new() { CompanyName = "comp_3", Description = "company 3", OrderId = 3 },
        };
        var orderProducts = new List<OrderProduct>()
        {
            new() { OrderId = 1, Price = 10, ProductId = 1, Quantity = 10, Product = new(){ Name = "prod_1", Price = 10 } },
            new() { OrderId = 1, Price = 3, ProductId = 2, Quantity = 3, Product = new(){ Name = "prod_2", Price = 3 } },
            new() { OrderId = 3, Price = 10, ProductId = 3, Quantity = 10, Product = new(){ Name = "prod_3", Price = 10} },
        };
        var expectedOutput = new List<Order>()
        {
            new() { 
                CompanyName = "comp_1", 
                Description = "company numero uno", 
                OrderId = 1, 
                OrderTotal = 109,
                OrderProducts = [ 
                    new() { OrderId = 1, Price = 10, ProductId = 1, Quantity = 10, Product = new(){ Name = "prod_1", Price = 10 } } ,
                    new() { OrderId = 1, Price = 3, ProductId = 2, Quantity = 3, Product = new(){ Name = "prod_2", Price = 3 } },
                ] 
            },
            new() { CompanyName = "comp_2", Description = "company 2", OrderId = 2 },
            new() { CompanyName = "comp_3", Description = "company 3", OrderId = 3, OrderTotal = 100,
                OrderProducts = [
                    new() { OrderId = 3, Price = 10, ProductId = 3, Quantity = 10, Product = new(){ Name = "prod_3", Price = 10} },
                ]
            },
        };

        _orderDataAccessMock
            .Setup(o => o.GetOrders(It.IsAny<int>()))
            .Returns(orders);
        _orderProductDataAccessMock
            .Setup(op => op.GetOrderProducts())
            .Returns(orderProducts);

        //Act
        var result = _serviceUnderTest.GetOrdersForCompany(1);

        //Assert
        _orderDataAccessMock.Verify(o => o.GetOrders(It.IsAny<int>()), Times.Once);
        _orderProductDataAccessMock.Verify(op => op.GetOrderProducts(), Times.Once);
        Assert.NotEmpty(result);
        AssertVerifyOrderLists(expectedOutput, result);
    }

    private static void AssertVerifyOrderLists(List<Order> expectedOrders, List<Order> actualOrders)
    {
        Assert.Equal(expectedOrders.Count, actualOrders.Count);

        for(int orderCount = 0; orderCount < expectedOrders.Count; orderCount++)
        {
            Assert.Equal(expectedOrders[orderCount].OrderId, actualOrders[orderCount].OrderId);
            Assert.Equal(expectedOrders[orderCount].CompanyName, actualOrders[orderCount].CompanyName);
            Assert.Equal(expectedOrders[orderCount].Description, actualOrders[orderCount].Description);
            Assert.Equal(expectedOrders[orderCount].OrderTotal, actualOrders[orderCount].OrderTotal);

            for(int orderProductsCount = 0; 
                orderProductsCount < expectedOrders[orderCount].OrderProducts.Count; 
                orderProductsCount++)
            {
                Assert.Equal(expectedOrders[orderCount].OrderProducts[orderProductsCount].ProductId, 
                    actualOrders[orderCount].OrderProducts[orderProductsCount].ProductId);
                Assert.Equal(expectedOrders[orderCount].OrderProducts[orderProductsCount].OrderId,
                    actualOrders[orderCount].OrderProducts[orderProductsCount].OrderId);
                Assert.Equal(expectedOrders[orderCount].OrderProducts[orderProductsCount].Quantity,
                    actualOrders[orderCount].OrderProducts[orderProductsCount].Quantity);
                Assert.Equal(expectedOrders[orderCount].OrderProducts[orderProductsCount].Product.Name,
                    actualOrders[orderCount].OrderProducts[orderProductsCount].Product.Name);
                Assert.Equal(expectedOrders[orderCount].OrderProducts[orderProductsCount].Product.Price,
                    actualOrders[orderCount].OrderProducts[orderProductsCount].Product.Price);
            }
        }
    }
}
