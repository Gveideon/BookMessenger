namespace BookMessenger.Models
{
    public class BookSortViewModel
    {
        public SortBookState NameSort { get; set; }
        public SortBookState LikeNameSort { get; set; }
        public SortBookState DislikeNameSort { get; set; }
        public SortBookState AddingSortNameSort { get; set; }
        public SortBookState Current { get; set; }
        public BookSortViewModel(SortBookState state)
        {
            NameSort = state == SortBookState.NameAsc ? SortBookState.NameDesc : SortBookState.NameAsc;
            LikeNameSort = state == SortBookState.LikeAsc ? SortBookState.LikeDesc : SortBookState.LikeAsc;
            DislikeNameSort = state == SortBookState.DislikeAsc ? SortBookState.DislikeDesc : SortBookState.DislikeAsc;
            AddingSortNameSort = state == SortBookState.AddingAsc ? SortBookState.AddingDesc : SortBookState.AddingAsc;
            Current = state;
        }

    }
}
