using System;
using System.Collections.Generic;

namespace apptarefa2.Models;

public partial class Incidente
{
    public int Codigo { get; set; }

    public string DescricaoProblema { get; set; } = null!;

    public DateTime DataIncidente { get; set; }

    public string? Solucao { get; set; }

    public string Resolvido { get; set; } = null!;
}
