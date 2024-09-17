using AutoMapper;
using Microsoft.Extensions.Configuration;
using ModelA.DTO.Response;
using Repository.Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class BookingReservationsService : IBookingReservationService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingReservationsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<BookingReservationsResponse> GetAllBookingReservations()
        {
            var listBookingReservations = _unitOfWork.BookingReservationRepository.Get(orderBy: p => p.OrderByDescending(q => q.BookingDate)).ToList();
            var BookingReservationsResponses = _mapper.Map<IEnumerable<BookingReservationsResponse>>(listBookingReservations);
            return BookingReservationsResponses;
        }

        public List<BookingReservationsResponse> GetbookingReservationsByCustomerId(long id)
        {
            try
            {
                var bookingReservations = _unitOfWork.BookingReservationRepository.Get(
                    filter: b => b.CustomerId == id, includeProperties: "BookingDetails");

                if (bookingReservations == null || !bookingReservations.Any())
                {
                    return null;
                }

                var bookingReservationsResponse = _mapper.Map<List<BookingReservationsResponse>>(bookingReservations);
                return bookingReservationsResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
