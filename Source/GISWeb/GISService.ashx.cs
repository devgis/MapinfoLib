using System;
using System.Collections.Generic;
using System.Web;

namespace GISWeb
{
    /// <summary>
    /// GISService1 的摘要说明
    /// </summary>
    public class GISService1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            double x = 0;
            double y = 0;
            try
            {
                x = Convert.ToDouble(context.Request.Params["x"]);
            }
            catch
            {
                context.Response.Write(string.Empty);
                return;
            }

            try
            {
                y = Convert.ToDouble(context.Request.Params["y"]);
            }
            catch
            {
                context.Response.Write(string.Empty);
                return;
            }

            try
            {
                context.Response.Write(GISLib.SearchClass.Instance.SearchNearRoad(x, y));
            }
            catch(Exception ex)
            {
                context.Response.Write(string.Empty);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}