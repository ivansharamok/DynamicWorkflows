// -----------------------------------------------------------------------
// <copyright file="SendEmailAction.cs" company="Sitecore">
// SendEmailAction.cs.
// </copyright>
// -----------------------------------------------------------------------

namespace Sitecore.SharedSource.Workflows.Rules.Actions
{
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Globalization;
    using Sitecore.Rules.Actions;
    using Sitecore.Security.Accounts;
    using Sitecore.SharedSource.Workflows.Rules;
    using Sitecore.StringExtensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    /// <summary>
    /// Represents send email rule action.
    /// </summary>
    /// <typeparam name="T">
    /// Type of RuleContext
    /// </typeparam>
    public class SendEmailAction<T> : RuleAction<T> where T : WorkflowRuleContext
    {
        /// <summary>
        /// Gets or sets email From field.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets email To field.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets email Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets email Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets email MailServer.
        /// If not set, default mail server from web.config is used.
        /// </summary>
        public string MailServer { get; set; }

        /// <summary>
        /// Gets or sets email server port.
        /// </summary>
        public string ServerPort { get; set; }

        /// <summary>
        /// Rule action main entry method.
        /// </summary>
        /// <param name="ruleContext">
        /// The rule context.
        /// </param>
        public override void Apply(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            Assert.IsNotNull(ruleContext.Arguments, "arguments");
            if (!string.IsNullOrEmpty(this.From) && !string.IsNullOrEmpty(this.To))
            {
                this.SendEmail(
                   this.From, this.To, this.Title, this.Message, this.MailServer, this.ServerPort, ruleContext.Arguments.DataItem);
                if (Settings.EnableDebug)
                {
                    Log.Info("DynamicWorkflow::Email has been sent for item '{0}'.".FormatWith(ruleContext.Item.Uri), this);
                }
            }
            else
            {
                Log.Error(Translate.Text("Failed to send an email due to missing From and To fields"), this);
            }
        }

        /// <summary>
        /// Sends email
        /// </summary>
        /// <param name="from">
        /// The email from field.
        /// </param>
        /// <param name="to">
        /// The email to field.
        /// </param>
        /// <param name="title">
        /// The email title.
        /// </param>
        /// <param name="message">
        /// The email message.
        /// </param>
        /// <param name="mailServer">
        /// The mail server.
        /// </param>
        /// <param name="port">
        /// The mail server port.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        protected void SendEmail(string from, string to, string title, string message, string mailServer, string port, Item item)
        {
            Assert.ArgumentNotNull(item, "item");
            SmtpClient smtpClient = new SmtpClient();
            StringBuilder body = new StringBuilder(message);
            string recepients = this.EnsureEmailSeparator(to);
            if (recepients.ToLower().Contains("role="))
            {
                recepients = this.GetRecepients(recepients);
                if (string.IsNullOrEmpty(recepients))
                {
                    Log.Error("Email rule action error. Failed to retrieve emails from specified role(s) members.", this);
                    return;
                }
            }

            body.Append(Environment.NewLine);
            body.AppendLine(
               "Item path: {0}, Language: {1}, Version: {2}".FormatWith(
                  new object[] { item.Paths.FullPath, item.Language.ToString(), item.Version.ToString() }));
            if (!string.IsNullOrEmpty(mailServer))
            {
                smtpClient.Host = mailServer;
                if (!string.IsNullOrEmpty(port))
                {
                    int portNumber;
                    if (int.TryParse(port, out portNumber))
                    {
                        smtpClient.Port = portNumber;
                    }
                    else
                    {
                        Log.Warn(Translate.Text("Mail server port number is invalid. Port number 25 was be used."), this);
                    }
                }
            }
            else
            {
                string server = Sitecore.Configuration.Settings.MailServer;
                if (!string.IsNullOrEmpty(server))
                {
                    smtpClient.Host = server;
                    smtpClient.Port = Sitecore.Configuration.Settings.MailServerPort;
                    string userName = Sitecore.Configuration.Settings.MailServerUserName;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        smtpClient.Credentials = new NetworkCredential(
                           userName, Sitecore.Configuration.Settings.MailServerPassword);
                        smtpClient.UseDefaultCredentials = false;
                    }
                }
                else
                {
                    Log.Error(Translate.Text("Failed to send an email due to missing mail server value"), this);
                }
            }

            MailMessage mailMessage = new MailMessage(from, recepients) { Subject = title, Body = body.ToString() };
            smtpClient.Send(mailMessage);
        }

        /// <summary>
        /// Gets emails for all members of specified roles.
        /// </summary>
        /// <param name="roleNames">
        /// The role names.
        /// </param>
        /// <returns>
        /// Returns emails as a string value separated by ';'.
        /// </returns>
        protected virtual string GetRecepients(string roleNames)
        {
            string recepients = string.Empty;
            string[] parameters = roleNames.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            if (parameters.Length == 2)
            {
                string[] roles = parameters[1].Split(
                   new char[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries);
                recepients =
                   roles.Select(this.GetRoleMembers).Where(emials => !string.IsNullOrEmpty(emials)).Aggregate(
                      recepients, (current, emials) => string.Join(",", new string[] { current, emials }));
            }

            if (string.IsNullOrEmpty(recepients))
            {
                return recepients;
            }

            return recepients[0] == ',' ? recepients.Substring(1) : recepients;
        }

        /// <summary>
        /// Gets user emails for a specified role.
        /// </summary>
        /// <param name="roleName">
        /// The role name.
        /// </param>
        /// <returns>
        /// Returns user's emails.
        /// </returns>
        private string GetRoleMembers(string roleName)
        {
            IEnumerable<string> emails = null;
            Role role = Role.Exists(roleName) ? Role.FromName(roleName) : null;
            if (role != null)
            {
                IEnumerable<Account> users = RolesInRolesManager.GetRoleMembers(role, false).Where(account => account.AccountType == AccountType.User);
                emails = users.Where(user => !string.IsNullOrEmpty(((User)user).Profile.Email)).Select(user => ((User)user).Profile.Email);
            }

            string emailList = emails != null ? string.Join(",", emails.ToArray()) : null;
            return emailList;
        }

        /// <summary>
        /// Ensures that multiple emails separated by comma.
        /// </summary>
        /// <param name="emails">
        /// The email list.
        /// </param>
        /// <returns>
        /// Returns a list of emails separated by comma.
        /// </returns>
        private string EnsureEmailSeparator(string emails)
        {
            return emails.Replace(';', ',');
        }
    }
}
