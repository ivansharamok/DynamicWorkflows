// -----------------------------------------------------------------------
// <copyright file="GetRules.cs" company="Sitecore">
// GetRules processor.
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Pipelines.WorkflowActionRule
{
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Rules;
    using Sitecore.SharedSource.Workflows.Rules;

    /// <summary>
    /// Retrieves workflow action rules.
    /// </summary>
    public class GetRules : WorkflowActionRuleProcessor
    {
        /// <summary>
        /// Runs pipeline processor code.
        /// </summary>
        /// <param name="context">Pipeline arguments.</param>
        public override void Process(WorkflowActionRuleContextArgs context)
        {
            Assert.ArgumentNotNull(context, "workflowActionRuleContextArgs");
            Assert.IsNotNull(context.RuleContext, "workflowRuleContext");
            context.Rules = this.BuildRuleList(context.RuleContext.Arguments.ProcessorItem);
        }

        /// <summary>
        /// Returns rules of a landing workflow action.
        /// </summary>
        /// <param name="processorItem">
        /// The processor item.
        /// </param>
        /// <returns>
        /// Returns RuleList of landing workflow.
        /// </returns>
        protected RuleList<WorkflowRuleContext> BuildRuleList(ProcessorItem processorItem)
        {
            Assert.ArgumentNotNull(processorItem, "processorItem");
            Item actionItem = processorItem.InnerItem;
            RuleList<WorkflowRuleContext> ruleList = new RuleList<WorkflowRuleContext>();
            if (!string.IsNullOrEmpty(actionItem["Rule"]))
            {
                ruleList.AddRange(RuleFactory.GetRules<WorkflowRuleContext>(actionItem.Fields["Rule"]).Rules);
            }

            if (!string.IsNullOrEmpty(actionItem["Rules"]))
            {
                Item[] rules = ((MultilistField)actionItem.Fields["Rules"]).GetItems();
                ruleList.AddRange(RuleFactory.GetRules<WorkflowRuleContext>(rules, "Rule").Rules);
            }

            return ruleList;
        }
    }
}
