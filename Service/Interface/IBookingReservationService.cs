using ModelA.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBookingReservationService
    {
        IEnumerable<BookingReservationsResponse> GetAllBookingReservations();

        List<BookingReservationsResponse> GetbookingReservationsByCustomerId(long id);
    }
}
