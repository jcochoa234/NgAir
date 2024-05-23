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
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        private readonly DataContext _context;

        public CategoriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Category>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public async Task<IEnumerable<Category>> GetComboAsync()
        {
            return await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<ActionResponse<PagingResponse<Category>>> GetPagedAsync(PaginationDTO pagination)
        {
            var categories = await _context.Categories.ToListAsync();
            var page = PagedList<Category>.ToPagedList(categories, pagination);

            var pagingResponse = new PagingResponse<Category>
            {
                Current = page.MetaData.Current,
                PageSize = page.MetaData.PageSize,
                Total = page.MetaData.Total,
                TotalPages = page.MetaData.TotalPages,
                Items = page.ToList(),
            };

            return new ActionResponse<PagingResponse<Category>>
            {
                WasSuccess = true,
                Result = pagingResponse
            };
        }


        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Categories.AsQueryable();

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
