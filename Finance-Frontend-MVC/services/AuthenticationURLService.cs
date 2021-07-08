using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance_Frontend_MVC.services
{
    public class AuthenticationURLService
    {
        private string authURL;
        public AuthenticationURLService(string urlString) {
            this.authURL = urlString;
        }
        public string GetAuthURL()
        {
            return authURL;
        }
    }
}
