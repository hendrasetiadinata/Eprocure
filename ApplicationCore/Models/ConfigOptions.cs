using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ConfigOptions
    {
        public string DefaultConnectionString { get; set; }
        public string ApiSecretKey { get; set; }

        public JwtOptions JwtOptions { get; set; }
    }

    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Expires { get; set; }
    }

}
