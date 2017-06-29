// -----------------------------------------------------------------------
// <copyright file="WorkflowRuleContext.cs" company="Sitecore">
// WorkflowRuleContext.cs.
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Rules
{
    using Sitecore.Rules;
    using Sitecore.Workflows.Simple;

    /// <summary>
    /// Represents workflow rule context.
    /// </summary>
    public class WorkflowRuleContext : RuleContext
    {
        /// <summary>
        /// Gets or sets workflow arguments.
        /// </summary>
        public WorkflowPipelineArgs Arguments { get; set; }

        /// <summary>
        /// Gets or sets indication of whether workflow command cancelation was a result of a rule configuration.
        /// </summary>
        public bool IsCancelledByRule { get; set; }
    }
}
