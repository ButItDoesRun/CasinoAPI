using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebServer.Services
{
    public class LoggingModel : PageModel
    {
        private readonly ILogger _logger;

        public LoggingModel(ILogger<LoggingModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("About page visited at {DT}",
                DateTime.UtcNow.ToLongTimeString());
        }
    }
}
