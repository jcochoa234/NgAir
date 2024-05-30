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
    public class CategoriesRepository(DataContext context) : GenericRepository<Category>(context), ICategoriesRepository
    {
        private readonly DataContext _context = context;

        public override async Task<ActionResponse<IEnumerable<Category>>> GetAsync(RequestParams requestParams)
        {
            var queryable = _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(requestParams.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(requestParams.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Category>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderByDynamic(requestParams)
                    .Paginate(requestParams)
                    .ToListAsync()
            };
        }

        public async Task<IEnumerable<Category>> GetComboAsync()
        {
            return await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public override Task<ActionResponse<PagingResponse<Category>>> GetPagedAsync(RequestParams requestParams)
        {
            Expression<Func<Category, bool>>? filters = null;

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
                var customFilter = CustomExpressionFilter<Category>.CustomFilter(columnFilters, "category");
                filters = customFilter;
            }

            var queryable = _context.Categories.AsQueryable().FilterDynamic(filters!);

            var page = PagedList<Category>.ToPagedList(queryable.OrderByDynamic(requestParams), requestParams);

            var pagingResponse = new PagingResponse<Category>
            {
                Current = page.MetaData.Current,
                PageSize = page.MetaData.PageSize,
                Total = page.MetaData.Total,
                TotalPages = page.MetaData.TotalPages,
                Items = page.ToList(),
            };

            return Task.FromResult(new ActionResponse<PagingResponse<Category>>
            {
                WasSuccess = true,
                Result = pagingResponse
            });
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams)
        {
            var queryable = _context.Categories.AsQueryable();

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
