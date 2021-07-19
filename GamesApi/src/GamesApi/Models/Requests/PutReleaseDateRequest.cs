using System;

namespace GamesApi.Models.Requests
{
    public class PutReleaseDateRequest
    {
        public string Id { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}