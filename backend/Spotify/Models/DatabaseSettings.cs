namespace Spotify.Models
{
    public class DatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        
        public string SongCategoriesCollectionName { get; set; }

        public string SongsCollectionName { get; set; }
        
        public string ConnectionString { get; set; }
        
        public string DatabaseName { get; set; }
        
        public string ArtistsCollectionName { get; set; }
    }
}