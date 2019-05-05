using System.Collections.Generic;

namespace Chapter1.Models
{
    public class CustomCustomer
    {
        #region Members.
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string BillingAddress { get; set; }
        public IEnumerable<CustomOrder> Orders { get; set; }
        #endregion
    }
}
