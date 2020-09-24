/* Copyright (C) Extra-Terrestrial Technologies - All Rights Reserved
 * Unauthorized copying of this file, via any medium is prohibited not including
 * the individuals and/or companies stated below;
 *
 * -Split Polygon
 *
 * Proprietary and confidential
 * Written by General Wrex <generalwrex@gmail.com>, 2014
 */

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