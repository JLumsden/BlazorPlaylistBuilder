using ActualPlaylistBuilder.Models;

namespace ActualPlaylistBuilder.Services
{
    public interface ISetlistService
    {
        void SetData(List<string> searchList, PlaylistDetails playlist);
        List<string> GetSearchList();
        PlaylistDetails GetPlaylist();
    }
    public class SetlistService : ISetlistService
    {
        private List<string> SearchList { get; set; } = new();
        private PlaylistDetails Playlist { get; set; } = new();

        public void SetData(List<string> searchList, PlaylistDetails playlist)
        {
            SearchList = searchList;
            Playlist = playlist;
        }
        public List<string> GetSearchList()
        {
            return SearchList;
        }

        public PlaylistDetails GetPlaylist()
        {
            return Playlist;
        }
    }
}
