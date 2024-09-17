using ModelA.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBookingReportService
    {
        IEnumerable<BookingReport> GetBookingReport(DateTime startDate, DateTime endDate);
    }
}


