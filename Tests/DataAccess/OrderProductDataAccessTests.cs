using Api.DataAccess;
using Api.Infrastructure;
using Moq.AutoMock;
using Moq;
using System.Data;
using Xunit;
using Api.Models;
using System.Collections.Generic;

namespace Tests.DataAccess;

public class OrderProductDataAccessTests
{
    private readonly OrderProductDataAccess _dataAccessUnderTest;
    private readonly Mock<IDatabase> _databaseMock;

    public OrderProductDataAccessTests()
    {
        var autoMock = new AutoMocker();
        _databaseMock = autoMock.GetMock<IDatabase>();

        _dataAccessUnderTest = new OrderProductDataAccess(_databaseMock.Object);
    }

    [Fact]
    public void OrderProductDataAccess_Calls_IDatabase()
    {
        //Arrange
        _databaseMock.Setup(db => db.ExecuteReader(It.IsAny<string>()))
            .Returns(MockEmptyIDataReader());

        //Act
        var result = _dataAccessUnderTest.GetOrderProducts();

        //Assert
        _databaseMock.Verify(db => db.ExecuteReader(It.IsAny<string>()), Times.Once);
        Assert.Empty(result);
    }

    private static IDataReader MockEmptyIDataReader()
    {
        var moq = new Mock<IDataReader>();
        moq.Setup(x => x.Read()).Returns(false);

        return moq.Object;
    }

    [Fact]
    public void OrderProductDataAccess_Returns_Expected_Order()
    {
        //Arrange
        _databaseMock.Setup(db => db.ExecuteReader(It.IsAny<string>()))
            .Returns(MockIDataReaderReturnsOneRow());
        var expectedResult = new List<OrderProduct>() {
            new() { OrderId = 1, ProductId = 2, Price = 10, Quantity = 1, Product = new() { Name = "product_name", Price = 10 } }
        };

        //Act
        var result = _dataAccessUnderTest.GetOrderProducts();

        //Assert
        _databaseMock.Verify(db => db.ExecuteReader(It.IsAny<string>()), Times.Once);
        Assert.NotEmpty(result);
        Assert.Equal(expectedResult[0].OrderId, result[0].OrderId);
        Assert.Equal(expectedResult[0].ProductId, result[0].ProductId);
        Assert.Equal(expectedResult[0].Price, result[0].Price);
        Assert.Equal(expectedResult[0].Quantity, result[0].Quantity);
        Assert.Equal(expectedResult[0].Product.Name, result[0].Product.Name);
        Assert.Equal(expectedResult[0].Product.Price, result[0].Product.Price);
    }

    private IDataReader MockIDataReaderReturnsOneRow()
    {
        var moq = new Mock<IDataReader>();

        bool readToggle = true;
        moq.Setup(x => x.Read())
        .Returns(() => readToggle)
        .Callback(() => readToggle = false);


        moq.Setup(x => x.GetDecimal(0)).Returns(10);
        moq.Setup(x => x.GetInt32(1)).Returns(1);
        moq.Setup(x => x.GetInt32(2)).Returns(2);
        moq.Setup(x => x.GetInt32(3)).Returns(1);
        moq.Setup(x => x.GetString(4)).Returns("product_name");
        moq.Setup(x => x.GetDecimal(5)).Returns(10);

        return moq.Object;
    }
}
