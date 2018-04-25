using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TwitterBackup.Providers
{
    public class TwitterFacadeProvider
    {
        private string accessToken;

        public TwitterFacadeProvider(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public async Task<string> RetrieveTwitterAccountAsync(string screenName)
        {
            throw new NotImplementedException();
        }

        private async Task<string> RetrieveTwitterStatusesByAccountAsync(string screenName)
        {
            throw new NotImplementedException();
        }
    }
}
