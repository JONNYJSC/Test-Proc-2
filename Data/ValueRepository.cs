using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Test_Prod_2.Models;

namespace Test_Prod_2.Data
{
    public class ValueRepository
    {
        private readonly string _connectionString;
        //private readonly TestContext _context;
        public ValueRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TestContext");
        }

        public async Task<List<Value>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllValues", sql)) 
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var resp = new List<Value>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            resp.Add(MapToValue(reader));
                        }
                    }
                    return resp;
                }
            }
        }

        private Value MapToValue(SqlDataReader reader)
        {
            return new Value()
            {
                Id = (int)reader["Id"],
                Value1 = (int)reader["Value1"],
                Value2 = reader["Value2"].ToString()
            };
        }

        public async Task Insert(Value value)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CreateValue", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Value1", value.Value1));
                    cmd.Parameters.Add(new SqlParameter("@Value2", value.Value2));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
