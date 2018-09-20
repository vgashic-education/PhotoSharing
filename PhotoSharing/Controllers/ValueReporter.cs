using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoSharing.Controllers
{
	public class ValueReporter : ActionFilterAttribute
	{
		private void logValues(RouteData routeData)
		{
			string controllerName = routeData.Values["controller"].ToString();
			string actionName = routeData.Values["action"].ToString();

			Debug.WriteLine($"Controller: {controllerName}; Action: {actionName}", "ActionValues");

			foreach (var item in routeData.Values) 
			{
				Debug.WriteLine($">> Name: {item.Key}; Value: {item.Value}");
			}
		}


		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			logValues(filterContext.RouteData);
		}
	}
}