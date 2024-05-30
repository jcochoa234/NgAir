﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NgAir.BackEnd.UnitsOfWork.Interfaces;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;

namespace NgAir.BackEnd.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class TemporalOrdersController : GenericController<TemporalOrder>
    {
        private readonly ITemporalOrdersUnitOfWork _temporalOrdersUnitOfWork;

        public TemporalOrdersController(IGenericUnitOfWork<TemporalOrder> unitOfWork, ITemporalOrdersUnitOfWork temporalOrdersUnitOfWork) : base(unitOfWork)
        {
            _temporalOrdersUnitOfWork = temporalOrdersUnitOfWork;
        }

        [HttpGet("my")]
        public override async Task<IActionResult> GetAsync()
        {
            var action = await _temporalOrdersUnitOfWork.GetAsync(User.Identity!.Name!);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _temporalOrdersUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCountAsync()
        {
            var action = await _temporalOrdersUnitOfWork.GetCountAsync(User.Identity!.Name!);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpPost("full")]
        public async Task<IActionResult> PostAsync(TemporalOrderDto temporalOrderDto)
        {
            var action = await _temporalOrdersUnitOfWork.AddFullAsync(User.Identity!.Name!, temporalOrderDto);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpPut("full")]
        public async Task<IActionResult> PutFullAsync(TemporalOrderDto temporalOrderDto)
        {
            var action = await _temporalOrdersUnitOfWork.PutFullAsync(temporalOrderDto);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound(action.Message);
        }

    }
}
