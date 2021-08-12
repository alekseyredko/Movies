using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.BlazorWasm.Options
{
    public class HttpOptions
    {
        public const string Position = "HttpClientOptions";

        public string ServerEndpoint { get; set; }
    }
}
