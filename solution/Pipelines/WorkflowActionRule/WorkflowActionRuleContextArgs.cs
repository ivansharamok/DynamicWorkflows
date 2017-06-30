// -----------------------------------------------------------------------
// <copyright file="WorkflowActionRuleContextArgs.cs" company="Sitecore">
// Represents arguments for WorkflowActionRuleContext pipeline.
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Pipelines.WorkflowActionRule
{
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Rules;
    using Sitecore.SharedSource.Workflows.Rules;

    /// <summary>
    /// Represents arguments for WorkflowActionRuleContext pipeline.
    /// </summary>
    public class WorkflowActionRuleContextArgs : Sitecore.Pipelines.PipelineArgs
    {
        /// <summary>
        /// Instantiates an object of WorkflowActionRuleContextArgs type.
        /// </summary>
        /// <param name="context">WorkflowRuleContext object</param>
        public WorkflowActionRuleContextArgs(WorkflowRuleContext context)
        {
            Assert.ArgumentNotNull(context, "workflowRuleContext");
            this.RuleContext = context;

            Assert.IsNotNull(context.Arguments, "context.Arguments");
            this.ActionItem = context.Arguments.ProcessorItem;
            this.DataItem = context.Arguments.DataItem;
            this.Failed = false;
        }

        /// <summary>
        /// Gets or sets action item instance.
        /// </summary>
        public ProcessorItem ActionItem { get; set; }

        /// <summary>
        /// Gets or sets data item instance.
        /// </summary>
        public Item DataItem { get; set; }

        /// <summary>
        /// Gets or sets RuleContext property
        /// </summary>
        public WorkflowRuleContext RuleContext { get; set; }

        /// <summary>
        /// Gets or sets rules for a workflow action.
        /// </summary>
        public RuleList<WorkflowRuleContext> Rules { get; set; }

        /// <summary>
        /// Gets or sets success of the WorkflowActionRule pipeline.
        /// </summary>
        public bool Failed { get; set; }

        /// <summary>
        /// Gets or sets error message.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
