using System;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Portolo.Framework.Web
{
    public class UltimateJsonResult : JsonResult
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind, DateParseHandling = DateParseHandling.DateTimeOffset
        };

        public override void ExecuteResult(ControllerContext context)
        {
            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("GET request not allowed");
            }

            var response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/json";

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data == null)
            {
                return;
            }

            this.MaxJsonLength = int.MaxValue;
            response.Write(JsonConvert.SerializeObject(this.Data, Settings));
        }
    }
}
