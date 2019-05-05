using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter1.Models.GraphqlType
{
    public class CustomOrderSellableItemRelationInputType:InputObjectGraphType
    {
        #region Members.
        public CustomOrderSellableItemRelationInputType()
        {
            base.Name = "CustomOrderSellableItemRelationInput";
            Field<NonNullGraphType<IntGraphType>>("quantity");
            Field<NonNullGraphType<StringGraphType>>("barcode");
            Field<NonNullGraphType<IntGraphType>>("orderId");
        }
        #endregion
    }
}
