// -----------------------------------------------------------------------
// <copyright file="Settings.cs" company="Sitecore">
// Settings class
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows
{
    using Sitecore.Globalization;

    /// <summary>
    /// Represents DynamicWorkflow settings.
    /// </summary>
    public static class Settings
    {
        public static bool EnableDebug
        {
            get
            {
                return Configuration.Settings.GetBoolSetting("DynamicWorkflow.EnableDebug", false);
            }
        }

        public static string ErrorMessage
        {
            get
            {
                return Translate.Text("An error happened during workflow action rule processing. Please check the log file for more details.");
            }
        }
    }
}
