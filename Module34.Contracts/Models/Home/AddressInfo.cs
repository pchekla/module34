namespace Module34.Contracts.Models.Home;

/// <summary>
/// Информация об адресе дома
/// </summary>
public class AddressInfo
{
    public int House { get; set; }
    public int Building { get; set; }
    public required string Street { get; set; }
} 