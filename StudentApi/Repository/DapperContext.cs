using Microsoft.Extensions.Configuration; 
using System.Data;
using System.Data.SqlClient; 

namespace StudentApi.Repository
{
  public class DapperContext
  {
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public DapperContext(IConfiguration configuration)
    {
      _configuration = configuration;
      _connectionString = _configuration.GetConnectionString("DatabaseConnection");
    }
    public IDbConnection InitialiseConnection()
        => new SqlConnection(_connectionString);
  }
}
