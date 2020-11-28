using System;
using System.Collections.Generic;

namespace BreweryProject.Models
{
    public partial class IngredientInventoryAddition
    {
        public int IngredientInventoryAdditionId { get; set; }
        public int IngredientId { get; set; }
        public int SupplierId { get; set; }
        public double Quantity { get; set; }
        public double? QuantityRemaining { get; set; }
        public decimal UnitCost { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
