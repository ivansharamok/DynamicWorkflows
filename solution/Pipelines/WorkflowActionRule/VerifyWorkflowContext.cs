// -----------------------------------------------------------------------
// <copyright file="VerifyWorkflowContext.cs" company="Sitecore">
// VerifyWorkflowContext class
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Pipelines.WorkflowActionRule
{
    using Sitecore.Diagnostics;
    using Sitecore.StringExtensions;

    /// <summary>
    /// Ensures required parameters for workflow context.
    /// </summary>
    public class VerifyWorkflowContext : WorkflowActionRuleProcessor
    {
        /// <summary>
        /// Runs pipeline processor code.
        /// </summary>
        /// <param name="context"></param>
        public override void Process(WorkflowActionRuleContextArgs context)
        {
            Assert.ArgumentNotNull(context, "workflowActionRuleContextArgs");
            Assert.IsNotNull(context.RuleContext, "workflowRuleContext");
            if (context.DataItem == null || context.ActionItem == null)
            {
                Log.SingleWarn("DynamicWorkflow::Pipeline was aborted. The dataItem={0}, processorItem={1}".FormatWith(new object[]
                {
               context.DataItem == null ? "NULL" : context.DataItem.Uri.ToString(), context.ProcessorItem == null ? "NULL" : context.ProcessorItem.InnerItem.Uri.ToString()
                }), this);

                context.Failed = true;
                context.ErrorMessage = Settings.ErrorMessage;
                context.AbortPipeline();
            }
        }
    }
}
