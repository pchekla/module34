namespace Module34.Contracts.Models.Home;

/// <summary>
/// Информация о вашем доме (модель ответа)
/// </summary>
public class InfoResponse
{
    public int FloorAmount { get; set; }
    public required string Telephone { get; set; }
    public required string Heating { get; set; }
    public int CurrentVolts { get; set; }
    public bool GasConnected { get; set; }
    public int Area { get; set; }
    public required string Material { get; set; }
    public required AddressInfo AddressInfo { get; set; }
} 