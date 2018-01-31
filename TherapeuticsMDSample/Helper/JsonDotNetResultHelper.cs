using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TherapeuticsMDSample.Helper
{
    public class JsonDotNetResultHelper : JsonResult
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat
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

            //var success = this.Data.GetType().GetProperty("success");
            //if (success != null) {
            //    var successVal = (bool?)success.GetValue(success, null);
            //    if (successVal.HasValue && successVal.Value == false)
            //    {
            //        response.Status = "Problem occured";
            //        response.StatusCode = 500;
            //    }
            //}


            if (this.Data == null)
            {
                return;
            }

            //response.Write(JsonConvert.SerializeObject(this.Data, Settings));

            JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting.Indented };
            JsonSerializer serializer = JsonSerializer.Create(Settings);
            serializer.Serialize(writer, Data);
            writer.Flush();

        }
    }
}