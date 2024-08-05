using ActualPlaylistBuilder.Components.Shared;
using ActualPlaylistBuilder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ActualPlaylistBuilder.Services
{
    public interface ISpotifyAppCredentials
    {
        string GetHeaderValue();
    }

    public class SpotifyAppCredentials : ISpotifyAppCredentials
    {
        private readonly IConfiguration _configuration;
        public SpotifyAppCredentials(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetHeaderValue()
        {
            //TODO Do this better
            string ClientId = _configuration["client_id"];
            string ClientSecret = _configuration["client_secret"];
            return string.Format("{0}:{1}", ClientId, ClientSecret);
        }
    }

    public interface ISpotifyAnonAuthService
    {
        Task<string> GetAnonToken();
    }
    public class SpotifyAnonAuthService : ISpotifyAnonAuthService
    {
        private readonly ISpotifyAppCredentials _credentials;
        private AnonToken? _token = null;
        private DateTime? TokenExpire = null;
        public SpotifyAnonAuthService(ISpotifyAppCredentials spotifyAppCredentials)
        {
            _credentials = spotifyAppCredentials;
        }
        public async Task<string> GetAnonToken()
        {
            string retVal = string.Empty;
            if (_token is not null && TokenExpire.HasValue && TokenExpire.Value > DateTime.Now)
            {
                retVal = _token.access_token;
            }
            else
            {
                await SetAnonToken();
                retVal = GetAnonToken().Result;
            }
            return retVal;
        }

        private async Task SetAnonToken()
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_credentials.GetHeaderValue())));

                List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

                var response = httpClient.PostAsync("https://accounts.spotify.com/api/token", requestBody).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                AnonToken? tempToken = JsonConvert.DeserializeObject<AnonToken>(responseContent);
                if (tempToken != null)
                {
                    _token = tempToken;
                    TokenExpire = DateTime.Now.AddSeconds(_token.expires_in - 30);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
