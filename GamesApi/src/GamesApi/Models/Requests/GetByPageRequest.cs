using GamesApi.Common.Enums;

namespace GamesApi.Models.Requests
{
    public class GetByPageRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public SortedTypeEnum SortedType { get; set; }
    }
}