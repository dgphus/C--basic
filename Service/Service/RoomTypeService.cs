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

namespace Service.Service;

public class RoomTypeService : IRoomTypeService
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RoomTypeService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _configuration = configuration;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public IEnumerable<RoomTypeResponse> GetAllRoomType()
    {
        var listRoomType = _unitOfWork.RoomTypeRepository.Get().ToList();
        var roomResponses = _mapper.Map<IEnumerable<RoomTypeResponse>>(listRoomType);
        return roomResponses;
    }

    public async Task<RoomTypeResponse> GetRoomTypeById(int id)
    {
        try
        {
            var room = _unitOfWork.RoomTypeRepository.Get(
                filter: a => a.RoomTypeId == id).FirstOrDefault();

            if (room == null)
            {
                throw new Exception("room not found");
            }

            var roomResponse = _mapper.Map<RoomTypeResponse>(room);
            return roomResponse;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
