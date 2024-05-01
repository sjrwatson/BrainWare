using System.Data;

namespace Api.Infrastructure
{
    public interface IDatabase
    {
        int ExecuteNonQuery(string query);
        IDataReader ExecuteReader(string query);
    }
}