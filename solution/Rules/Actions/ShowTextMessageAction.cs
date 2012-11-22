// -----------------------------------------------------------------------
// <copyright file="ShowTextMessageAction.cs" company="Sitecore">
// ShowTextMessageAction.cs.
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Rules.Actions
{
   using Sitecore.Globalization;
   using Sitecore.Rules.Actions;
   using Sitecore.SharedSource.Workflows.Rules;
   using Sitecore.Web.UI.Sheer;

   /// <summary>
   /// TODO: Update summary.
   /// </summary>
   /// <typeparam name="T">
   /// Type of RuleContext
   /// </typeparam>
   public class ShowTextMessageAction<T> : RuleAction<T> where T : WorkflowRuleContext
   {
      /// <summary>
      /// Gets or sets TextMessage.
      /// </summary>
      public string TextMessage { get; set; }

      /// <summary>
      /// Applies rule action to show text message.
      /// </summary>
      /// <param name="ruleContext">
      /// The rule context.
      /// </param>
      public override void Apply(T ruleContext)
      {
         if (!string.IsNullOrEmpty(this.TextMessage))
         {
            SheerResponse.Alert(Translate.Text(this.TextMessage), new string[0]);
         }
      }
   }
}
