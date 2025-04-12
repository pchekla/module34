namespace Module34.Contracts.Devices;

/// <summary>
/// Добавляет поддержку нового устройства.
/// </summary>
public class AddDeviceRequest
{
    public required string Name { get; set; }
    public required string Manufacturer { get; set; }
    public required string Model { get; set; }
    public required string SerialNumber { get; set; }
    public int CurrentVolts { get; set; }
    public bool GasUsage { get; set; }
    public required string Location { get; set; }
} 