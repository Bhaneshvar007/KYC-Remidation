using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.Data;

namespace MicrosoftEmailService.Models
{
    public class readEmail
    {


        public static async Task<List<readEmailModel>> ReadEmail(ParaModel model)
        {
            int starthour = Convert.ToInt32(ConfigurationManager.AppSettings.Get("dts_email_HourStart"));
            DataTable dtExisting = new DataTable();
            DataRow[] aRow;

            List<readEmailModel> MessageData = new List<readEmailModel>();
            try
            {
                ApplicationSettings bll = CommonHelper.GetApplicationSettings(model.application_code);
                if (!string.IsNullOrWhiteSpace(bll.tenant_id) && !string.IsNullOrWhiteSpace(bll.client_id) && !string.IsNullOrWhiteSpace(bll.client_secret))
                {

                    ClientSecretCredential secretCredential = new ClientSecretCredential(bll.tenant_id, bll.client_id, bll.client_secret, new TokenCredentialOptions()
                    {
                        AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
                    });
                    GraphServiceClient graphServiceClient = new GraphServiceClient((TokenCredential)secretCredential);
                    IUserMessagesCollectionPage async1 = await graphServiceClient.Users[bll.user_object_id].Messages.Request().Top(100).GetAsync();
                    CommonHelper.WriteLog("total count :" + async1.Count.ToString());
                    if (async1.Count > 0)
                    {
                        DateTime starttime = DateTime.Now.AddHours(-starthour);
                        if (model.application_code == "DTS")
                        {
                            dtExisting = CommonHelper.getDataTable("getEmailUtilityEmailAudit", null);
                        }

                        foreach (Message message in (IEnumerable<Message>)async1.CurrentPage)
                        {
                            if (model.application_code == "DTS")
                            {
                                aRow = dtExisting.Select("MessageUid = '" + message.InternetMessageId + "'");
                                if (aRow.Length > 0)
                                {
                                    continue;
                                }
                                //if (message.ReceivedDateTime.Value.LocalDateTime < starttime)
                                //{
                                //    continue;
                                //}
                            }
                            readEmailModel Message = new readEmailModel();
                            Message.MessageID = message.InternetMessageId;// message.Id;
                            Message.ConversationId = message.ConversationId;
                            Message.EmailFrom = message.Sender.EmailAddress.Address;
                            Message.EmailSubject = message.Subject;
                            Message.EmailMesaage = message.Body.Content.ToString();
                            Message.EmailRecieveDateTime = message.ReceivedDateTime.Value.LocalDateTime;
                            Message.EmailRecieveDateTimeUTC = message.ReceivedDateTime.Value.UtcDateTime;
                            DateTime dateTime = Convert.ToDateTime(Message.EmailRecieveDateTime);
                            dateTime = dateTime.Date;
                            if (dateTime.CompareTo(DateTime.Now.Date) == 0)
                            {
                                if (message.CcRecipients != null)
                                {
                                    Message.EmailCC = new List<string>();
                                    foreach (Recipient ccRecipient in message.CcRecipients)
                                        Message.EmailCC.Add(ccRecipient.EmailAddress.Address);
                                }
                                readEmailModel readEmailModel = Message;
                                bool? hasAttachments = message.HasAttachments;
                                int num = hasAttachments.Value ? 1 : 0;
                                readEmailModel.EmailHasAttachments = num != 0;
                                hasAttachments = message.HasAttachments;
                                if (hasAttachments.Value)
                                {
                                    IMessageAttachmentsCollectionPage async2 = await graphServiceClient.Users[bll.user_object_id].Messages[message.Id].Attachments.Request().GetAsync();
                                    if (async2.CurrentPage.Count > 0)
                                        Message.EmailAttachments = async2.CurrentPage;
                                }
                                MessageData.Add(Message);
                                Message = (readEmailModel)null;
                            }
                        }

                    }
                    graphServiceClient = (GraphServiceClient)null;
                }
                else
                    CommonHelper.WriteLog("Something missing from TenantId/ClientID/ClientSecret,Please update ApplicationSettins.json");
                return MessageData;
            }
            catch (Exception ex)
            {
                CommonHelper.WriteLog("error in function:ReadEmail():" + ex.Message);
                CommonHelper.WriteLog("error in function:ReadEmail():" + ex.InnerException?.ToString());
                return MessageData;
            }
        }


    }
    public class readEmailModel
    {
        public string MessageID { get; set; }
        public string ConversationId { get; set; }
        public string EmailFrom { get; set; }
        public string EmailSubject { get; set; }

        public string EmailMesaage { get; set; }
        public DateTime EmailRecieveDateTime { get; set; }
        public DateTime EmailRecieveDateTimeUTC { get; set; }
        public List<string> EmailCC { get; set; }

        public bool EmailHasAttachments { get; set; }

        public IList<Attachment> EmailAttachments { get; set; }
    }


}