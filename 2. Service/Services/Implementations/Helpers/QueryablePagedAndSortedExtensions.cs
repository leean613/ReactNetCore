using Common.Extentions;
using DTOs.Share;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Helpers
{
    public static class QueryablePagedAndSortedExtensions
    {
        public static async Task<IPagedResultDto<TEntityDto>> GetPagedResultAsync<TProjection, TEntityDto>(
            this IQueryable<TProjection> source,
            int pageIndex,
            int pageSize,
            Func<TProjection, TEntityDto> convert)
        {
            var count = await source.CountAsync().ConfigureAwait(false);

            var items = source
                .PageBy((pageIndex - 1) * pageSize, pageSize)
                .MakeQueryToDatabase();

            return new PagedResultDto<TEntityDto>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = count,
                Items = items.ConvertArray(convert),
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
        }
    }
}
