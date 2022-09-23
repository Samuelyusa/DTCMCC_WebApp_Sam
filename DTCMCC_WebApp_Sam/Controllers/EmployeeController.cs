using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;
using DTCMCC_WebApp_Sam.Models;
using System;

namespace DTCMCC_WebApp_Sam.Controllers
{
    public class EmployeeController : Controller
    {
        SqlConnection sqlConnection;
        string connectionString = "Data Source=DESKTOP-GK9TR5F;Initial Catalog=DTSMCC001;User ID=mccdts1;Password=mccdts;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Read
        public IActionResult Index()
        {
            string query = "select * from Employees";

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            
            List<Employee> Employees = new List<Employee>();

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Employee employee = new Employee();
                            employee.EmployeeId = Convert.ToInt32(sqlDataReader[0]);
                            employee.FirstName = sqlDataReader[1].ToString();
                            Employees.Add(employee);
                        }
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return View(Employees);
        }

        //Read By ID
        //GET
        public IActionResult Details(int Id, string FirstName)
        {
            string query = "select * from employees where EmployeeId = @EmployeeId";

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@EmployeeId";
            sqlParameter.Value = Id;

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add(sqlParameter);

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            ViewData["EmployeeId"] = sqlDataReader[0];
                            ViewData["FirstName"] = sqlDataReader[1];
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return View();
        }


        //GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                try
                {
                    sqlCommand.CommandText = "INSERT INTO Employees " +
                        "(EmployeeID, FirstName) VALUES (@EmployeeId, @FirstName) ";

                    SqlParameter sqlParameterId = new SqlParameter();
                    sqlParameterId.ParameterName = "@EmployeeId";
                    sqlParameterId.Value = employee.EmployeeId;

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@FirstName";
                    sqlParameter.Value = employee.FirstName;

                    sqlCommand.Parameters.Add(sqlParameterId);
                    sqlCommand.Parameters.Add(sqlParameter);

                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //UPDATE GET
        public IActionResult Edit(int Id, string FirstName)
        {
            string query = "select * from employees where EmployeeId = @EmployeeId";

            SqlParameter sqlParameterId = new SqlParameter();
            sqlParameterId.ParameterName = "@EmployeeId";
            sqlParameterId.Value = Id;

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add(sqlParameterId);

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            ViewData["EmployeeId"] = sqlDataReader[0];
                            ViewData["FirstName"] = sqlDataReader[1];
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return View();
        }

        //UPDATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;

                try
                {
                    sqlCommand.CommandText = "update Employees " +
                        "set FirstName = @FirstName where EmployeeId = @EmployeeId";

                    SqlParameter sqlParameterId = new SqlParameter();
                    sqlParameterId.ParameterName = "@EmployeeId";
                    sqlParameterId.Value = employee.EmployeeId;

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@FirstName";
                    sqlParameter.Value = employee.FirstName;

                    sqlCommand.Parameters.Add(sqlParameterId);
                    sqlCommand.Parameters.Add(sqlParameter);

                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //Delete
        public IActionResult Delete(int? Id, bool? saveChangesError = false)
        {
            string query = "select * from employees where EmployeeId = @EmployeeId";

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@EmployeeId";
            sqlParameter.Value = Id;

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add(sqlParameter);

            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            ViewData["EmployeeId"] = sqlDataReader[0];
                            ViewData["FirstName"] = sqlDataReader[1];
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Rows");
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int Id)
        {
            string query = "delete from employees where EmployeeId = @EmployeeId";

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@EmployeeId";
            sqlParameter.Value = Id;

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add(sqlParameter);
            try
            {
                sqlConnection.Open();
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
