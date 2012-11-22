// -----------------------------------------------------------------------
// <copyright file="WorkflowRuleAction.cs" company="Sitecore">
// Workflow action that evaluates rules configured for workflow action item.
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Actions
{
   using Sitecore.Diagnostics;
   using Sitecore.Pipelines;
   using Sitecore.SharedSource.Workflows.Pipelines.WorkflowActionRule;
   using Sitecore.SharedSource.Workflows.Rules;
   using Sitecore.Workflows.Simple;

   /// <summary>
   /// This workflow action is intended for landing workflows.
   /// It's job is to transfer an item to a destination workflow based on a condition.
   /// </summary>
   public class WorkflowRuleAction
   {
      /// <summary>
      /// Main entry method of the workflow action.
      /// </summary>
      /// <param name="args">
      /// The workflow action arguments.
      /// </param>
      public void Process(WorkflowPipelineArgs args)
      {
         Assert.ArgumentNotNull(args, "args");
         var ruleContextArgs = new WorkflowRuleContext() { Arguments = args };
         var pipelineArgs = new WorkflowActionRuleContextArgs(ruleContextArgs);
         CorePipeline.Run("runWorkflowActionRules", pipelineArgs);
      }
   }
}