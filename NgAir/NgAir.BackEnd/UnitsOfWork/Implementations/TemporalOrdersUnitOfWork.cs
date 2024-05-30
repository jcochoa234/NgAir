﻿using NgAir.BackEnd.Repositories.Interfaces;
using NgAir.BackEnd.UnitsOfWork.Interfaces;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;
using NgAir.Shared.Responses;

namespace NgAir.BackEnd.UnitsOfWork.Implementations
{
    public class TemporalOrdersUnitOfWork : GenericUnitOfWork<TemporalOrder>, ITemporalOrdersUnitOfWork
    {
        private readonly ITemporalOrdersRepository _temporalOrdersRepository;

        public TemporalOrdersUnitOfWork(IGenericRepository<TemporalOrder> repository, ITemporalOrdersRepository temporalOrdersRepository) : base(repository)
        {
            _temporalOrdersRepository = temporalOrdersRepository;
        }

        public async Task<ActionResponse<TemporalOrderDto>> AddFullAsync(string email, TemporalOrderDto temporalOrderDto) => await _temporalOrdersRepository.AddFullAsync(email, temporalOrderDto);

        public override async Task<ActionResponse<TemporalOrder>> GetAsync(int id) => await _temporalOrdersRepository.GetAsync(id);

        public async Task<ActionResponse<IEnumerable<TemporalOrder>>> GetAsync(string email) => await _temporalOrdersRepository.GetAsync(email);

        public async Task<ActionResponse<int>> GetCountAsync(string email) => await _temporalOrdersRepository.GetCountAsync(email);

        public async Task<ActionResponse<TemporalOrder>> PutFullAsync(TemporalOrderDto temporalOrderDto) => await _temporalOrdersRepository.PutFullAsync(temporalOrderDto);

    }
}
