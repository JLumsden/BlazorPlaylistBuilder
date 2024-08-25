using ActualPlaylistBuilder.Models;
using ActualPlaylistBuilder.Models.TrackSearch;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Web;
using System.Net.Http;

namespace ActualPlaylistBuilder.Services
{
    public interface ISpotifySearchService
    {
        Task<SongSearch> GetTrack(string searchTerm);
    }
    public class SpotifySearchService : ISpotifySearchService
    {
        private readonly ISpotifyAnonAuthService _anonAuthService;
        private HttpClient httpClient; 

        public SpotifySearchService(ISpotifyAnonAuthService spotifyAnonAuthService)
        {
            _anonAuthService = spotifyAnonAuthService;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _anonAuthService.GetAnonToken().Result);
        }

        public async Task<SongSearch> GetTrack(string searchTerm)
        {
            try
            {
                var url = string.Format("https://api.spotify.com/v1/search?q={0}&type=track&limit=50&market=US", HttpUtility.UrlEncode(searchTerm));

                var response = await httpClient.GetFromJsonAsync<SongSearch>(url);

                
                
                

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
