using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    //This ViewModel is used to show the list of Applications in the Show Job page
    public class JobApplications
    {
        public Job job { get; set; }
        public List<Application> applicantionList { get; set; }
    }
}