using Microsoft.VisualStudio.Web.BrowserLink;
using Newtonsoft.Json;
using System.ComponentModel.Composition;
using System.IO;

namespace W3cValidator
{
    [Export(typeof(IBrowserLinkExtensionFactory))]
    public class W3cValidatorFactory : IBrowserLinkExtensionFactory
    {
        public BrowserLinkExtension CreateExtensionInstance(BrowserLinkConnection connection)
        {
            return new W3cValidator();
        }

        public string GetScript()
        {
            using (Stream stream = GetType().Assembly.GetManifestResourceStream("W3cValidator.BrowserLink.W3cValidator.js"))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    public class W3cValidator : BrowserLinkExtension
    {
        public override void OnConnected(BrowserLinkConnection connection)
        {
            if (connection.Project == null)
            {
                return;
            }

            Browsers.Client(connection).Invoke("initialize", connection.Project.Name);

            if (ValidatorPackage.Options.RunOnPageLoad)
            {
                Browsers.Client(connection).Invoke("validate");
            }
        }

        [BrowserLinkCallback]
        public void Report(string json, string url, string project)
        {
            var result = JsonConvert.DeserializeObject<ValidationResult>(json);
            result.Project = project;
            result.Url = url;

            ErrorListService.Process(result);

            System.Diagnostics.Debug.Write(json);
        }
    }
}