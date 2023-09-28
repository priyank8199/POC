using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class Student
    {

        [Key]
        public int Studid { get; set; }

        [Display(Name = "Student Name")]
        public string Studname { get; set; }

        [Display(Name = "Gender")]
        public char Gender { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

       
        [Display(Name = "Subjects")]
        public string Subjects { get; set; }

        [Display(Name = "Fees")]
            public double Fees { get; set; }
        
    }
}
