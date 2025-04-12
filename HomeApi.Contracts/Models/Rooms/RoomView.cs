using System;

namespace HomeApi.Contracts.Models.Rooms
{
    /// <summary>
    /// Информация о комнате
    /// </summary>
    public class RoomView
    {
        public Guid Id { get; set; }
        public DateTime AddDate { get; set; }
        public string Name { get; set; }
        public int Area { get; set; }
        public bool GasConnected { get; set; }
        public int Voltage { get; set; }
    }
} 