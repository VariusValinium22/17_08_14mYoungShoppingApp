using System.Web;
using System.Web.Mvc;

namespace _17_08_14mYoungShoppingApp
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
