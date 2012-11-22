// -----------------------------------------------------------------------
// <copyright file="MoveItemToAction.cs" company="Sitecore">
// MoveItemToAction.cs.
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Rules.Actions
{
   using Sitecore.Data;
   using Sitecore.Data.Items;
   using Sitecore.Diagnostics;
   using Sitecore.Rules;
   using Sitecore.Rules.Actions;
   using Sitecore.StringExtensions;

   /// <summary>
   /// Represents MoveItemTo rule action.
   /// </summary>
   /// <typeparam name="T">
   /// Type of RuleContext
   /// </typeparam>
   public class MoveItemToAction<T> : RuleAction<T>
      where T : RuleContext
   {
      /// <summary>
      /// Gets or sets DestinationId.
      /// </summary>
      public ID DestinationId { get; set; }

      /// <summary>
      /// Applies rule action.
      /// </summary>
      /// <param name="ruleContext">
      /// The rule context.
      /// </param>
      public override void Apply(T ruleContext)
      {
         Assert.ArgumentNotNull(ruleContext, "ruleContext");
         Item item = ruleContext.Item;
         if (item != null && !ID.IsNullOrEmpty(this.DestinationId) && item.ParentID != this.DestinationId)
         {
            Item destinationItem = item.Database.GetItem(this.DestinationId);
            if (destinationItem != null)
            {
               item.MoveTo(destinationItem);
               if (Settings.EnableDebug)
               {
                  Log.Info("DynamicWorkflow::Item '{0} moved to '{1}' location".FormatWith(item.Uri, destinationItem.Uri), this);
               }
            }
         }
      }
   }
}
