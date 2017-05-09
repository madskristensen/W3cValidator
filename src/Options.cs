using Microsoft.VisualStudio.Shell;
using System.ComponentModel;

namespace W3cValidator
{
    public class Options : DialogPage
    {
        [Category("General")]
        [DisplayName("Run on page load")]
        [Description("Determines if the the W3C validator checker should run automatically on page load.")]
        [DefaultValue(true)]
        public bool RunOnPageLoad { get; set; } = true;
    }
}
