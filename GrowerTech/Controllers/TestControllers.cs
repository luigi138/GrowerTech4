using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System;
using System.Data;

namespace YourNamespace.Controllers
{
    public class TestController : Controller
    {
        private readonly string _connectionString;

        public TestController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleFIAP") ?? throw new ArgumentNullException("OracleFIAP connection string not found.");
        }

        public IActionResult Index()
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new OracleCommand("SELECT * FROM AGRICULTORES", connection))
                    using (var reader = command.ExecuteReader())
                    {
                        var result = new List<string>();
                        while (reader.Read())
                        {
                            result.Add(reader["NOME"]?.ToString() ?? string.Empty);
                        }

                        return Ok(result);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
