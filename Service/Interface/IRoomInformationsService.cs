using ModelA.DTO.Request;
using ModelA.DTO.Response;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IRoomInformationsService
    {
        IEnumerable<RoomInformationsResponse> GetAllRoom();

        Task<RoomInformationsResponse> CreateRoom(RoomInformationsRequest roomInformationRequest);
        Task<RoomInformationsResponse> UpdateRoom(int id, RoomInformationsRequest roomInformationRequest);
        Task<bool> DeleteRoom(int id);

        Task<RoomInformationsResponse> GetRoomInformationById(int id);

        Task<RoomInformationsResponse> GetRoomInformationByRoomTypeId(int id);

        IEnumerable<RoomInformationsResponse> GetRoomsSortedByPrice(bool ascendingOrder);
    }
}
