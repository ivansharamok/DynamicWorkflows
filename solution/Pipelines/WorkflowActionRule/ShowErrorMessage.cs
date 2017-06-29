// -----------------------------------------------------------------------
// <copyright file="ShowErrorMessage.cs" company="Sitecore">
// ShowErrorMessage pipeline processor.
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Pipelines.WorkflowActionRule
{
    using Sitecore.Diagnostics;
    using Sitecore.Web.UI.Sheer;

    /// <summary>
    /// Responsible for showing displaying error message to a user.
    /// </summary>
    public class ShowErrorMessage : WorkflowActionRuleProcessor
    {
        /// <summary>
        /// Runs pipeline processor code.
        /// </summary>
        /// <param name="context">Pipeline arguments.</param>
        public override void Process(WorkflowActionRuleContextArgs context)
        {
            Assert.ArgumentNotNull(context, "workflowActionRuleContextArgs");
            if (context.Failed)
            {
                SheerResponse.Alert(context.ErrorMessage, new string[0]);
            }
        }
    }
}
