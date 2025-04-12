using System;
using System.Collections.Generic;

namespace HomeApi.Models.DbFirst;

public partial class NetworkUser
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Login { get; set; }
}
