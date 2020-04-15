using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class Feedbacks
    {
        /*
         * Feedbacks can have a 
         * id
         * firstname
         * lastname
         * email
         * feedback
         * feedback type 
         * 
         */
        [Key]
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Feedback { get; set; }
        
        //representing the one to many
        public int typeId { get; set; }
        [ForeignKey("typeId")]
        public virtual FeedbackTypes FeedbackType { get; set; }
    }
}