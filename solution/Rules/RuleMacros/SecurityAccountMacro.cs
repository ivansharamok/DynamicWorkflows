// -----------------------------------------------------------------------
// <copyright file="SecurityAccountMacro.cs" company="Sitecore">
// SecurityAccountMacro.cs.
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Rules.RuleMacros
{
   using System;
   using System.Xml.Linq;

   using Sitecore.Diagnostics;
   using Sitecore.Rules.RuleMacros;
   using Sitecore.Security;
   using Sitecore.Text;
   using Sitecore.Web.UI.Sheer;

   /// <summary>
   /// Represents SecurityAccount rule macro.
   /// </summary>
   public class SecurityAccountMacro : IRuleMacro
   {
      /// <summary>
      /// Macro main entry method
      /// </summary>
      /// <param name="element">
      /// The element.
      /// </param>
      /// <param name="name">
      /// The name.
      /// </param>
      /// <param name="parameters">
      /// The parameters.
      /// </param>
      /// <param name="value">
      /// The value.
      /// </param>
      public void Execute(XElement element, string name, UrlString parameters, string value)
      {
         Assert.ArgumentNotNull(element, "element");
         Assert.ArgumentNotNull(name, "name");
         Assert.ArgumentNotNull(parameters, "parameters");
         Assert.ArgumentNotNull(value, "value");

         SelectAccountOptions options = SelectAccountOptions.Parse();
         string securityType = parameters["type"];
         if (!string.IsNullOrEmpty(securityType))
         {
            if (securityType.Equals("role", StringComparison.InvariantCultureIgnoreCase))
            {
               options.ExcludeUsers = true;
            }
            else if (securityType.Equals("user", StringComparison.InvariantCultureIgnoreCase))
            {
               options.ExcludeRoles = true;
            }
         }

         string multipleSelection = parameters["multiselect"];
         if (!string.IsNullOrEmpty(multipleSelection))
         {
            options.Multiple = multipleSelection.Equals("true", StringComparison.InvariantCultureIgnoreCase);
         }

         SheerResponse.ShowModalDialog(options.ToUrlString().ToString(), true);
      }
   }
}
