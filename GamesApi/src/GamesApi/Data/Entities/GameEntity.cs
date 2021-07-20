using System;

namespace GamesApi.Data.Entities
{
    public class GameEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
    }
}