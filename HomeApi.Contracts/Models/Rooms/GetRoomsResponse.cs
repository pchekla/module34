namespace HomeApi.Contracts.Models.Rooms
{
    /// <summary>
    /// Ответ на запрос списка комнат
    /// </summary>
    public class GetRoomsResponse
    {
        public int RoomAmount { get; set; }
        public RoomView[] Rooms { get; set; }
    }
}