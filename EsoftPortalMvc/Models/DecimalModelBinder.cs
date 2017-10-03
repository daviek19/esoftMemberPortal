using System;
using System.Globalization;
using System.Web.Mvc;

namespace EsoftPortalMvc
{
    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            ModelState modelState = new ModelState { Value = valueResult };

            object actualValue = null;

            if (valueResult.AttemptedValue != string.Empty)
            {
                try
                {
                    string attemptedValue = valueResult.AttemptedValue.ToString();
                    int index1 = attemptedValue.IndexOf('.', 0);
                    if (index1 != -1 && attemptedValue.Length >= index1 + 3)
                    {
                        attemptedValue = attemptedValue.Substring(0, index1 + 3);
                        attemptedValue = attemptedValue.Replace(",", "");
                    }

                    if (attemptedValue.Contains(".") && attemptedValue.Contains(","))
                    {
                        int index = attemptedValue.IndexOf(',', attemptedValue.IndexOf(',') + 1);
                        
                        if (index != -1)
                        {
                            attemptedValue = attemptedValue.Substring(0, index);
                        }
                        else
                        {
                            attemptedValue = attemptedValue.Substring(0, attemptedValue.Length);
                            // Not working a figure like 450,000.00 being converted to 450
                            //if (attemptedValue.Contains(".") && attemptedValue.Contains(","))
                            //{
                            //    index = attemptedValue.IndexOf(',');
                            //    attemptedValue = attemptedValue.Substring(0, index);
                            //}
                        }

                    }
                    if (attemptedValue.Length > 2 && attemptedValue.Substring(attemptedValue.Length - 2) == ",0")
                    {
                        attemptedValue = attemptedValue.Substring(0, attemptedValue.Length - 2);
                    }
                    string zero = attemptedValue.Replace(",", "").Replace(".", "");
                    if (string.IsNullOrWhiteSpace(zero) || string.IsNullOrEmpty(zero) || Convert.ToDecimal(zero) == 0)
                    {
                        attemptedValue = "0";
                    }
                    actualValue = Convert.ToDecimal(attemptedValue, CultureInfo.CurrentCulture);
                }
                catch (FormatException e)
                {
                    modelState.Errors.Add(e);
                }
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

            return actualValue;
        }
    }

}