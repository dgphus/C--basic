using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelA.DTO.Request
{
    public class RoomInformationsRequest
    {
        [Required(ErrorMessage = "Room number is required")]
        public string RoomNumber { get; set; }
        public string RoomDetailDescription { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Room max capacity must be greater than 0")]
        public int? RoomMaxCapacity { get; set; }
        public int RoomTypeID { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Room price per day must be greater than or equal to 0")]
        public decimal? RoomPricePerDay { get; set; }
    }
}
