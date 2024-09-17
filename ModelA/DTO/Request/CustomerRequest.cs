using ModelA.DTO.validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelA.DTO.Request
{
    public class CustomerRequest
    {
        //public int CustomerID { get; set; }
        [Required(ErrorMessage = "Full name is required.")]
        public string? CustomerFullName { get; set; }

        [RegularExpression("^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$", ErrorMessage = "Fake phone!")]

        public string? Telephone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [Required(ErrorMessage = "Email is required.")]
        public string EmailAddress { get; set; } = null!;

        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [MinAge(18, ErrorMessage = "The age at least 18.")]
        public DateTime CustomerBirthday { get; set; }

        public string? Password { get; set; }
    }
}
