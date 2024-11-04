using Microsoft.AspNetCore.Mvc;
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
            _connectionString = configuration.GetConnectionString("OracleFIAP");
        }

        
        public IActionResult Index()
        {
            try
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    connection.Open();
                    var command = new OracleCommand("SELECT * FROM AGRICULTORES", connection);
                    var reader = command.ExecuteReader();

                    var result = new List<string>();
                    while (reader.Read())
                    {
                        result.Add(reader["NOME"].ToString());
                    }

                    return Ok(result);  
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
    }
}