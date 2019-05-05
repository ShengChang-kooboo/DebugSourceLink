using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter1.Models.GraphqlType
{
    /// <summary>
    /// 定义输入类型
    /// </summary>
    public class SellableItemInputType:InputObjectGraphType
    {
        #region Members.

        #endregion

        #region Constructors.
        public SellableItemInputType()
        {
            Name = "SellableItemInput";
            Field<NonNullGraphType<StringGraphType>>("barcode");
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<NonNullGraphType<DecimalGraphType>>("sellingPrice");
        }
        #endregion

        #region Methods.

        #endregion
    }
}
