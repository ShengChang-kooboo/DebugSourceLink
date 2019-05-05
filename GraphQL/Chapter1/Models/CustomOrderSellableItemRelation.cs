using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter1.Models
{
    [Table("CustomOrderSellableItemRelations")]
    public class CustomOrderSellableItemRelation
    {
        #region Members.
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Barcode { get; set; }
        [ForeignKey("Barcode")]
        public virtual SellableItem SellableItem { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual CustomOrder CustomOrder { get; set; }
        #endregion
    }
}
