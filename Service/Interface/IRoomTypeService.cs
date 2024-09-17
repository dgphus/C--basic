using ModelA.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface;

public interface IRoomTypeService
{
    IEnumerable<RoomTypeResponse> GetAllRoomType();
    Task<RoomTypeResponse> GetRoomTypeById(int id);
}
