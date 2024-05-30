using Microsoft.EntityFrameworkCore;
using NgAir.BackEnd.Data;
using NgAir.BackEnd.Repositories.Interfaces;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.Repositories.Implementations
{
    public class TemporalOrdersRepository(DataContext context, IUsersRepository usersRepository) : GenericRepository<TemporalOrder>(context), ITemporalOrdersRepository
    {
        private readonly DataContext _context = context;
        private readonly IUsersRepository _usersRepository = usersRepository;

        public async Task<ActionResponse<TemporalOrderDto>> AddFullAsync(string email, TemporalOrderDto temporalOrderDto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == temporalOrderDto.ProductId);
            if (product == null)
            {
                return new ActionResponse<TemporalOrderDto>
                {
                    WasSuccess = false,
                    Message = "Producto no existe"
                };
            }

            var user = await _usersRepository.GetUserAsync(email);
            if (user == null)
            {
                return new ActionResponse<TemporalOrderDto>
                {
                    WasSuccess = false,
                    Message = "Usuario no existe"
                };
            }

            var temporalOrder = new TemporalOrder
            {
                Product = product,
                Quantity = temporalOrderDto.Quantity,
                Remarks = temporalOrderDto.Remarks,
                User = user
            };

            try
            {
                _context.Add(temporalOrder);
                await _context.SaveChangesAsync();
                return new ActionResponse<TemporalOrderDto>
                {
                    WasSuccess = true,
                    Result = temporalOrderDto
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<TemporalOrderDto>
                {
                    WasSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ActionResponse<IEnumerable<TemporalOrder>>> GetAsync(string email)
        {
            var temporalOrders = await _context.TemporalOrders
                .Include(to => to.User!)
                .Include(to => to.Product!)
                .ThenInclude(p => p.ProductCategories!)
                .ThenInclude(pc => pc.Category)
                .Include(to => to.Product!)
                .ThenInclude(p => p.ProductImages)
                .Where(x => x.User!.Email == email)
                .ToListAsync();

            return new ActionResponse<IEnumerable<TemporalOrder>>
            {
                WasSuccess = true,
                Result = temporalOrders
            };
        }

        public async Task<ActionResponse<int>> GetCountAsync(string email)
        {
            var count = await _context.TemporalOrders
                .Where(x => x.User!.Email == email)
                .SumAsync(x => x.Quantity);

            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = (int)count
            };
        }

        public async Task<ActionResponse<TemporalOrder>> PutFullAsync(TemporalOrderDto temporalOrderDto)
        {
            var currentTemporalOrder = await _context.TemporalOrders.FirstOrDefaultAsync(x => x.Id == temporalOrderDto.Id);
            if (currentTemporalOrder == null)
            {
                return new ActionResponse<TemporalOrder>
                {
                    WasSuccess = false,
                    Message = "Registro no encontrado"
                };
            }

            currentTemporalOrder!.Remarks = temporalOrderDto.Remarks;
            currentTemporalOrder.Quantity = temporalOrderDto.Quantity;

            _context.Update(currentTemporalOrder);
            await _context.SaveChangesAsync();
            return new ActionResponse<TemporalOrder>
            {
                WasSuccess = true,
                Result = currentTemporalOrder
            };
        }

        public override async Task<ActionResponse<TemporalOrder>> GetAsync(int id)
        {
            var temporalOrder = await _context.TemporalOrders
                .Include(ts => ts.User!)
                .Include(ts => ts.Product!)
                .ThenInclude(p => p.ProductCategories!)
                .ThenInclude(pc => pc.Category)
                .Include(ts => ts.Product!)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (temporalOrder == null)
            {
                return new ActionResponse<TemporalOrder>
                {
                    WasSuccess = false,
                    Message = "Registro no encontrado"
                };
            }

            return new ActionResponse<TemporalOrder>
            {
                WasSuccess = true,
                Result = temporalOrder
            };
        }

    }
}
