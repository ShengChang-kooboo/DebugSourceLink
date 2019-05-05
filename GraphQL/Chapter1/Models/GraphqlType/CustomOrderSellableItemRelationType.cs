using Chapter1.Abstract;
using GraphQL.Types;

namespace Chapter1.Models.GraphqlType
{
    public class CustomOrderSellableItemRelationType:ObjectGraphType<CustomOrderSellableItemRelation>
    {
        #region Constructors.
        public CustomOrderSellableItemRelationType(IMockDataSource mockDataSource)
        {
            Field(r => r.Barcode);
            Field<SellableItemType, SellableItem>()
                .Name("SellableItem")
                .Resolve(fieldContext =>
                {
                    return mockDataSource.GetSellableItemByBarcode(fieldContext.Source.Barcode);
                });

            Field(r => r.Quantity);
            Field(r => r.OrderId);
            Field<CustomOrderType, CustomOrder>()
                .Name("CustomOrder")
                .ResolveAsync(fieldContext =>
                {
                    return mockDataSource.GetCustomOrderByIdAsync(fieldContext.Source.OrderId);
                });
        }
        #endregion
    }
}
