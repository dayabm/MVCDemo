using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class Employee
    {
        [Required(ErrorMessage= "Please enter employee number")]
        public int EmpNo { get; set; }
        [Required(ErrorMessage = "Please enter employee name"), MaxLength(30)]
        public string EmpName { get; set; }
        public double Salary { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Location { get; set; }
        public bool EmploymentStatus { get; set; }
    }
}