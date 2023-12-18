namespace BookMessenger.Models
{
    public enum SortBookState
    {
        NameAsc,    
        NameDesc,   
        AuthorNameAsc,    
        AuthorNameDesc,   
        LikeAsc, 
        LikeDesc,    
        DislikeAsc, 
        DislikeDesc,    
        AddingAsc, 
        AddingDesc 
    }
    public class FilterBookViewModel
    {
        public FilterBookViewModel(string bookName, string authorName)
        {
            SelectedBookName = bookName;
            SelectedAuthorName = authorName;
        }
        public string SelectedBookName { get; }
        public string SelectedAuthorName { get; }
    }
}
