using Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelA.DTO.Response
{
    public class CustomerResponse
    {
        public int CustomerId { get; set; }

        public string? CustomerFullName { get; set; }

        public string? Telephone { get; set; }

        public string EmailAddress { get; set; } = null!;

        public DateOnly? CustomerBirthday { get; set; }

        public byte? CustomerStatus { get; set; }

        public virtual ICollection<BookingReservationsResponse> BookingReservations { get; set; } = new List<BookingReservationsResponse>();
    }
}
