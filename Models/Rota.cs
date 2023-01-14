using System;
using System.Collections.Generic;

namespace RepositoryGeneric.Models;

public partial class Rota
{
    public string? Origem { get; set; }

    public string? Destino { get; set; }

    public decimal? Valor { get; set; }
}
