using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class NotificationTypes
    {
        /*
         * Notification types can have a 
         * id
         * name
         */
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}