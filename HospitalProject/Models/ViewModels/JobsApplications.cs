using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    //This ViewModel is used to show the Job Name for each Application in the Applications List view. 
    public class JobsApplications
    {
        public List<Job> jobList { get; set; }
        public List<Application> applicationList { get; set; }
    }
}