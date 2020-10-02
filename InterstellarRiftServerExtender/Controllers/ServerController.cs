using IRSE.Managers;
using IRSE.API.ResultObjects;
using System;
using System.Web.Http;

namespace IRSE.Controllers
{
    public class ServerController : ApiController
    {
        [HttpGet]
        public ServerStatusResult GetStatus()
        {
            try
            {
                return new ServerStatusResult(false, "Hai Ettna! Here are some juicy status updates for you!", ServerInstance.Instance.Uptime);
            }
            catch (Exception ex)
            {
                return new ServerStatusResult(true, "GetStatus() Exception:" + ex.ToString(), new TimeSpan());
            }
        }
    }
}