using System;
using System.Threading.Tasks;
using AutoMapper;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository _repository;
        private IMapper _mapper;
        
        public RoomsController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Получение списка комнат
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _repository.GetRooms();
            
            var response = new GetRoomsResponse
            {
                RoomAmount = rooms.Length,
                Rooms = _mapper.Map<Room[], RoomView[]>(rooms)
            };
            
            return StatusCode(200, response);
        }
        
        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost] 
        [Route("")] 
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _repository.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repository.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }
            
            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }
        
        /// <summary>
        /// Обновление комнаты
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRoomRequest request)
        {
            var room = await _repository.GetRoomById(id);
            if (room == null)
                return StatusCode(404, $"Ошибка: Комната с идентификатором {id} не существует.");
            
            // Проверяем, не пытается ли пользователь обновить имя комнаты на существующее
            if (room.Name != request.Name)
            {
                var existingRoom = await _repository.GetRoomByName(request.Name);
                if (existingRoom != null)
                    return StatusCode(409, $"Ошибка: Комната с именем {request.Name} уже существует.");
            }
            
            // Сохраняем дату добавления и идентификатор
            var roomAddDate = room.AddDate;
            var roomId = room.Id;
            
            // Обновляем свойства комнаты из запроса
            _mapper.Map(request, room);
            
            // Восстанавливаем неизменяемые свойства
            room.AddDate = roomAddDate;
            room.Id = roomId;
            
            await _repository.UpdateRoom(room);
            
            return StatusCode(200, $"Комната {request.Name} успешно обновлена!");
        }
        
        /// <summary>
        /// Удаление комнаты
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var room = await _repository.GetRoomById(id);
            if (room == null)
                return StatusCode(404, $"Ошибка: Комната с идентификатором {id} не существует.");
            
            await _repository.DeleteRoom(room);
            
            return StatusCode(200, $"Комната {room.Name} успешно удалена!");
        }
    }
}