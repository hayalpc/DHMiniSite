
namespace DHCore.Models
{
    public class DataList<TEntity>
    {
        public List<TEntity> Data { get; set; }
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public long TotalCount { get; set; }
    }
}
