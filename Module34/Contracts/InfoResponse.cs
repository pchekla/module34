namespace Module34.Contracts;

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

public class AddressInfo
{
    public int House { get; set; }
    public int Building { get; set; }
    public required string Street { get; set; }
}
