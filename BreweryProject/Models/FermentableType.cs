﻿using System;
using System.Collections.Generic;

namespace BreweryProject.Models
{
    public partial class FermentableType
    {
        public FermentableType()
        {
            Fermentable = new HashSet<Fermentable>();
        }

        public int FermentableTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Fermentable> Fermentable { get; set; }
    }
}
