namespace Module34.Contracts.Models.Home;

/// <summary>
/// Настройки дома
/// </summary>
public class HomeOptions
{
    public int FloorAmount { get; set; }
    public string Telephone { get; set; } = string.Empty;
    public string Heating { get; set; } = string.Empty;
    public int CurrentVolts { get; set; }
    public bool GasConnected { get; set; }
    public int Area { get; set; }
    public string Material { get; set; } = string.Empty;
    public Address Address { get; set; } = new Address { Street = string.Empty };
} 