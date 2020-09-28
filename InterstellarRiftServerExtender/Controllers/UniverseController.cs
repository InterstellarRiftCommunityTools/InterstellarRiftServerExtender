using IRSE.ResultObjects;
using System;
using System.Web.Http;

namespace IRSE.Controllers
{
    public class UniverseController : ApiController
    {
        [HttpGet]
        public BaseResult SaveGalaxy()
        {
            try
            {
                //ServerInstance.Instance.Handlers.ForceGalaxySave();

                return new BaseResult(false, "Method Disabled");
            }
            catch (Exception ex)
            {
                return new BaseResult(true, "SaveGalaxy() Exception: " + ex.ToString());
            }
        }
    }
}