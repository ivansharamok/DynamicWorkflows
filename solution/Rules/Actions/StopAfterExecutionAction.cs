// -----------------------------------------------------------------------
// <copyright file="StopAfterExecutionAction.cs" company="Sitecore">
// StopAfterExecutionAction.cs
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Rules.Actions
{
   using Sitecore.Diagnostics;
   using Sitecore.Rules;
   using Sitecore.Rules.Actions;
   using Sitecore.StringExtensions;

   /// <summary>
   /// Represents stop rule action.
   /// </summary>
   /// <typeparam name="T">
   /// Type of RuleContext
   /// </typeparam>
   public class StopAfterExecutionAction<T> : RuleAction<T> where T : RuleContext
   {
      /// <summary>
      /// Method aborts rule context preventing other rules from being executed.
      /// </summary>
      /// <param name="ruleContext">
      /// The rule context.
      /// </param>
      public override void Apply(T ruleContext)
      {
         Assert.ArgumentNotNull(ruleContext, "ruleContext");
         ruleContext.Abort();
         if (Settings.EnableDebug)
         {
            Log.Info("DynamicWorkflow::StopAfterExecutionAction has stopped further rule processing for item '{0}'".FormatWith(ruleContext.Item.Uri), this);
         }
      }
   }
}
