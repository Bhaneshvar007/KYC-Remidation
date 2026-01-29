using MicrosoftEmailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicrosoftEmailService.Controllers
{
    public class EmailController : ApiController
    {

        [HttpPost]
        public bool SendEmail()
        {
            var result = sendEmail.SendEmail();
            return result;
        }

        [HttpPost]
        public Task<List<readEmailModel>> ReadEmail([FromBody] ParaModel model)
        {
            CommonHelper.WriteLog("email reading start" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            Task<List<readEmailModel>> task = readEmail.ReadEmail(model);
            CommonHelper.WriteLog("email reading end" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            return task;
        }

    }
}