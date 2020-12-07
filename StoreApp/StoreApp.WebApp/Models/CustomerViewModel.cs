using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Display(Name = " First Name")]
        [Required, RegularExpression("[A-Z].*", ErrorMessage ="Please capitalize and use only a-z characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required, RegularExpression("[A-Z].*",ErrorMessage = "Please capitalize and use only a-z characters")]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Phone Number")]
        [Required, RegularExpression("^\\(?[\\d]{3}\\)?[\\s-]?[\\d]{3}[\\s-]?[\\d]{4}$", ErrorMessage ="Input must match phone number format '(123)456-7890'")]
        public string PhoneNumber { get; set; }

    }
}
