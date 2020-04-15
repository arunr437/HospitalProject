using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class Departments
    {
        /*
         * Departments have 
         * id 
         * name (Ex. HR)
         * email (for the department)
         * 
         * 
         */
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }

        //representing the many in many departments to many feedback types
        public ICollection<FeedbackTypes> types { get; set; }
    }
}