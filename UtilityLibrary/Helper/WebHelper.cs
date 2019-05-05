using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLibrary.Helper
{
    public class WebHelper
    {
        public static async Task<string> GetWebPageSourceCode(Uri requestUri)
        {
            string message = string.Empty;
            HttpClient httpClient = new HttpClient();
            var result = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, requestUri));
            if (result.IsSuccessStatusCode)
            {
                message = await result.Content.ReadAsStringAsync();
            }
            return message;
        }
    }
}
