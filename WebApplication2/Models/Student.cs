using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    /// <summary>
    /// This calss kepps the studesnts details.
    /// </summary>
    public class Student
    {   
        public Student() { }
        [Required]
        [Display(Name = "ID")]
        public string ID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
    }
}