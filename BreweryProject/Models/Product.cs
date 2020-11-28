﻿using System;
using System.Collections.Generic;

namespace BreweryProject.Models
{
    public partial class Product
    {
        public int BatchId { get; set; }
        public int ProductContainerSizeId { get; set; }
        public int QuantityRacked { get; set; }
        public int QuantityRemaining { get; set; }
        public decimal? IngredientCost { get; set; }
        public decimal? SuggestedPrice { get; set; }

        public virtual Batch Batch { get; set; }
        public virtual ProductContainerSize ProductContainerSize { get; set; }
    }
}
