// -----------------------------------------------------------------------
// <copyright file="MoveToState.cs" company="Sitecore">
// MoveToState rule action
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Rules.Actions
{
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Rules.Actions;
    using Sitecore.StringExtensions;

    /// <summary>
    /// Class represents a workflow action Move to State.
    /// </summary>
    /// <typeparam name="T">
    /// RuleContext compatible type.
    /// </typeparam>
    public class MoveToState<T> : RuleAction<T> where T : WorkflowRuleContext
    {
        /// <summary>
        /// Gets or sets StateId for the action.
        /// </summary>
        public ID StateId { get; set; }

        /// <summary>
        /// Executes action's logic.
        /// </summary>
        /// <param name="ruleContext">
        /// The rule context.
        /// </param>
        public override void Apply(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            Item item = ruleContext.Item;
            if (item != null && !ID.IsNullOrEmpty(this.StateId))
            {
                ID originalTargetState = ruleContext.Arguments.NextStateId;
                ruleContext.Arguments.NextStateId = this.StateId;
                if (Sitecore.Configuration.Settings.GetBoolSetting("DynamicWorkflow.LogStateSkipping", false))
                {
                    Log.Info("DynamicWorkflow::Item '{0}' was detoured from {1} workflow state to {2} one by the rule.".FormatWith(item.ID, originalTargetState, this.StateId), this);
                }
            }
        }
    }
}
