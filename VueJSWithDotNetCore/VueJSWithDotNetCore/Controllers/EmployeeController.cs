using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VueJSWithDotNetCore.Models;

namespace VueJSWithDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration,IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var query = @"select EmployeeId, EmployeeName, Department, CONVERT(varchar,DateOfJoining,23) as DateOfJoining, PhotoFileName from employee";

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
        public JsonResult Post(Employee emp)
        {
            var query = @"insert into employee values(@employeeName, @department, @dateOfJoining, @photoFileName)";

            DataTable dt = new DataTable();
            string connStr = _configuration.GetConnectionString("MyConnString");
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@employeeName", emp.EmployeeName);
                    cmd.Parameters.AddWithValue("@department", emp.Department);
                    cmd.Parameters.AddWithValue("@dateOfJoining", emp.DateOfJoining);
                    cmd.Parameters.AddWithValue("@photoFileName", emp.PhotoFileName);
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    conn.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            var query = @"update employee set 
                            employeeName = @employeeName, 
                            department = @department, 
                            dateOfJoining = @dateOfJoining, 
                            photoFileName = @photoFileName
                            where employeeId = @employeeId";

            DataTable dt = new DataTable();
            string connStr = _configuration.GetConnectionString("MyConnString");
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@employeeName", emp.EmployeeName);
                    cmd.Parameters.AddWithValue("@department", emp.Department);
                    cmd.Parameters.AddWithValue("@dateOfJoining", emp.DateOfJoining);
                    cmd.Parameters.AddWithValue("@photoFileName", emp.PhotoFileName);
                    cmd.Parameters.AddWithValue("@employeeId", emp.EmployeeId);
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    conn.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{empId}")]
        public JsonResult Delete(int empId)
        {
            var query = @"delete from employee where employeeId=@employeeId";

            DataTable dt = new DataTable();
            string connStr = _configuration.GetConnectionString("MyConnString");
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@employeeId", empId);
                    reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                    conn.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch(Exception ex)
            {
                return new JsonResult("anonymous");
            }
        }
    }
}
