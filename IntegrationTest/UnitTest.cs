using ApplicationCore.Models;
using ApplicationCore.Utility;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{
    public class UnitTest
    {
        [Fact]
        public async Task Request_Token_Has_Result()
        {
            using var client = new HttpClient()
            {
                Timeout = TimeSpan.FromMinutes(30)
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var body = new AuthenticateRequest()
            {
                Username = "testing@yahoo.com",
                Password = "abcd@jkj"
            };

            var response = await client.PostAsync("https://localhost:5001/api/Authenticate",
                new StringContent(Serializer.Serialize(body), Encoding.UTF8, "application/json"));


            Assert.NotNull(response.Content);
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);

            var str = await response.Content.ReadAsStringAsync();

            Assert.True(!string.IsNullOrEmpty(str));
            Assert.True(!string.IsNullOrEmpty(Serializer.DeserializeToObj<AuthenticateResponse>(str).Token));
        }
    }
}
