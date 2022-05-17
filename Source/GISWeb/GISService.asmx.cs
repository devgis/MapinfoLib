using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace GISWeb
{
    /// <summary>
    /// GISService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class GISService : System.Web.Services.WebService
    {

        [WebMethod]
        public string SearchNearRoad(double x,double y)
        {
            return GISLib.SearchClass.Instance.SearchNearRoad(x, y);
        }
    }
}
