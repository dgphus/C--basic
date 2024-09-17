using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface IUnitOfWork
    {
        public IGenericRepository<BookingDetail> BookingDetailRepository { get;  }
        public IGenericRepository<BookingReservation> BookingReservationRepository { get; }
        public IGenericRepository<Customer> CustomerRepository { get; }
        public IGenericRepository<RoomInformation> RoomInformationRepository { get; }
        public IGenericRepository<RoomType> RoomTypeRepository { get; }
        void Save();
        void Dispose();
    }
}
