using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class AddDepartment
    {
        public List<Departments> Departments { get; set; }
        public List<FeedbackTypes> Feedbacks { get; set; }
    }
}