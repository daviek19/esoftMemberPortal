﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsoftPortalMvc.Models
{
    public interface IValidationDictionary
    {
        void AddError(string key, string errorMessage);
        bool IsValid { get; }
    }
}