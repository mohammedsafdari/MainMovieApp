using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCMovieRentalApp.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Customer Name is mandatory")]
        [StringLength(50)]
        [Display(Name="Customer Name")]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        [Min18YearsIfAMember]
        public DateTime DateOfBirth { get; set; }

        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }
    }
}