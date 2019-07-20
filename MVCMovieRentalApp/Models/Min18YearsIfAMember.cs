using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCMovieRentalApp.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == 0)
                return new ValidationResult("Membership Type is mandatory");
            if (customer.MembershipTypeId == 1)
                return ValidationResult.Success;
            if (customer.DateOfBirth == null)
                return new ValidationResult("Birthdate is required");
            var age = DateTime.Today.Year - customer.DateOfBirth.Year;
            return (age >= 18) ? ValidationResult.Success 
                : new ValidationResult("Age should be greater than 18 years to buy membership");
        }
    }
}