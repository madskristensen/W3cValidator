using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.Shell.Interop;
using Newtonsoft.Json;

namespace W3cValidator
{
    class ValidationResult
    {
        public string Url { get; set; }
        public string Project { get; set; }

        [JsonProperty("messages")]
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public string Extract { get; set; }
        public string Message { get; set; }
    }
}
