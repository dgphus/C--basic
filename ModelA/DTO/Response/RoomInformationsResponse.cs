using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelA.DTO.Response
{
    public class RoomInformationsResponse
    {
        public string RoomNumber { get; set; }
        public string RoomDetailDescription { get; set; }
        public int? RoomMaxCapacity { get; set; }
        public int RoomTypeID { get; set; }
        public byte? RoomStatus { get; set; }
        public decimal? RoomPricePerDay { get; set; }

        public RoomTypeResponse RoomType { get; set; }

        public string? ErrorMessage { get; set; }
        //public RoomType RoomTypes { get; set; }

    }
}
