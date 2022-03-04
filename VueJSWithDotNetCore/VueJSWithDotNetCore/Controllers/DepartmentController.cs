using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VueJSWithDotNetCore.Models;

namespace VueJSWithDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var query = @"select * from department";

            DataTable dt = new DataTable();
            string connStr = _configuration.GetConnectionString("MyConnString");
            SqlDataReader reader;
            using(SqlConnection conn=new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    conn.Close();
                }
            }
            return new JsonResult(dt);
        }

        [HttpPost]
        public JsonResult Post(Department dep)
        {
            var query = @"insert into department values(@departmentName)";

            DataTable dt = new DataTable();
            string connStr = _configuration.GetConnectionString("MyConnString");
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@departmentName", dep.DepartmentName);
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    conn.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            var query = @"update department set department = @departmentName where departmentId=@departmentId";

            DataTable dt = new DataTable();
            string connStr = _configuration.GetConnectionString("MyConnString");
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@departmentName", dep.DepartmentName);
                    cmd.Parameters.AddWithValue("@departmentId", dep.DepartmentId);
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    conn.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{depId}")]
        public JsonResult Delete(int depId)
        {
            var query = @"delete from department where departmentId=@departmentId";

            DataTable dt = new DataTable();
            string connStr = _configuration.GetConnectionString("MyConnString");
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@departmentId", depId);
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    conn.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
    }
}
