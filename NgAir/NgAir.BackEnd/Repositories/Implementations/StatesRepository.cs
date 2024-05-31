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
    public class StatesRepository(DataContext context) : GenericRepository<State>(context), IStatesRepository
    {
        private readonly DataContext _context = context;

        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync()
        {
            var states = await _context.States
                .OrderBy(x => x.Name)
                .Include(s => s.Cities)
                .ToListAsync();
            return new ActionResponse<IEnumerable<State>>
            {
                WasSuccess = true,
                Result = states
            };
        }

        public override async Task<ActionResponse<State>> GetAsync(int id)
        {
            var state = await _context.States
                 .Include(s => s.Cities)
                 .FirstOrDefaultAsync(s => s.Id == id);

            if (state == null)
            {
                return new ActionResponse<State>
                {
                    WasSuccess = false,
                    Message = "Estado no existe"
                };
            }

            return new ActionResponse<State>
            {
                WasSuccess = true,
                Result = state
            };
        }

        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync(RequestParams requestParams)
        {
            var queryable = _context.States
                .Include(x => x.Cities)
                .Where(x => x.Country!.Id == requestParams.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(requestParams.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(requestParams.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<State>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(requestParams)
                    .ToListAsync()
            };
        }

        public async Task<IEnumerable<State>> GetComboAsync(int countryId)
        {
            return await _context.States
                .Where(s => s.CountryId == countryId)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public override Task<ActionResponse<PagingResponse<State>>> GetPagedAsync(RequestParams requestParams)
        {
            Expression<Func<State, bool>>? filters = null;

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
                var customFilter = CustomExpressionFilter<State>.CustomFilter(columnFilters, "state");
                filters = customFilter;
            }

            var queryable = _context.States
                .Include(x => x.Cities)
                .AsQueryable()
                .FilterDynamic(filters!);

            var page = PagedList<State>.ToPagedList(queryable.OrderByDynamic(requestParams), requestParams);

            var pagingResponse = new PagingResponse<State>
            {
                Current = page.MetaData.Current,
                PageSize = page.MetaData.PageSize,
                Total = page.MetaData.Total,
                TotalPages = page.MetaData.TotalPages,
                Items = page.ToList(),
            };

            return Task.FromResult(new ActionResponse<PagingResponse<State>>
            {
                WasSuccess = true,
                Result = pagingResponse
            });
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(RequestParams requestParams)
        {
            var queryable = _context.States
                .Where(x => x.Country!.Id == requestParams.Id)
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
