// -----------------------------------------------------------------------
// <copyright file="WorkflowActionRuleProcessor.cs" company="Sitecore">
// Represents processor type for WorkflowActionRule pipeline.
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Pipelines.WorkflowActionRule
{
   /// <summary>
   /// Abstract processor type for WorkflowActionRule pipeline.
   /// </summary>
   public abstract class WorkflowActionRuleProcessor
   {
      /// <summary>
      /// Executes code for a processor of WorkflowActionRule pipeline.
      /// </summary>
      /// <param name="context">WorkflowActionRule context</param>
      public abstract void Process(WorkflowActionRuleContextArgs context);
   }
}
