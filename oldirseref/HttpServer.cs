/* Copyright (C) Extra-Terrestrial Technologies - All Rights Reserved
 * Unauthorized copying of this file, via any medium is prohibited not including
 * the individuals and/or companies stated below;
 * 
 * -Split Polygon 
 * 
 * Proprietary and confidential
 * Written by General Wrex <generalwrex@gmail.com>, 2014
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.IO;

namespace IRSE
{
	public static class ServerHost
	{
		private static HttpSelfHostServer m_server;

		public static void StartHttpServer(string url, int port)
		{
			try
			{
				Console.WriteLine("Starting Http Server...");
				var config = new HttpSelfHostConfiguration(string.Format("http://{0}:{1}/", url, port));

				config.Routes.MapHttpRoute(
					"API Default", Config.Settings.MainEndpointName + "/{controller}/{action}/{id}",
					new { id = RouteParameter.Optional });

				config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));


				m_server = new HttpSelfHostServer(config);
				m_server.OpenAsync().Wait();
				Console.WriteLine(string.Format("Http Server is running on {0}:{1}/{2}/.", url, port, Config.Settings.MainEndpointName));
			}
			catch (Exception ex)
			{
				Console.WriteLine("Server Controller Failed to Initialize! Haulting HTTP Server");
				Console.WriteLine(ex.ToString());
			}
		
		}

		public static void StopHttpServer()
		{
			Console.WriteLine("Stopping http server...");
			m_server.CloseAsync().Wait();
			m_server.Dispose();
			m_server = null;
			Console.WriteLine("Http server stopped successfully");
		}

	}
}
