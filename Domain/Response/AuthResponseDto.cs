using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Response
{
    public class AuthenticationResponse
    {
        public bool IsAuthSuccessful { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string  ErrorMessage { get; set; }
        public string  Token { get; set; }

        public string UserRole { get; set; }
    }
}
