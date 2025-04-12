namespace HomeApi.Contracts.Models.Rooms
{
    /// <summary>
    /// Запрос на обновление комнаты
    /// </summary>
    public class UpdateRoomRequest
    {
        public string Name { get; set; }
        public int Area { get; set; }
        public bool GasConnected { get; set; }
        public int Voltage { get; set; }
    }
} 