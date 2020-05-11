using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAngular.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private Conexion.MyDBContext db;
        public EmployeeController(Conexion.MyDBContext context)
        {
            db = context; //Inicializando mi variable.
        }

        [HttpGet]
        public IEnumerable<Employee> Employee()
        {
            List<Employee> employees = (from d in db.Employee
                                        select new Employee
                                        {
                                            EmployeeID = d.EmployeeID,
                                            EmployeeName = d.EmployeeName,
                                            Department = d.Department,
                                            Email = d.Email,
                                            DOJ = d.DOJ

                                        }).ToList();
            return employees;
        }

        [HttpGet("{id}")]
        public Employee detail(long id)
        {
            var employee = db.Employee.Find(id);
            return employee;
        }


        [HttpPost]
        public string Post(Employee emp)
        {
            string message = "Error adding new employee";
            if (ModelState.IsValid)
            {
                db.Employee.Add(emp);
                db.SaveChanges();
                message = "Employee Added successfully!";
            }
          
          

            return message;
        }


        [HttpPut]
        public string Put(Employee emp)
        {
            string message = "Error updating an employee";

            if (ModelState.IsValid)
            {
                //var employee = db.Employee.Find(emp.EmployeeID);

                db.Employee.Update(emp);
                db.SaveChanges();
                message = "Employee updated successfully!";
            }



            return message;
        }

        [HttpDelete("{id}")]
        public string Delete(long id)
        {
            string message = "Error deleting an employee";

            if (ModelState.IsValid)
            {
                var employee = db.Employee.Find(id);

                db.Employee.Remove(employee);
                db.SaveChanges();
                message = "Employee deleted successfully!";
            }



            return message;
        }

        //public IEnumerable<Employee> Employee(long? id)
        //{
        //    List<Employee> employee = new List<Employee>
        //    {
        //        db.Employee.FirstOrDefault(d => d.EmployeeID == id)
        //    };
        //    //.FirstOrDefaultAsync(m => m.Id == id);


        //    return employee;


        //}

    }
}