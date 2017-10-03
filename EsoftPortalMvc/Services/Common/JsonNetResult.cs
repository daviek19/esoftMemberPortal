using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EsoftPortalMvc.Services.Common
{
    public class JsonNetResult : JsonResult
{
  public object Data { get; set; }
  public bool includeTime { get; set; }

 
  public JsonNetResult()
  {
      includeTime = false;
  }
  public override void ExecuteResult(ControllerContext context)
  {
      HttpResponseBase response = context.HttpContext.Response;
      response.ContentType = "application/json";
      if (ContentEncoding != null)
          response.ContentEncoding = ContentEncoding;
      if (Data != null)
      {
          JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting.Indented };
          if (includeTime)
          {
              JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings()
              {
                  DateFormatString = "dd/MM/yyyy hh:mm:ss tt",
                  Formatting = Formatting.Indented
              });
              serializer.Serialize(writer, Data);
              writer.Flush();
          }
          else {
              JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings()
              {
                  DateFormatString = "dd/MM/yyyy",
                  Formatting = Formatting.Indented
              });
              serializer.Serialize(writer, Data);
              writer.Flush();
          }
      }
  }
}
}