using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Identity.Models
{
    public class UserLoginInfo
    {
        public UserLoginInfo(string loginProvider, string providerKey, string providerDisplayName)
        {
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
            ProviderDisplayName = providerDisplayName;
        }

        public string LoginProvider { get; }
        public string ProviderKey { get; }
        public string ProviderDisplayName { get; }
    }
    public class UserLoginInfoMutable
    {
        public UserLoginInfoMutable() { }
        public UserLoginInfoMutable(string loginProvider, string providerKey, string providerDisplayName)
        {
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
            ProviderDisplayName = providerDisplayName;
        }

        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
    }
}
