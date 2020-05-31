using System;
using System.Net.Cache;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankOfDotNet.ConsoleClient
{
    class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();
        private static async Task MainAsync()
        {
            //Discouver all the endpoints using metadata of identity server
            var client = new HttpClient();

            var discoRO = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (discoRO.IsError)
            {
                Console.WriteLine(discoRO.Error);
                return;
            }

            //Grab a bearer token

            //var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            //var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");
            var responseRO = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                ClientId = "ro.client",
                Address = discoRO.TokenEndpoint,
                ClientSecret = "xxxxxx",
                UserName = "MhSaleem2",
                Password = "S@leem1982"
            });
            if (responseRO.IsError)
            {
                Console.WriteLine(responseRO.Error);
                return;
            }

            //var disco = IdentityModel.Client.HttpClientDiscoveryExtensions.GetDiscoveryDocumentAsync(""); //.DiscoveryEndpoint.ParseUrl("http://localhost:5000");
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            //Grab a bearer token

            //var tokenClient = new IdentityModel.Client.TokenClient(disco.TokenEndpoint,);

            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint, //"https://demo.identityserver.io/connect/token",
                ClientId = "client",
                ClientSecret = "secrect"
            });

            if (response.IsError)
            {
                Console.WriteLine(response.Error);
                return;
            }

            Console.WriteLine(responseRO.Json);
            Console.WriteLine("\n\n");

            Console.WriteLine(response.Json);
            Console.WriteLine("\n\n");
            //Consume our Cusotmer API
            client.SetBearerToken(responseRO.AccessToken);
            var customerInfo = new StringContent(JsonConvert.SerializeObject(
                new { FirstName = "Test2", LastName = "Test2" }
                ), Encoding.UTF8, "application/json");

            var createCustomerResponse = await client.PostAsync("http://localhost:56820/api/customers/", customerInfo);
            if (!createCustomerResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(createCustomerResponse.StatusCode);
            }

            var getCusotmerResponse = await client.GetAsync("http://localhost:56820/api/customers/");
            if (!getCusotmerResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(getCusotmerResponse.StatusCode);
            }
            else
            {
                var content = getCusotmerResponse.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content.Result));

            }
            Console.Read();
        }

    }
}
