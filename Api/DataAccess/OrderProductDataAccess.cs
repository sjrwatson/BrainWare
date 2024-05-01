using Api.Infrastructure;
using Api.Models;
using System.Data;

namespace Api.DataAccess;

public class OrderProductDataAccess : IOrderProductDataAccess
{
    private readonly IDatabase _database;

    public OrderProductDataAccess(IDatabase database)
    {
        _database = database;
    }

    public List<OrderProduct> GetOrderProducts()
    {
        //Get the order products
        var sql2 =
            "SELECT op.price, op.order_id, op.product_id, op.quantity, p.name, p.price FROM orderproduct op INNER JOIN product p on op.product_id=p.product_id";

        var reader2 = _database.ExecuteReader(sql2);

        var orderProducts = new List<OrderProduct>();

        while (reader2.Read())
        {
            var record2 = (IDataRecord)reader2;

            orderProducts.Add(new OrderProduct()
            {
                OrderId = record2.GetInt32(1),
                ProductId = record2.GetInt32(2),
                Price = record2.GetDecimal(0),
                Quantity = record2.GetInt32(3),
                Product = new Product()
                {
                    Name = record2.GetString(4),
                    Price = record2.GetDecimal(5)
                }
            });
        }

        reader2.Close();

        return orderProducts;
    }
}
