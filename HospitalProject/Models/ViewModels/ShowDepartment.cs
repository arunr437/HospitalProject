using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class ShowDepartment
    {
        public virtual Departments Department { get; set; }

        public List<FeedbackTypes> types { get; set; }
    }
}