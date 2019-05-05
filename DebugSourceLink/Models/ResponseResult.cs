using System;

namespace DebugSourceLink.Models
{
    [Serializable]
    public class ResponseResult
    {
        public string Result { get; set; }
        public bool Status { get; set; }
        public string ErrorMessage { get; set; }
        public ResponseCode ResponseCode { get; set; }

        public ResponseResult()
        {
            Result = string.Empty;
        }
    }

    public enum ResponseCode
    {
        Successful = 1,
        InvalidArguments = 2,
        Unauthorized = 3,
        Timeoout = 4
    }
}
