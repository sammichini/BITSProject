using System;
using System.Collections.Generic;

namespace BreweryProject.Models
{
    public partial class HopType
    {
        public HopType()
        {
            Hop = new HashSet<Hop>();
        }

        public int HopTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Hop> Hop { get; set; }
    }
}
