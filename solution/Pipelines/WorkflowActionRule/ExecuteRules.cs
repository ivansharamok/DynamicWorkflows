// -----------------------------------------------------------------------
// <copyright file="ExecuteRules.cs" company="Sitecore">
// ExecuteRules pipeline processor
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Pipelines.WorkflowActionRule
{
   using System;
   using Sitecore.Diagnostics;
   using Sitecore.SharedSource.Workflows.Rules;

   /// <summary>
   /// Responsible for execution of workflow action rules.
   /// </summary>
   public class ExecuteRules : WorkflowActionRuleProcessor
   {
      /// <summary>
      /// Runs pipeline processor code.
      /// </summary>
      /// <param name="context">Pipeline arguments.</param>
      public override void Process(WorkflowActionRuleContextArgs context)
      {
         Assert.ArgumentNotNull(context, "workflowActionRuleContextArgs");
         if (context.Rules != null && context.Rules.Count > 0)
         {
            var ruleContext = new WorkflowRuleContext()
            {
               Item = context.DataItem,
               Arguments = context.RuleContext.Arguments
            };
            try
            {
               context.Rules.Run(ruleContext);
            }
            catch (Exception ex)
            {
               context.Failed = true;
               Log.Error("DynamicWorkflow::Rule execution failed.", ex, this);
            }
            finally
            {
               if (context.Failed)
               {
                  context.ErrorMessage = Settings.ErrorMessage;
               }
            }
         }
      }
   }
}
