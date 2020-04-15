using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    //Model for Jobs
    public class Job
    {
        [Key]
        public int jobId { get; set; }
        public string name { get; set; }
        public string skill { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public int salary { get; set; } 
    }
}