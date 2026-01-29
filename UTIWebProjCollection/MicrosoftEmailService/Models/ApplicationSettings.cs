using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicrosoftEmailService.Models
{
    public class ApplicationSettings
    {
        public string application_code { get; set; }

        public string application_name { get; set; }

        public string tenant_id { get; set; }

        public string client_id { get; set; }

        public string client_secret { get; set; }

        public string user_object_id { get; set; }
    }
}