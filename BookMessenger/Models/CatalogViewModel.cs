namespace BookMessenger.Models
{
    public class CatalogViewModel
    {
        public List<Book> Books { get; set; }   
        public Book SelectedBook { get; set; }
        public UserProfile UserProfile { get; set; }
        public FilterBookViewModel FilterBookViewModel { get; set; }
        public BookSortViewModel SortViewModel { get; set; }
        public CatalogPageViewModel CatalogPageViewModel { get; set; }
        public CatalogViewModel(List<Book> books,
            Book book,
            UserProfile userProfile,
            FilterBookViewModel filterBook,
            BookSortViewModel bookSort,
            CatalogPageViewModel catalogPage)
        {
            Books = books;
            SelectedBook = book;
            UserProfile = userProfile;
            FilterBookViewModel = filterBook;
            SortViewModel = bookSort;
            CatalogPageViewModel = catalogPage;
        }
    }
}
