using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace W3cValidator
{
    internal static class VsHelpers
    {
        public static DTE2 DTE => ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE2;

    }
}
