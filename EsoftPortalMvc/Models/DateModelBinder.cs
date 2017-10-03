
using EsoftPortalMvc.Services.Common;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace EsoftPortalMvc
{
    public class DateModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            ModelState modelState = new ModelState { Value = valueResult };

            object actualValue = null;
            if (valueResult == null)
            {
               // modelState.Errors.Add("Value Is Null");
            }
            else
            {
                try
                {
                    actualValue = ValueConverters.ConvertNullToDatetime(valueResult.AttemptedValue);
                }
                catch (FormatException e)
                {
                    modelState.Errors.Add(e);
                }

                bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            }
            return actualValue;
        }
    }

}