namespace Module34.Models.Home;

/// <summary>
/// Адрес дома
/// </summary>
public class Address
   {
       public int House { get; set; }
       public int Building { get; set; }
       public required string Street { get; set; }
   }