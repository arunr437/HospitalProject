using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class FeedbackTypes
    {
        /*Notification types have 
         * id
         * name
         * and can have a department assoiated to them
         * 
         * 
         */
        [Key]
        public int typeId { get; set; }

        public string name { get; set; }

        //representing many in the many feedbacks to many departments
        public ICollection<Departments> Departments { get; set; }
    }
}