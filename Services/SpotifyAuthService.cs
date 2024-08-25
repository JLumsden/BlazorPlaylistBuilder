using ActualPlaylistBuilder.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web;

namespace ActualPlaylistBuilder.Services
{
    public interface ISpotifyAuthService
    {
        void SetCode(string code);
        void StartAuth();
        Task<string> OrchestratePlaylist(PlaylistDetails playlistDetails, List<string> songUris);
    }
    public class SpotifyAuthService : ISpotifyAuthService
    {
        private readonly ISpotifyAppCredentials _credentials;
        private readonly NavigationManager _navigationManager;
        private HttpClient httpClient { get; set; }
        private string code { get; set; } = string.Empty;
        private string playlistId { get; set; } = string.Empty;
        private AuthToken? _token = null;
        private SpotifyUser? spotifyUser { get; set; }
        private CreatedPlaylist? createdPlaylist { get; set; }
        public SpotifyAuthService(NavigationManager navigationManager, ISpotifyAppCredentials spotifyAppCredentials)
        {
            _credentials = spotifyAppCredentials;
            _navigationManager = navigationManager;
            httpClient = new HttpClient();
        }

        public void SetCode(string code)
        {
            this.code = code;
        }

        public void StartAuth()
        {
            var queryString = HttpUtility.ParseQueryString("");
            queryString.Add("client_id", _credentials.GetClientId());
            queryString.Add("response_type", "code");
            queryString.Add("redirect_uri", "https://localhost:7147/token");
            queryString.Add("scope", "playlist-modify-public");
            
            _navigationManager.NavigateTo($"https://accounts.spotify.com/authorize?{queryString}");
        }

        public async Task<string> OrchestratePlaylist(PlaylistDetails playlistDetails, List<string> songUris)
        {
            _token = await GetAccessToken(code);
            if(_token is not null) spotifyUser = await GetSpotifyUser();
            if(spotifyUser is not null) createdPlaylist = await CreatePlaylist(spotifyUser.Id, playlistDetails);
            if(createdPlaylist is not null) await PopulatePlaylist(createdPlaylist.Id, songUris);
            return createdPlaylist.Uri;
        }
        private async Task<AuthToken> GetAccessToken(string code)
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_credentials.GetHeaderValue())));

            List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();

            requestData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
            requestData.Add(new KeyValuePair<string, string>("code", code));
            requestData.Add(new KeyValuePair<string, string>("redirect_uri", "https://localhost:7147/token"));

            using var client = new HttpClient();
            using var req = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token") { Content = new FormUrlEncodedContent(requestData) };
            using var res = await httpClient.PostAsync("https://accounts.spotify.com/api/token", new FormUrlEncodedContent(requestData));
            var responseContent = await res.Content.ReadAsStringAsync();
            AuthToken? tempToken = JsonConvert.DeserializeObject<AuthToken>(responseContent);
            if (tempToken != null)
            {
                return tempToken;
            }
            return null;
        }

        private async Task<SpotifyUser> GetSpotifyUser()
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.access_token);

                var response = await httpClient.GetAsync("https://api.spotify.com/v1/me");
                var content = await response.Content.ReadAsStringAsync();
                SpotifyUser? tempUser = JsonConvert.DeserializeObject<SpotifyUser>(content);

                if(response is not null && !string.IsNullOrEmpty(tempUser.Id))
                {
                    return tempUser;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        private async Task<CreatedPlaylist> CreatePlaylist(string userId, PlaylistDetails playlistDetails)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.access_token);

                var response = await httpClient.PostAsJsonAsync($"https://api.spotify.com/v1/users/{userId}/playlists", playlistDetails);

                var responseContent = await response.Content.ReadAsStringAsync();
                CreatedPlaylist? tempPlaylist = JsonConvert.DeserializeObject<CreatedPlaylist>(responseContent);

                return tempPlaylist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        private async Task<bool> PopulatePlaylist(string playlistId, List<string> songUris)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.access_token);

                PlaylistUris playlistUris = new PlaylistUris
                {
                    uris = songUris
                };

                var response = await httpClient.PostAsJsonAsync($"https://api.spotify.com/v1/playlists/{playlistId}/tracks", playlistUris);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
