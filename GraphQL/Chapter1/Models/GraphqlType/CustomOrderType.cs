using Chapter1.Abstract;
using GraphQL.Types;
using System.Collections.Generic;

namespace Chapter1.Models.GraphqlType
{
    public class CustomOrderType:ObjectGraphType<CustomOrder>
    {
        #region Constructors.
        public CustomOrderType(IMockDataSource mockDataSource)
        {
            Field(o => o.Tag);
            Field(o => o.CreateAt);
            /*!!!!请问此处的CustomCustomerType泛型参数类型有何意义？*/
            Field<CustomCustomerType, CustomCustomer>()
                .Name("CustomCustomer")
                .ResolveAsync(fieldContext => 
                {
                    return mockDataSource.GetCustomCustomerByIdAsync(fieldContext.Source.CustomerId);
                });


            Field<CustomOrderSellableItemRelationType, IEnumerable<CustomOrderSellableItemRelation>>()
                .Name("AllOrderSellableItemRelations")
                .ResolveAsync(fieldContext =>
                {
                    return mockDataSource.GetCustomOrderSellableItemRelationByOrderIdAsync(fieldContext.Source.OrderId);
                });
        }
        #endregion
    }
}
