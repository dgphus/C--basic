using Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelA.DTO.Response
{
    public class BookingReservationsResponse
    {
        public int BookingReservationId { get; set; }

        public DateOnly? BookingDate { get; set; }

        public decimal? TotalPrice { get; set; }

        public int CustomerId { get; set; }

        public byte? BookingStatus { get; set; }
        public virtual ICollection<BookingDetailResponse> BookingDetails { get; set; } = new List<BookingDetailResponse>();
    }
}
