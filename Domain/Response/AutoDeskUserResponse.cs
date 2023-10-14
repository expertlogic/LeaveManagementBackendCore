using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Response
{
    
    public class AutoDeskUserResponse
    {
        public string sub { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string preferred_username { get; set; }
        public string email { get; set; }
        public bool email_verified { get; set; }
        public string profile { get; set; }
        public string picture { get; set; }
        public string locale { get; set; }
        public int updated_at { get; set; }
    }

    public class AutoDeskAccessTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
    }
}
