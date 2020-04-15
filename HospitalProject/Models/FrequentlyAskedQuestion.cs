using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    //Model for Fequently Asked Questions
    public class FrequentlyAskedQuestion
    {
        [Key]
        public int id { get; set; }
        public string questions { get; set; }
        public string answers { get; set; }
        public string category { get; set; }

    }
}