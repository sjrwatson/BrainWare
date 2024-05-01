using Api.Infrastructure;
using Api.Models;
using System.Data;

namespace Api.DataAccess;

public class OrderDataAccess : IOrderDataAccess
{
    private readonly IDatabase _database;

    public OrderDataAccess(IDatabase database)
    {
        _database = database;
    }

    public List<Order> GetOrders(int CompanyId)
    {
        // Get the orders
        var sql1 =
            "SELECT c.name, o.description, o.order_id FROM company c INNER JOIN [order] o on c.company_id=o.company_id";

        var reader1 = _database.ExecuteReader(sql1);

        var values = new List<Order>();

        while (reader1.Read())
        {
            var orderRecord = (IDataRecord)reader1;

            values.Add(new Order()
            {
                CompanyName = orderRecord.GetString(0),
                Description = orderRecord.GetString(1),
                OrderId = orderRecord.GetInt32(2),
                OrderProducts = []
            });

        }

        reader1.Close();

        return values;
    }

}
