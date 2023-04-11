﻿using System;
using System.Collections.Generic;

namespace NeoAPT.NeoModels;

public partial class TipIncen
{
    public int IdTipIncen { get; set; }

    public string? Tinombre { get; set; }

    public string? Tidesc { get; set; }

    public bool? Tiesta { get; set; }

    public virtual ICollection<Resuman> Resumen { get; } = new List<Resuman>();
}
