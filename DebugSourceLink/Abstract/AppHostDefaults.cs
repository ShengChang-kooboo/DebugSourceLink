using System.Collections.Generic;
using System.Text;

namespace DebugSourceLink.Abstract
{
    public static class AppHostDefaults
    {
        #region Members.
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;
        public static readonly List<string> PublicUris = new List<string> { "/account/index", "/account/login" };
        public static readonly string INVOKER_TOKEN_HEADER = "DebugSourceLink_Authorization_Header";
        #endregion
    }
}
