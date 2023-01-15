using System;
using System.Collections.Generic;

namespace RotasWebAPI.Models;

public partial class RotasDTO
{
    public int Id { get; set; }

    public string? Origem { get; set; }

    public string? Destino { get; set; }

    public decimal? Valor { get; set; }
}
