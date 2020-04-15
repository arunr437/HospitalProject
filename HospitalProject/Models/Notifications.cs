using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class Notifications
    {
        /*
         * Notificaitions can have a 
         * id
         * headlin
         * content
         * active (display on main page or not)
         * notification type 
         * 
         */

        [Key]
        public int id { get; set; }
        public string headline { get; set; }
        public string content { get; set; }

        public int active { get; set; }
        public int typeId { get; set; }
        [ForeignKey("typeId")]
        public virtual NotificationTypes Type { get; set; }
    }
}