using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GetUserIPProject.Controllers
{
    [Route("api/ip")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "api")]
    public class IpController : ControllerBase
    {
        private readonly ILogger<IpController> _logger;
        public IpController(ILogger<IpController> logger)
        {
            this._logger = logger;
        }

        [HttpGet("health-check")]
        [AllowAnonymous]
        public IActionResult HealthCheck()
        {
            return Ok(new
            {
                message = "Good",
                time = DateTime.Now.ToString()
            });
        }

        [HttpGet("current")]
        public IActionResult GetUserIp()
        {
            string ipClient = "";
            string port = HttpContext.Connection.RemotePort.ToString();

            try
            {
                ipClient = HttpContext.Connection.RemoteIpAddress.ToString();
            } catch (NullReferenceException ex) 
            {
                this._logger.LogInformation($"Can not get information of ip, details: {ex.Message}");
                return NotFound(new
                {
                    message = "Not found your ip address, please check again",
                    ip = "",
                    port = port
                });
            }
            
            Dictionary<string, object> response = new Dictionary<string, object>()
            {
                {"message", "SUCCESS" },
                {"ip", ipClient },
                {"port", port }
            };

            try
            {
                string json = JsonSerializer.Serialize(response);
                // Log information of response
                this._logger.LogInformation($"Get ip address complete {json}");
            } catch (NotSupportedException notSupport)
            {
                this._logger.LogInformation($"Response type not support: {notSupport.Message}");
                return BadRequest(new
                {
                    message = "Can not cast data into json type, please try again",
                    ip = "",
                    port = port
                });
            }
            return Ok(response);
        }
    }
}
