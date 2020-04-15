using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class NewDonationViewModel 
    {
        //A list of provinces to be used in the donations class
        public List<Province> Provinces { get; set; }
        //A list of Designations for a donation
        public List<Designation> Designations { get; set; }
        //All the properties of the donation (not one by one FirstName, ....)
        public Donation Donation { get; set; }
  
    }
}