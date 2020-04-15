using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Donation 
    {
        //donations information:
        //it stores the information of the donor and the amount as the gift
        //Province and Designations are from different tables
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        //Label in the form:
        [Display(Name = "Fund Allocation")]
        //foreign key (representing the one to many relationship: One designation to many Donations)
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }


        //To get the label as Province in the form
        [Display(Name = "Province")]
        //foreign key (representing the one to many relationship: One province to many Donations)
        public int ProvinceId { get; set; }
        public Province Province { get; set; }

        public string City { get; set; }
        public string ZipCode { get; set; }


    }
}