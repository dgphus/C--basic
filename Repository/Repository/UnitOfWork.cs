using Repository.Entity;

namespace Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private FuminiHotelManagementContext _context = new FuminiHotelManagementContext();
        private IGenericRepository<BookingDetail> _bookingDetailRepository;
        private IGenericRepository<BookingReservation> _bookingReservationRepository;
        private IGenericRepository<Customer> _customerRepository;
        private IGenericRepository<RoomInformation> _roomInformationRepository;
        private IGenericRepository<RoomType> _roomTypeRepository;


        public UnitOfWork()
        {
        }

        public IGenericRepository<BookingDetail> BookingDetailRepository
        {
            get
            {

                if (_bookingDetailRepository == null)
                {
                    _bookingDetailRepository = new GenericRepository<BookingDetail>(_context);
                }
                return _bookingDetailRepository;
            }
        }
        public IGenericRepository<BookingReservation> BookingReservationRepository
        {
            get
            {

                if (_bookingReservationRepository == null)
                {
                    _bookingReservationRepository = new GenericRepository<BookingReservation>(_context);
                }
                return _bookingReservationRepository;
            }
        }
        public IGenericRepository<Customer> CustomerRepository
        {
            get
            {

                if (_customerRepository == null)
                {
                    _customerRepository = new GenericRepository<Customer>(_context);
                }
                return _customerRepository;
            }
        }
        public IGenericRepository<RoomInformation> RoomInformationRepository
        {
            get
            {

                if (_roomInformationRepository == null)
                {
                    _roomInformationRepository = new GenericRepository<RoomInformation>(_context);
                }
                return _roomInformationRepository;
            }
        }
        public IGenericRepository<RoomType> RoomTypeRepository
        {
            get
            {

                if (_roomTypeRepository == null)
                {
                    _roomTypeRepository = new GenericRepository<RoomType>(_context);
                }
                return _roomTypeRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}