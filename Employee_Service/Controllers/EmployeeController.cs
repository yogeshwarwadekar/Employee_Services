using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Employee_Service.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet,Route("showEmployee")]
        public IEnumerable<EmployeeDetail> showEmployee()
        {
            using (Employee_DatabaseEntities entities = new Employee_DatabaseEntities())
            {
                return entities.EmployeeDetails.ToList();
            }
        }
        
                
        [HttpGet, Route("employeeDetail")]
        public Employee employeeDetail([FromUri]int id)
        {
            using ( Employee_DatabaseEntities entities = new  Employee_DatabaseEntities())
            {
                return entities.Employees.FirstOrDefault(e => e.Emp_ID == id);
            }
        }


        [HttpPost, Route("addEmployee")]
        public void addEmployee([FromBody] Employee employee)
        {
            using ( Employee_DatabaseEntities entities = new  Employee_DatabaseEntities())
            {
                try
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error" + ex);
                }
            }
        }
        

        [HttpDelete, Route("deleteEmployee")]
        public void deleteEmployee(string employeeIds)
        {
            using ( Employee_DatabaseEntities entities = new  Employee_DatabaseEntities())
            {
                try
                {
                    employeeIds = employeeIds.Substring(0,employeeIds.Length - 1);
                    int[] IDs = Array.ConvertAll(employeeIds.Split(','), int.Parse);
                    foreach (int i in IDs)
                    {
                        entities.Employees.Remove(entities.Employees.FirstOrDefault(e => e.Emp_ID == i));
                        entities.SaveChanges();
                    }                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("delete employee : " + ex);
                }
            }
        }


        [HttpPut, Route("updateEmployee")]
        public void updateEmployee([FromUri]int empID, [FromBody]Employee employee)
        {
            using ( Employee_DatabaseEntities entities = new  Employee_DatabaseEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.Emp_ID == empID);

                entity.Emp_First_Name = employee.Emp_First_Name;
                entity.Emp_Last_Name = employee.Emp_Last_Name;
                entity.Emp_Email_ID = employee.Emp_Email_ID;
                entity.Emp_Mobile_Number = employee.Emp_Mobile_Number;
                entity.Emp_State_ID = Convert.ToInt32(employee.Emp_State_ID);
                entity.Emp_City_ID = Convert.ToInt32(employee.Emp_City_ID);
                entity.Emp_Skill_ID = Convert.ToInt32(employee.Emp_Skill_ID);
                entity.Emp_DOB = employee.Emp_DOB;
                entity.Emp_DOJ = employee.Emp_DOJ;
                entity.Emp_Dept_ID = Convert.ToInt32(employee.Emp_Dept_ID);
                entity.Emp_Rating = employee.Emp_Rating;

                entities.SaveChanges();
            }
        }


        [HttpGet, Route("showState")]
        public IEnumerable<State> showState()
        {
            using (Employee_DatabaseEntities entities = new Employee_DatabaseEntities())
            {
                return entities.States.ToList();
            }
        }


        [HttpGet, Route("showCity")]
        public IEnumerable<City> showCity(int stateValue)
        {
            using (Employee_DatabaseEntities entities = new Employee_DatabaseEntities())
            {
                var cities = from city_List in entities.Cities
                             where city_List.State_ID == stateValue
                             select city_List;

                return cities.ToList();
            }
        }


        [HttpGet, Route("showDepartment")]
        public IEnumerable<Department> showDepartment()
        {
            using (Employee_DatabaseEntities entities = new Employee_DatabaseEntities())
            {
                return entities.Departments.ToList();
            }
        }


        [HttpGet, Route("showSkill")]
        public IEnumerable<Skill> showSkill()
        {
            using (Employee_DatabaseEntities entities = new Employee_DatabaseEntities())
            {
                return entities.Skills.ToList();
            }
        }

        [HttpGet, Route("showRating")]
        public IEnumerable<Rating> showRating()
        {
            using (Employee_DatabaseEntities entities = new Employee_DatabaseEntities())
            {
                return entities.Ratings.ToList();
            }
        }
    }
}
