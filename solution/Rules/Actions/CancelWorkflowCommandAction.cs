// -----------------------------------------------------------------------
// <copyright file="CancelWorkflowCommandAction.cs" company="Sitecore">
// CancelWorkflowCommandAction.cs.
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Rules.Actions
{
    using Sitecore.Diagnostics;
    using Sitecore.Rules.Actions;
    using Sitecore.SharedSource.Workflows.Rules;
    using Sitecore.StringExtensions;

    /// <summary>
    /// Represent cancel workflow command rule action.
    /// </summary>
    /// <typeparam name="T">
    /// Type of RuleContext
    /// </typeparam>
    public class CancelWorkflowCommandAction<T> : RuleAction<T>
       where T : WorkflowRuleContext
    {
        /// <summary>
        /// Applies rule action to stop workflow command or action execution.
        /// </summary>
        /// <param name="ruleContext">
        /// The rule context.
        /// </param>
        public override void Apply(T ruleContext)
        {
            if (ruleContext.Arguments != null)
            {
                ruleContext.Arguments.AbortPipeline();
                ruleContext.IsCancelledByRule = true;
                if (Settings.EnableDebug)
                {
                    Log.Info("DynamicWorkflow::Workflow command for item '{0}' was cancelled by the rule.".FormatWith(ruleContext.Item.Uri), this);
                }
            }
        }
    }
}
