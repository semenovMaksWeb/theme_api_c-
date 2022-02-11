using Microsoft.EntityFrameworkCore;
using Npgsql;
namespace api
{
    public class Connection: DbContext
    {
       static public NpgsqlConnection conn = 
            new NpgsqlConnection("Server=127.0.0.1;User Id=postgres;" +
                                 "Password=postgres;Database=system;");      
    }
}
