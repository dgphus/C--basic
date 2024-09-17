using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelA.DTO.Request;
using ModelA.DTO.Response;
using Repository.Entity;
using Repository.Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class RoomInformationsService : IRoomInformationsService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomInformationsService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<RoomInformationsResponse> CreateRoom(RoomInformationsRequest roomInformationRequest)
        {
            try
            {
                var roomTypeExists = _unitOfWork.RoomTypeRepository.Exists(rt => rt.RoomTypeId == roomInformationRequest.RoomTypeID);

                if (!roomTypeExists)
                {
                    return new RoomInformationsResponse
                    {
                        ErrorMessage = "Room type not found."
                    };
                }

                var roomNumberExists = _unitOfWork.RoomInformationRepository.Exists(r => r.RoomNumber == roomInformationRequest.RoomNumber);
                if (roomNumberExists)
                {
                    return new RoomInformationsResponse
                    {
                        ErrorMessage = "Room number already exists."
                    };
                }

                var room = _mapper.Map<RoomInformation>(roomInformationRequest);

                room.RoomStatus = 1;

                _unitOfWork.RoomInformationRepository.Insert(room);
                _unitOfWork.Save();

                var roomResponse = _mapper.Map<RoomInformationsResponse>(room);
                return roomResponse;
            }
            catch (Exception ex)
            {
                return new RoomInformationsResponse
                {
                    ErrorMessage = $"An error occurred while creating the room: {ex.Message}"
                };
            }
        }


        public async Task<bool> DeleteRoom(int id)
        {
            try
            {
                var room = _unitOfWork.RoomInformationRepository.GetByID(id);
                if (room == null || room.RoomStatus == 0)
                {
                    throw new Exception("room not found.");
                }

                room.RoomStatus = 0;
                _unitOfWork.RoomInformationRepository.Update(room);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<RoomInformationsResponse> GetAllRoom()
        {
            var listRoom = _unitOfWork.RoomInformationRepository.Get(
                filter: s => s.RoomStatus == 1, includeProperties: "RoomType").ToList();
            var roomResponses = _mapper.Map<IEnumerable<RoomInformationsResponse>>(listRoom);
            return roomResponses;
        }

        public async Task<RoomInformationsResponse> UpdateRoom(int id, RoomInformationsRequest roomInformationRequest)
        {
            try
            {
                var existingRoom = _unitOfWork.RoomInformationRepository.GetByID(id);
                if (existingRoom == null || existingRoom.RoomStatus == 0)
                {
                    return new RoomInformationsResponse
                    {
                        ErrorMessage = "Room not found."
                    };
                }

                var roomTypeExists = _unitOfWork.RoomTypeRepository.Exists(rt => rt.RoomTypeId == roomInformationRequest.RoomTypeID);
                if (!roomTypeExists)
                {
                    return new RoomInformationsResponse
                    {
                        ErrorMessage = "Room type not found."
                    };
                }

                var roomNumberExists = _unitOfWork.RoomInformationRepository.Exists(r => r.RoomNumber == roomInformationRequest.RoomNumber && r.RoomId != id);
                if (roomNumberExists)
                {
                    return new RoomInformationsResponse
                    {
                        ErrorMessage = "Room number already exists."
                    };
                }

                _mapper.Map(roomInformationRequest, existingRoom);

                _unitOfWork.RoomInformationRepository.Update(existingRoom);
                _unitOfWork.Save();

                var roomResponse = _mapper.Map<RoomInformationsResponse>(existingRoom);
                return roomResponse;
            }
            catch (Exception ex)
            {
                return new RoomInformationsResponse
                {
                    ErrorMessage = $"An error occurred while updating the room: {ex.Message}"
                };
            }
        }

        public async Task<RoomInformationsResponse> GetRoomInformationById(int id)
        {
            try
            {
                var room = _unitOfWork.RoomInformationRepository.Get(
                    filter: a => a.RoomId == id, includeProperties: "RoomType").FirstOrDefault();

                if (room == null)
                {
                    throw new Exception("room not found");
                }

                var roomResponse = _mapper.Map<RoomInformationsResponse>(room);
                return roomResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RoomInformationsResponse> GetRoomInformationByRoomTypeId(int id)
        {
            try
            {
                var room = _unitOfWork.RoomInformationRepository.Get(
                    filter: a => a.RoomTypeId == id, includeProperties: "RoomType").FirstOrDefault();

                if (room == null)
                {
                    throw new Exception("room not found");
                }

                var roomResponse = _mapper.Map<RoomInformationsResponse>(room);
                return roomResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<RoomInformationsResponse> GetRoomsSortedByPrice(bool ascendingOrder)
        {
            Expression<Func<RoomInformation, object>> orderByExpression = room => room.RoomPricePerDay;

            Func<IQueryable<RoomInformation>, IOrderedQueryable<RoomInformation>> orderBy = null;
            if (ascendingOrder)
            {
                orderBy = q => q.OrderBy(orderByExpression);
            }
            else
            {
                orderBy = q => q.OrderByDescending(orderByExpression);
            }

            var sort =  _unitOfWork.RoomInformationRepository.Get(orderBy: orderBy, includeProperties: "RoomType");

            var sortResponses = _mapper.Map<IEnumerable<RoomInformationsResponse>>(sort);
            return sortResponses;
        }


    }
}
