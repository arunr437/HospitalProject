using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    //Model for Applications. This class has JobId as the Foreign Key
    public class Application
    {
        [Key]
        public int applicationId { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailId { get; set; }
        public string coverLetter { get; set; }
        public string resume { get; set; }

        public int JobId { get; set; }
        [ForeignKey("JobId")]
        public Job job { get; set; }
    }
}