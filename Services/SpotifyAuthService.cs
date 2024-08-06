﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

//https://localhost:7147/token?code=AQD6HpNv2xqtdb-X0BKp2M6wYEZEDHpfiZIxvfNol3vgFjRaeBvqvfupbNwdZ7zGMG3NNb9TxqGH87nwPPKiTEJ67qPmd_sWI8oQ6Baeh-ao3rHtulmUPsOMQL3sP8YzIO9Cr0bHiLqIkN8XnS53HVfnnify6xWTT22CNyyVILPvTBXI5DEyq--mGDV08N4ygwDKuTkrec0-kEQYqv4NCSyy5OdR-IZfnZC171apipBRJObu9JG-3ZUuhjeEP2QW3wihZj7GfNJjgzzs

namespace ActualPlaylistBuilder.Services
{
    public interface ISpotifyAuthService
    {
        void StartAuth();
        Task<string> CreatePlaylist(string code);
    }
    public class SpotifyAuthService : ISpotifyAuthService
    {
        private readonly ISpotifyAppCredentials _credentials;
        private readonly NavigationManager _navigationManager;
        private readonly string CodeChallenge;
        private readonly string HashedChallenge;
        private HttpClient httpClient { get; set; }
        public SpotifyAuthService(NavigationManager navigationManager, ISpotifyAppCredentials spotifyAppCredentials)
        {
            _credentials = spotifyAppCredentials;
            _navigationManager = navigationManager;
            CodeChallenge = GenerateNonce();
            HashedChallenge = QuickHash(CodeChallenge);
            httpClient = new HttpClient();
        }

        private string GenerateNonce()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz123456789";
            var random = new Random();
            var nonce = new char[128];
            for (int i = 0; i < nonce.Length; i++)
            {
                nonce[i] = chars[random.Next(chars.Length)];
            }

            return new string(nonce);
        }

        string QuickHash(string codeVerifier)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
            var b64Hash = Convert.ToBase64String(hash);
            var code = Regex.Replace(b64Hash, "\\+", "-");
            code = Regex.Replace(code, "\\/", "_");
            code = Regex.Replace(code, "=+$", "");
            return code;
        }

        public void StartAuth()
        {
            var queryString = HttpUtility.ParseQueryString("");
            queryString.Add("client_id", _credentials.GetClientId());
            queryString.Add("response_type", "code");
            queryString.Add("redirect_uri", "https://localhost:7147/token");
            queryString.Add("scope", "playlist-modify-public");
            queryString.Add("code_challenge_method", "S256");
            queryString.Add("code_challenge", HashedChallenge);
            

            _navigationManager.NavigateTo($"https://accounts.spotify.com/authorize?{queryString}");
        }

        public async Task<string> CreatePlaylist(string code)
        {
            await GetAccessToken(code);
            return string.Empty;
        }
        private async Task GetAccessToken(string code)
        {
            //httpClient.DefaultRequestHeaders.Accept.Clear();
            //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_credentials.GetHeaderValue())));

            httpClient = new HttpClient();

            List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();

            requestData.Add(new KeyValuePair<string, string>("client_id", _credentials.GetClientId()));
            requestData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
            requestData.Add(new KeyValuePair<string, string>("code", code));
            requestData.Add(new KeyValuePair<string, string>("redirect_uri", "https://localhost:7147/token"));

            requestData.Add(new KeyValuePair<string, string>("code_verifier", CodeChallenge));

            var parameters = new Dictionary<string, string>
            {
                {"client_id", _credentials.GetClientId()},
                {"grant_type", "authorization_code"},
                {"code", code},
                {"redirect_uri", "https://localhost:7147"},
                {"code_verifier", CodeChallenge}
            };

            FormUrlEncodedContent requestBody = new FormUrlEncodedContent(parameters);
            var req = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token") { Content = requestBody };
            var response = await httpClient.SendAsync(req);

            response.EnsureSuccessStatusCode();
        }
    }
}
