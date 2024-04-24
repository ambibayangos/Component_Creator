﻿using System;
using System.Collections.Generic;

namespace Altium_Component_Creator.Models;

public partial class ResistorTable
{
    public string PartNumber { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string Package { get; set; } = null!;

    public double? Power { get; set; }

    public double? Voltage { get; set; }

    public double? Tolerance { get; set; }

    public string Description { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public string FoorprintRef { get; set; } = null!;

    public string FootprintPath { get; set; } = null!;

    public string LibraryRef { get; set; } = null!;

    public string LibraryPath { get; set; } = null!;

    public string? ComponentLink1Description { get; set; }

    public string? ComponentLink1Url { get; set; }
}
