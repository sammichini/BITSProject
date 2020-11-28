using System;
using System.Collections.Generic;

namespace BreweryProject.Models
{
    public partial class BatchContainer
    {
        public int BatchId { get; set; }
        public int BrewContainerId { get; set; }
        public double? Volume { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual BrewContainer BrewContainer { get; set; }
    }
}
