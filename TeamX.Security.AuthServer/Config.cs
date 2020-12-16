using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TeamX.Security.AuthServer
{
    public class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            //client credentials
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // Client Secrets
                    ClientSecrets =
                    {
                        new Secret ("secret".Sha256())
                    },
                    AllowedScopes = {"api1"}
                },
        };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }
    }
}
