using Microsoft.EntityFrameworkCore;
using NgAir.BackEnd.Data;
using NgAir.BackEnd.Helpers;
using NgAir.BackEnd.Paging;
using NgAir.BackEnd.Repositories.Interfaces;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;
using System.Linq.Expressions;
using System.Text.Json;

namespace NgAir.BackEnd.Repositories.Implementations
{
    public class CitiesRepository(DataContext context) : GenericRepository<City>(context), ICitiesRepository
    {
        private readonly DataContext _context = context;

        public override async Task<ActionResponse<IEnumerable<City>>> GetAsync(RequestParams requestParams)
        {
            var queryable = _context.Cities
                .Where(x => x.State!.Id == requestParams.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(requestParams.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(requestParams.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<City>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(requestParams)
                    .ToListAsync()
            };
        }

        public async Task<IEnumerable<City>> GetComboAsync(int stateId)
        {
            return await _context.Cities
                .Where(c => c.StateId == stateId)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public override Task<ActionResponse<PagingResponse<City>>> GetPagedAsync(RequestParams requestParams)
        {
            Expression<Func<City, bool>>? filters = null;

            List<ColumnFilter> columnFilters = [];
            if (!String.IsNullOrEmpty(requestParams.ColumnFilters))
            {
                try
                {
                    columnFilters.AddRange(JsonSerializer.Deserialize<List<ColumnFilter>>(requestParams.ColumnFilters)!);
                }
                catch (Exception)
                {
                    columnFilters = [];
                }
            }
            if (columnFilters.Count > 0)
            {
                var customFilter = CustomExpressionFilter<City>.CustomFilter(columnFilters, "city");
                filters = customFilter;
            }

            var queryable = _context.Cities
                .Where(c => c.StateId == requestParams.Id)
                .AsQueryable()
                .FilterDynamic(filters!);

            var page = PagedList<City>.ToPagedList(queryable.OrderByDynamic(requestParams), requestParams);

            var pagingResponse = new PagingResponse<City>
            {
                Current = page.MetaData.Current,
                PageSize = page.MetaData.PageSize,
                Total = page.MetaData.Total,
                TotalPages = page.MetaData.TotalPages,
                Items = page.ToList(),
            };

            return Task.FromResult(new ActionResponse<PagingResponse<City>>
            {
                WasSuccess = true,
                Result = pagingResponse
            });
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams)
        {
            var queryable = _context.Cities
                .Where(x => x.State!.Id == requestParams.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(requestParams.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(requestParams.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / requestParams.PageSize);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

    }
}
