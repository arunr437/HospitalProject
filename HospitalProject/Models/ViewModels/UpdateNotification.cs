using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateNotification
    {
        public virtual Notifications Notifications { get; set; }

        public List<NotificationTypes> types { get; set; }
    }
}