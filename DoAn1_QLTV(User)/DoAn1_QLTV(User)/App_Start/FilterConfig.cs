using System.Web;
using System.Web.Mvc;

namespace DoAn1_QLTV_User_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
