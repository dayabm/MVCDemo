using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    public class EmployeeController : Controller
    {
        private string connectionString = "Data Source=.;Initial Catalog = NeuTest; Integrated Security=true";
        // GET: Employee
        public ActionResult Index()
        {
            List<Employee> result = new List<Employee>();

            string sqlQuery = String.Format("select * from Employees");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //Create a Command object
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    SqlDataReader dataReader = command.ExecuteReader();

                    Employee employee = null;

                    //load into the result object the returned row from the database
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            employee = new Employee();

                            employee.EmpNo = Convert.ToInt32(dataReader["EmpNo"] ?? 0);
                            employee.EmpName = dataReader["EmpName"].ToString();
                            employee.Salary = Convert.ToDouble(dataReader["Salary"] ?? 0);
                            employee.JoiningDate = Convert.ToDateTime(dataReader["JoiningDate"] ?? DateTime.MinValue).Date;
                            employee.Location = dataReader["Location"].ToString();
                            employee.EmploymentStatus = Convert.ToBoolean(dataReader["EmploymentStatus"] ?? false);

                            result.Add(employee);
                        }
                    }
                }
            }
            return View(result);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Create the SQL Query for inserting an article
                    string sqlQuery = $"Insert into Employees (EmpNo, EmpName ,Salary, JoiningDate, Location, EmploymentStatus)" + " Values(@EmpNo, @EmpName, @Salary, @JoiningDate, @Location, @EmploymentStatus)";

                    //Create and open a connection to SQL Server
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        //Create a Command object
                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                        {
                            command.Parameters.Add("@EmpNo", SqlDbType.Int).Value = emp.EmpNo;
                            command.Parameters.Add("@EmpName", SqlDbType.VarChar).Value = emp.EmpName;
                            command.Parameters.Add("@Salary", SqlDbType.Decimal).Value = emp.Salary;
                            command.Parameters.Add("@JoiningDate", SqlDbType.DateTime).Value = emp.JoiningDate;
                            command.Parameters.Add("@Location", SqlDbType.VarChar).Value = emp.Location;
                            command.Parameters.Add("@EmploymentStatus", SqlDbType.Bit, 50).Value = emp.EmploymentStatus;

                            //Execute the command to SQL Server and return the newly created ID
                            int row = command.ExecuteNonQuery();
                            //ViewBag.HasEmployeeCreated = true;
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
