using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter1.Models.GraphqlType
{
    public class CustomCustomerInputType:InputObjectGraphType
    {
        #region Constructors.
        public CustomCustomerInputType()
        {
            base.Name = "CustomCustomerInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("billingAddress");
        }
        #endregion
    }
}
