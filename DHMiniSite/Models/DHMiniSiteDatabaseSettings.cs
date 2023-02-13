namespace DHMiniSite.Models
{
    public class DHMiniSiteDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string PostsCollectionName { get; set; } = null!;
        public string CommentsCollectionName { get; set; } = null!;
    }
}
