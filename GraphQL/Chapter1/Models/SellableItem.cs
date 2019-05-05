using System.Collections;
using System.Collections.Generic;

namespace Chapter1.Models
{
    public class SellableItem
    {
        public string Barcode { get; set; }
        public string Title { get; set; }
        public decimal SellingPrice { get; set; }
        /*配置EF Code First中的ForeignKey关联  https://www.cnblogs.com/lwqlun/p/9955901.html*/
        public virtual ICollection<CustomOrderSellableItemRelation> CustomOrderSellableItemRelations { get; set; }
    }
}
