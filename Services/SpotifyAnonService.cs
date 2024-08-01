using ActualPlaylistBuilder.Components.Shared;
using ActualPlaylistBuilder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ActualPlaylistBuilder.Services
{
    public interface ISpotifyAppCredentials
    {
        string GetHeaderValue();
    }

    public class SpotifyAppCredentials : ISpotifyAppCredentials
    {
        public string GetHeaderValue()
        {
            //TODO Do this better
            string ClientId = "";
            string ClientSecret = "";
            return ClientId + ":" + ClientSecret;
        }
    }

    public interface ISpotifyAnonService
    {
        Task<string> GetAuthToken();
    }
    public class SpotifyAnonService : ISpotifyAnonService
    {
        private readonly ISpotifyAppCredentials _credentials;
        public SpotifyAnonService(ISpotifyAppCredentials spotifyAppCredentials)
        {
            _credentials = spotifyAppCredentials;
        }
        public async Task<string> GetAuthToken()
        {
            

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _credentials.GetHeaderValue());
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.PostAsJsonAsync("https://accounts.spotify.com/api/token", new GrantType());

                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            return string.Empty;
        }
    }
}
