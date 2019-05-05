using System;
using System.Collections;
using System.Collections.Generic;

namespace Chapter1.Models
{
    public class CustomOrder
    {
        #region Members.
        public int OrderId { get; set; }
        public string Tag { get; set; }
        public DateTime CreateAt { get; set; }
        public CustomCustomer Customer { get; set; }
        public int CustomerId { get; set; }
        /*配置EF Code First中的ForeignKey关联  https://www.cnblogs.com/lwqlun/p/9955901.html*/
        public virtual ICollection<CustomOrderSellableItemRelation> CustomOrderSellableItemRelations { get; set; }
        #endregion
    }
}
