namespace GamesApi.Configuration
{
    public class Config
    {
        public GamesApiConfig GamesApi { get; set; } = null!;
        public RedisConfig Redis { get; set; } = null!;
    }
}