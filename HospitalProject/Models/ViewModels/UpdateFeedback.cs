using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateFeedback
    {
        public virtual Feedbacks Feedbacks { get; set; }

        public List<FeedbackTypes> feedbackTypes { get; set; }
    }
}