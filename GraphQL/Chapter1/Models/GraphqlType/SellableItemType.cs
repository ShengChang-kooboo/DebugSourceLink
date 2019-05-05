using Chapter1.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter1.Models.GraphqlType
{
    public class SellableItemType:ObjectGraphType<SellableItem>
    {
        #region Constructors.
        public SellableItemType()
        {
            /*为什么GraphQL查询field名称，必须要从小写字母开头？强制驼峰命名吗？！*/
            Field("barcode", i => i.Barcode, nullable:true, type:null);
            Field("title", i => i.Title, nullable:true, type:null);
            Field("sellingPrice", i => i.SellingPrice, nullable:true, type:null);
        }
        #endregion
    }
}
