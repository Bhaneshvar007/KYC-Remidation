using DocumentFormat.OpenXml.EMMA;
using KYCAPP.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KYCAPP.Web.CustomHelper
{
    internal class EmailHelper
    {

        string MailSmtp = ConfigurationManager.AppSettings["SmtpPrimaryMailServer"].ToString();
        string ccEmailId = ConfigurationManager.AppSettings["ccEmailId"].ToString();
        public void SendMail(List<MAIL_DATA> objMail, string toMailId, string fromMailId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<p> Dear Sir/Madam,<p>");
            sb.AppendLine("<p style='text-align: justify; text-justify: inter-word;'> This has reference to the folios in the KYC Remediation Module Mapped to our UFC. We understand that some");
            sb.AppendLine(" folios have address which are fall in jurisdiction of your UFC.</p>");
            sb.AppendLine("<p style='text-align: justify; text-justify: inter-word;'> We have therefore marked the following folios to your UFC and they will be available for allocation to your ");
            sb.AppendLine("team members. You may please allocate the folios to your team members for remediation purpose</p>");
            sb.AppendLine("<table border='1' width='100%'> <thead> <tr> <th>Sr No</th> <th> Folio Number</th><th> Investor Name</th></tr></thead>");
            sb.AppendLine("<tbody>");
            foreach (var item in objMail)
            {
                sb.AppendLine("<tr><td>" + item.SRNO + "</td><td>" + item.ACNO + "</td><td>" + item.INVNAME + "</td></tr>");
            }
            sb.AppendLine("</tbody></table>");
            sb.AppendLine("<p> Regards </p>");
            sb.AppendLine("<p>" + objMail[0].CM_Name + "</p>");
            string strEmpEmail = string.Empty;
            try
            {
                MailMessage msgMail = new MailMessage();
                strEmpEmail = toMailId;
                string strEmailID = strEmpEmail;
                string[] Multi = strEmailID.Trim().Split(',');
                foreach (string MultiEmailID in Multi)
                {
                    if (MultiEmailID.Contains("@"))
                    {
                        msgMail.To.Add(new MailAddress(MultiEmailID));
                    }
                }

                //For Multi CC Email ID
                string strCCEmailID = ccEmailId + ";" + fromMailId;
                string[] MultiCCMailId = strCCEmailID.Trim().Split(';');
                for (int i = 0; i < MultiCCMailId.Count(); i++)
                {
                    if (MultiCCMailId[i].Contains("@"))
                    {
                        msgMail.CC.Add(new MailAddress(MultiCCMailId[i]));
                    }

                }



                msgMail.From = new MailAddress(fromMailId);
                msgMail.Subject = "Reallocation of Folios – Inter UFC Transfer";
                msgMail.Body = sb.ToString();
                msgMail.IsBodyHtml = true;
                SmtpClient sc = new SmtpClient(MailSmtp, 25);
                sc.Send(msgMail);
                CommonHelper.WriteLog("Mail Send successfully..");

            }
            catch (Exception ex)
            {
                CommonHelper.WriteLog("Some error in sending mail.." + ex.Message);
            }

        }

        public void SendMail_SameUFC(List<MAIL_DATA> objMail, string toMailId, string fromMailId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<p> Dear Sir/Madam,<p>");
            sb.AppendLine("<p style='text-align: justify; text-justify: inter-word;'>We have retagged the below mentioned folios to you for KYC Remediation.</p>");
            sb.AppendLine("<table border='1' width='100%'> <thead> <tr> <th>Sr No</th> <th> Folio Number</th><th> Investor Name</th></tr></thead>");
            sb.AppendLine("<tbody>");
            foreach (var item in objMail)
            {
                sb.AppendLine("<tr><td>" + item.SRNO + "</td><td>" + item.ACNO + "</td><td>" + item.INVNAME + "</td></tr>");
            }
            sb.AppendLine("</tbody></table>");
            sb.AppendLine("<p> Regards </p>");
            sb.AppendLine("<p>" + objMail[0].CM_Name + "</p>");
            string strEmpEmail = string.Empty;
            try
            {
                MailMessage msgMail = new MailMessage();
                strEmpEmail = toMailId;
                string strEmailID = strEmpEmail;
                string[] Multi = strEmailID.Trim().Split(',');
                foreach (string MultiEmailID in Multi)
                {
                    if (MultiEmailID.Contains("@"))
                    {
                        msgMail.To.Add(new MailAddress(MultiEmailID));
                    }
                }

                //For Multi CC Email ID
                string strCCEmailID = ccEmailId + ";" + fromMailId;
                string[] MultiCCMailId = strCCEmailID.Trim().Split(';');
                for (int i = 0; i < MultiCCMailId.Count(); i++)
                {
                    if (MultiCCMailId[i].Contains("@"))
                    {
                        msgMail.CC.Add(new MailAddress(MultiCCMailId[i]));
                    }

                }



                msgMail.From = new MailAddress(fromMailId);
                msgMail.Subject = "Reallocation of Folios – Intra UFC Transfer (Within UFC)";
                msgMail.Body = sb.ToString();
                msgMail.IsBodyHtml = true;
                SmtpClient sc = new SmtpClient(MailSmtp, 25);
                sc.Send(msgMail);
                CommonHelper.WriteLog("Mail Send successfully..");

            }
            catch (Exception ex)
            {
                CommonHelper.WriteLog("SendMail_SameUFC(): Some error in sending mail.." + ex.Message);
            }

        }


    }
}
