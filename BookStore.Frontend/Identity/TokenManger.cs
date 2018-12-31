using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStore.Frontend.Identity
{
    public class TokenManger
    {
        public static async Task<string> GetToken() {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:8888");

            if (disco.IsError)
                throw new Exception(disco.Error);

            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest {
                Address = disco.TokenEndpoint,
                ClientId = "frontend",
                ClientSecret = "123456",
                Scope = "api1"
            });

            if (response.IsError)
                throw new Exception(response.Error);

            return response.AccessToken;
        }
    }
}
