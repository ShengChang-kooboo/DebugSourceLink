using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DebugSourceLink.Services
{
    /// <summary>
    /// ExecuteAsync method will return one completed task, THIS WAY IS RECOMMENDED!
    /// Original Version: https://www.cnblogs.com/zhiyong-ITNote/p/9898442.html
    /// </summary>
    public class TimedBackgroundServiceOne:BackgroundService
    {
        #region Members.
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly string _datetimeFormat;
        #endregion

        #region Constructors.
        public TimedBackgroundServiceOne(ILogger<TimedBackgroundServiceOne> logger)
        {
            _logger = logger;
            _datetimeFormat = "yyyy-MM-dd hh:mm:ss.fff";
        }
        #endregion

        #region Methods.
        /// <summary>
        /// This method will only be invoked once at the startup of app
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, DateTime.UtcNow.ToString(_datetimeFormat), TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(20));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToString(_datetimeFormat)}]=====Hello timed background service!");
        }

        public override void Dispose()
        {
            base.Dispose();
            _timer?.Dispose();
        }
        #endregion
    }


    /// <summary>
    /// ExecuteAsync method will return one uncompleted task
    /// </summary>
    public class TimedBackgroundServiceTwo:BackgroundService
    {
        #region Members.
        private readonly ILogger _logger;
        private readonly string _datetimeFormat;
        #endregion

        #region Constructors.
        public TimedBackgroundServiceTwo(ILogger<TimedBackgroundServiceTwo> logger)
        {
            _logger = logger;
            _datetimeFormat = "yyyy-MM-dd hh:mm:ss.fff";
        }
        #endregion

        #region Methods.
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToString(_datetimeFormat)}]=====LongRunning Service is starting.");
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"[{DateTime.UtcNow.ToString(_datetimeFormat)}]=====Finishing task one.");
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                _logger.LogDebug($"[{DateTime.UtcNow.ToString(_datetimeFormat)}]=====Finishing task two.");
            }
            _logger.LogInformation($"[{DateTime.UtcNow.ToString(_datetimeFormat)}]=====LongRunning Service is stoping.");
        }

        public override void Dispose()
        {
            base.Dispose();
        }
        #endregion
    }
}
