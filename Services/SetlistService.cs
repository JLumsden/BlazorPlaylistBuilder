namespace ActualPlaylistBuilder.Services
{
    public interface ISetlistService
    {
        void SetSearchList(List<string> searchList);
        List<string> GetSearchList();
    }
    public class SetlistService : ISetlistService
    {
        private List<string> SearchList { get; set; } = new();

        public void SetSearchList(List<string> searchList)
        {
            SearchList = searchList;
        }
        public List<string> GetSearchList()
        {
            return SearchList;
        }
    }
}
