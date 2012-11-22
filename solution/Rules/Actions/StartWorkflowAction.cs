// -----------------------------------------------------------------------
// <copyright file="StartWorkflowAction.cs" company="Sitecore">
// StartWorkflowAction.cs file
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Rules.Actions
{
   using Sitecore;
   using Sitecore.Data;
   using Sitecore.Data.Items;
   using Sitecore.Diagnostics;
   using Sitecore.Rules;
   using Sitecore.Rules.Actions;
   using Sitecore.StringExtensions;
   using Sitecore.Workflows;

   /// <summary>
   /// StarkWorkflowAction class
   /// </summary>
   /// <typeparam name="T">
   /// RuleContext type.
   /// </typeparam>
   public class StartWorkflowAction<T> : RuleAction<T>
      where T : RuleContext
   {
      /// <summary>
      /// Workflow ID
      /// </summary>
      private ID workflowId;

      /// <summary>
      /// Gets or sets WorkflowId.
      /// </summary>
      public ID WorkflowId
      {
         get
         {
            return this.workflowId ?? ID.Null;
         }

         set
         {
            Assert.ArgumentNotNull(value, "value");
            this.workflowId = value;
         }
      }

      /// <summary>
      /// Applies start workflow rule action to an item.
      /// </summary>
      /// <param name="ruleContext">
      /// The rule context.
      /// </param>
      public override void Apply(T ruleContext)
      {
         Assert.ArgumentNotNull(ruleContext, "ruleContext");
         Item item = ruleContext.Item;

         Assert.IsNotNull(item, "item");
         Assert.IsFalse(ID.IsNullOrEmpty(this.WorkflowId), "workflow is not set");
         using (new EditContext(item))
         {
            item[FieldIDs.Workflow] = this.WorkflowId.ToString();
         }

         IWorkflow workflow = item.Database.WorkflowProvider.GetWorkflow(this.WorkflowId.ToString());
         workflow.Start(item);
         if (Settings.EnableDebug)
         {
            Log.Audit("DynamicWorkflow::Workflow '{0}' started for item '{1}'.".FormatWith(this.WorkflowId, item.Uri), this);
         }
      }
   }
}
