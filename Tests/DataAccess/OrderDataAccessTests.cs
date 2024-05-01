using System.Collections.Generic;
using System.Data;
using Api.DataAccess;
using Api.Infrastructure;
using Api.Models;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Tests.DataAccess;

public class OrderDataAccessTests
{
    private readonly OrderDataAccess _dataAccessUnderTest;
    private readonly Mock<IDatabase> _databaseMock;

    public OrderDataAccessTests()
    {
        var autoMock = new AutoMocker();
        _databaseMock = autoMock.GetMock<IDatabase>();

        _dataAccessUnderTest = new OrderDataAccess(_databaseMock.Object);
    }

    [Fact]
    public void OrderDataAccess_Calls_IDatabase()
    {
        //Arrange
        _databaseMock.Setup(db => db.ExecuteReader(It.IsAny<string>()))
            .Returns(MockEmptyIDataReader());

        //Act
        var result = _dataAccessUnderTest.GetOrders(1);

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
    public void OrderDataAccess_Returns_Expected_Order()
    {
        //Arrange
        _databaseMock.Setup(db => db.ExecuteReader(It.IsAny<string>()))
            .Returns(MockIDataReaderReturnsOneRow());
        var expectedResult = new List<Order>() { 
            new() { CompanyName = "company_name", Description = "description", OrderId = 1 } 
        };

        //Act
        var result = _dataAccessUnderTest.GetOrders(1);

        //Assert
        _databaseMock.Verify(db => db.ExecuteReader(It.IsAny<string>()), Times.Once);
        Assert.NotEmpty(result);
        Assert.Equal(expectedResult[0].CompanyName, result[0].CompanyName);
        Assert.Equal(expectedResult[0].Description, result[0].Description);
        Assert.Equal(expectedResult[0].OrderId, result[0].OrderId);
    }

    private IDataReader MockIDataReaderReturnsOneRow()
    {
        var moq = new Mock<IDataReader>();

        bool readToggle = true;
        moq.Setup(x => x.Read())
        .Returns(() => readToggle)
        .Callback(() => readToggle = false);

        moq.Setup(x => x.GetString(0)).Returns("company_name");
        moq.Setup(x => x.GetString(1)).Returns("description");
        moq.Setup(x => x.GetInt32(2)).Returns(1);

        return moq.Object;
    }
}
