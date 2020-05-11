using System;
using System.Collections.Generic;
using System.Data;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using System.Web.Http;
using ApiAngular.Models;
using System.Data.SqlClient;
//using System.Net;

namespace ApiAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        //Department dep = new Department();
        string sql;
        DataTable dt = new DataTable();
        readonly SqlConnection cnx = new SqlConnection(Program.Cadena);
        SqlDataAdapter adp = new SqlDataAdapter();


        [HttpGet]
        public DataTable Get()
        {

            //List<Department> dep = new List<Department>();

            sql = @"select * from department";
            dt = new DataTable();
            adp = new SqlDataAdapter(sql, cnx);
            adp.Fill(dt); 
            return dt;
        }

        [HttpPost]
        public string Post(Department department)
        {
            string res = "Seccess";
            try
            {
                cnx.Open();
                sql = string.Format("insert into Department values('{0}')", department.DepartmentName);
                adp.InsertCommand = new SqlCommand(sql, cnx);
                adp.InsertCommand.CommandType = CommandType.Text;
                adp.InsertCommand.ExecuteNonQuery();
                cnx.Close();
            }
            catch (Exception)
            {

                res = "Faile to Add";
                throw;               
            }
            return res;
        }


        [HttpPut]
        public void Put(Department department)
        {
            cnx.Open();
            sql = string.Format("update Department set DepartmentName = '{0}' where departmentid = {1}", department.DepartmentName, department.DepartmentID);
            adp.UpdateCommand = new SqlCommand(sql, cnx);
            adp.UpdateCommand.CommandType = CommandType.Text;
            adp.UpdateCommand.ExecuteNonQuery();
            cnx.Close();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cnx.Open();
            sql = string.Format("delete from Department  where departmentid = {0}", id);
            adp.DeleteCommand = new SqlCommand(sql, cnx);
            adp.DeleteCommand.CommandType = CommandType.Text;
            adp.DeleteCommand.ExecuteNonQuery();
            cnx.Close();
        }

    } 
}