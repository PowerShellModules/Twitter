﻿using BluebirdPS.Cmdlets.Base;
using BluebirdPS.Core;
using System.IO;
using System.Management.Automation;
namespace BluebirdPS.Cmdlets.Helpers
{
    [Cmdlet(VerbsData.Export, "BluebirdPSConfiguration")]
    public class ExportBluebirdPSConfigurationCommand : BaseCmdlet
    {
        protected override void ProcessRecord()
        {
            string _action = File.Exists(Configuration.ConfigurationPath) ? "existing" : "new";

            if (File.Exists(Configuration.CredentialsPath))
            {
                Configuration.AuthLastExportDate = File.GetLastWriteTime(Configuration.CredentialsPath);
            }

            string message = $"Saved BluebirdPS Configuration to {_action} file: {Configuration.ConfigurationPath}";
            WriteVerbose(message);

            Config.ExportConfiguration();
        }
    }
}
