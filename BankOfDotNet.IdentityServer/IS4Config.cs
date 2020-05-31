using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace BankOfDotNet.IdentityServer
{
    public class IS4Config
    {
        public static IEnumerable<IdentityResource> IdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser()
                {
                    SubjectId = "1",
                    Username = "MhSaleem",
                    Password = "S@leem1983"

                },
                new TestUser()
                {
                    SubjectId = "2",
                    Username = "MhSaleem2",
                    Password = "S@leem1983"

                }
            };
        }
        public static IEnumerable<ApiResource> GetAllApiResources()
        {
            return new List<ApiResource>() { 
                new ApiResource("bankOfDotNetApi", "Customer API for Bank Of Dot Net")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("secrect".Sha256())},
                    AllowedScopes = { "bankOfDotNetApi" }
                },
                //Resource Owner Password Grant Type
                new Client()
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = {new Secret("xxxxxx".Sha256())},
                    AllowedScopes = { "bankOfDotNetApi" }
                },
                //Implicit Flow grant type
                new Client()
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = {"http://localhost:5001/signin-oidc"},
                    PostLogoutRedirectUris = {"http://localhost:5001/signout-callback-oidc"},
                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }

            };


        }
    }
}
