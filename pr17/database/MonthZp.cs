using System;
using System.Collections.Generic;

namespace pr17.database;

public partial class MonthZp
{
    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string DadsName { get; set; } = null!;

    public string Zeh { get; set; } = null!;

    public DateOnly FirstDay { get; set; }

    public decimal Zp { get; set; }

    public int Years { get; set; }

    public int Razryad { get; set; }

    public string Profession { get; set; } = null!;
}
