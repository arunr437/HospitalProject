using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class ShowFeedbackType
    {
        public FeedbackTypes type { get; set; }
        public List<Departments> departments { get; set; }
        public List<Feedbacks> feedbacks { get; set; }
    }
}