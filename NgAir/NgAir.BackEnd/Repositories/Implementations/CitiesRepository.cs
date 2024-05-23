using Microsoft.EntityFrameworkCore;
using NgAir.BackEnd.Data;
using NgAir.BackEnd.Helpers;
using NgAir.BackEnd.Paging;
using NgAir.BackEnd.Repositories.Interfaces;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.Repositories.Implementations
{
    public class CitiesRepository : GenericRepository<City>, ICitiesRepository
    {
        private readonly DataContext _context;

        public CitiesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Cities
                .Where(x => x.State!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<City>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
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

        public async Task<ActionResponse<PagingResponse<City>>> GetPagedAsync(PaginationDTO pagination)
        {
            var cities = await _context.Cities.ToListAsync();
            var page = PagedList<City>.ToPagedList(cities, pagination);

            var pagingResponse = new PagingResponse<City>
            {
                Current = page.MetaData.Current,
                PageSize = page.MetaData.PageSize,
                Total = page.MetaData.Total,
                TotalPages = page.MetaData.TotalPages,
                Items = page.ToList(),
            };

            return new ActionResponse<PagingResponse<City>>
            {
                WasSuccess = true,
                Result = pagingResponse
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Cities
                .Where(x => x.State!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.PageSize);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

    }
}
