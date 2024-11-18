using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;

namespace Pmad.Milsymbol.App6d
{
    [DebuggerDisplay("{Code} : {Name}")]
    public class App6dAmplifier
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
