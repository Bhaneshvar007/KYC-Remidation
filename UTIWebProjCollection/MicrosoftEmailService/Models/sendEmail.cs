using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Azure.Identity;
using Microsoft.Graph;

namespace MicrosoftEmailService.Models
{
    public class sendEmail
    {
        public static bool SendEmail()
        {
            try
            {
                var credentials = new ClientSecretCredential(
                                        "3218bdc1-7b64-41b5-ad83-da195299e285",
                                        "2fbca1fc-bb6a-47fe-bd09-86eacc518d50",
                                        "q4e8Q~8UMU4~NkMXXwilkR~ujWP-fBlU6RvTxdq0",
                                  new TokenCredentialOptions { AuthorityHost = AzureAuthorityHosts.AzurePublicCloud });

                GraphServiceClient graphServiceClient = new GraphServiceClient(credentials);

                var subject = $"Sent for demo at {DateTime.Now.ToString("s")}";
                var body = "This is the body of the message. Lots of fun things can go in here";

                // Define a simple e-mail message.
                var message = new Message
                {
                    Subject = subject,
                    Body = new ItemBody
                    {
                        ContentType = BodyType.Html,
                        Content = body
                    },
                    ToRecipients = new List<Recipient>()
    {
        new Recipient { EmailAddress = new EmailAddress { Address = "vikrantthakur151@gmail.com" }},
        new Recipient { EmailAddress = new EmailAddress { Address = "Pawas.Goyal@cylsys.com" }},
        new Recipient { EmailAddress = new EmailAddress { Address = "Ghanshyam.Vishwakarma@cylsys.com" }},
        new Recipient { EmailAddress = new EmailAddress { Address = "ankita.goyal@cylsys.com" }}
    }
                };

                // Send mail as the given user. 
                graphServiceClient
                    .Users["2db71d4d-1100-4a59-8c3c-81dc5e5680c2"]
                    .SendMail(message, true)
                    .Request()
                    .PostAsync().Wait();

                return true;
            }
            catch (System.Exception ex)
            {

                return false;
            }

        }

     
    }
}