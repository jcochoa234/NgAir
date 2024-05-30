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
    public class CountriesRepository(DataContext context) : GenericRepository<Country>(context), ICountriesRepository
    {
        private readonly DataContext _context = context;

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
        {
            var countries = await _context.Countries
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Country>>
            {
                WasSuccess = true,
                Result = countries
            };
        }

        public override async Task<ActionResponse<Country>> GetAsync(int id)
        {
            var country = await _context.Countries
                 .Include(c => c.States!)
                 .ThenInclude(s => s.Cities)
                 .FirstOrDefaultAsync(c => c.Id == id);

            if (country == null)
            {
                return new ActionResponse<Country>
                {
                    WasSuccess = false,
                    Message = "País no existe"
                };
            }

            return new ActionResponse<Country>
            {
                WasSuccess = true,
                Result = country
            };
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(RequestParams requestParams)
        {
            var queryable = _context.Countries
                .Include(c => c.States)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(requestParams.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(requestParams.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Country>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(requestParams)
                    .ToListAsync()
            };
        }

        public async Task<IEnumerable<Country>> GetComboAsync()
        {
            return await _context.Countries
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public override Task<ActionResponse<PagingResponse<Country>>> GetPagedAsync(RequestParams requestParams)
        {
            Expression<Func<Country, bool>>? filters = null;

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
                var customFilter = CustomExpressionFilter<Country>.CustomFilter(columnFilters, "country");
                filters = customFilter;
            }

            var queryable = _context.Countries.AsQueryable().FilterDynamic(filters!);

            var page = PagedList<Country>.ToPagedList(queryable.OrderByDynamic(requestParams), requestParams);

            var pagingResponse = new PagingResponse<Country>
            {
                Current = page.MetaData.Current,
                PageSize = page.MetaData.PageSize,
                Total = page.MetaData.Total,
                TotalPages = page.MetaData.TotalPages,
                Items = page.ToList(),
            };

            return Task.FromResult(new ActionResponse<PagingResponse<Country>>
            {
                WasSuccess = true,
                Result = pagingResponse
            });
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams)
        {
            var queryable = _context.Countries.AsQueryable();

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
