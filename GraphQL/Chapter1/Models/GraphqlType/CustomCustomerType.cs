using Chapter1.Abstract;
using GraphQL.Types;
using System.Collections.Generic;

namespace Chapter1.Models.GraphqlType
{
    public class CustomCustomerType:ObjectGraphType<CustomCustomer>
    {
        #region Members.
        public CustomCustomerType(IMockDataSource mockDataSource)
        {
            Field(c => c.Name);
            Field(c => c.BillingAddress);
            /*!!!!请问此处的CustomOrderType泛型参数类型有何意义？*/
            Field<ListGraphType<CustomOrderType>, IEnumerable<CustomOrder>>()
                .Name("CustomOrders")
                .ResolveAsync(fieldContext =>
                {
                    return mockDataSource.GetOrdersByCustomCustomerIdAsync(fieldContext.Source.CustomerId);
                });
        }
        #endregion
    }
}
