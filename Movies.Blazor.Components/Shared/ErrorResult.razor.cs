using Microsoft.AspNetCore.Components;
using Movies.BusinessLogic.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Blazor.Components.Shared
{
    public partial class ErrorResult
    {
        [Parameter]
        public Result Result { get; set; }
    }
}
