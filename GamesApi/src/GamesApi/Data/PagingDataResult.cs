using System.Collections.Generic;
using GamesApi.Data.Entities;

namespace GamesApi.Data
{
    public class PagingDataResult
    {
        public IEnumerable<GameEntity> Games { get; set; }
        public int TotalRecords { get; set; }
    }
}
