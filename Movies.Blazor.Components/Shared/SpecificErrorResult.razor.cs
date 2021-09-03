using Microsoft.AspNetCore.Components;
using Movies.BusinessLogic.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Blazor.Components.Shared
{
    public partial class SpecificErrorResult
    {
        [Parameter]
        public string FieldName { get; set; }

        [Parameter]
        public Result Result { get; set; }

        [Parameter]
        public string Key { get; set; }

        protected override Task OnParametersSetAsync()
        {
            if (string.IsNullOrEmpty(FieldName))
            {
                FieldName = Key;
            }

            return base.OnParametersSetAsync();
        }
    }
}
