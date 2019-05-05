using System;

namespace DebugSourceLink.Models
{
    /// <summary>
    /// Original Version: https://www.cnblogs.com/runningsmallguo/p/10234307.html#commentform
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}