namespace ParkingLotAPP.Domain.SeedWork.SearchableRepository
{
    public class SearchOutput<TEntity> where TEntity : Entity
    {
        public SearchOutput(int currentPage, int perPage, int total, IReadOnlyList<TEntity> items)
        {
            CurrentPage = currentPage;
            PerPage = perPage;
            Items = items;
            Total = total;
        }

        public int CurrentPage { get; set; }

        public int PerPage { get; set; }

        public int Total { get; set; }

        public IReadOnlyList<TEntity> Items { get; set; }
    }
}
