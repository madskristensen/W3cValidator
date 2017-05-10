using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;

namespace W3cValidator
{
    internal sealed class EnableValidation
    {
        private readonly Package _package;

        private EnableValidation(Package package)
        {
            _package = package;

            var commandService = ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (commandService != null)
            {
                var cmdId = new CommandID(PackageGuids.guidValidatorPackageCmdSet, PackageIds.EnableValidationId);
                var cmd = new OleMenuCommand(MenuItemCallback, cmdId);
                cmd.BeforeQueryStatus += BeforeQueryStatus;
                commandService.AddCommand(cmd);
            }
        }

        public static EnableValidation Instance
        {
            get;
            private set;
        }

        private IServiceProvider ServiceProvider
        {
            get { return _package; }
        }

        public static void Initialize(Package package)
        {
            Instance = new EnableValidation(package);
        }

        private void BeforeQueryStatus(object sender, EventArgs e)
        {
            var button = (MenuCommand)sender;
            button.Checked = ValidatorPackage.Options.RunOnPageLoad;
        }

        private void MenuItemCallback(object sender, EventArgs e)
        {
            var button = (MenuCommand)sender;

            ValidatorPackage.Options.RunOnPageLoad = !button.Checked;
            ValidatorPackage.Options.SaveSettingsToStorage();

            if (!ValidatorPackage.Options.RunOnPageLoad)
            {
                TableDataSource.Instance.CleanAllErrors();
            }
        }
    }
}
