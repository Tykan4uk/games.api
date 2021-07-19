using System.Collections.Generic;

namespace GamesApi.Models.Responses
{
    public class GetByPageResponse
    {
        public IEnumerable<GameModel> Games { get; set; }
        public int TotalRecords { get; set; }
    }
}