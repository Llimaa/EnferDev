using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnferDev.Api.ViewModels;
using EnferDev.Domain.Commands;
using EnferDev.Domain.Handlers;
using EnferDev.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EnferDev.Api.Controllers
{
    [Route("api/v1/hospitais")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IMapper _mapper;

        public HospitalController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IHospitalRepository repository)
            {
            var result = await repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<HospitalViewModel>>(result));
        }

        [Route("{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id, [FromServices] IHospitalRepository repository)
        {
            var result = await repository.GetById(id);
            return Ok(_mapper.Map<HospitalViewModel>(result));
        }

        [Route("ativos")]
        [HttpGet]
        public async Task<IActionResult> GetAllByActive([FromServices] IHospitalRepository repository)
        {
            var result = await repository.GetAllActive(true);
            return Ok(_mapper.Map<IEnumerable<HospitalViewModel>>(result));
        }

        [Route("desativos")]
        [HttpGet]
        public async Task<IActionResult> GetAllByDesactive([FromServices] IHospitalRepository repository)
        {
            var result = await repository.GetAllDesactive(false);
            return Ok(_mapper.Map<IEnumerable<HospitalViewModel>>(result));
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateHospitalCommand command, [FromServices] HospitalHandler handler)
        {
            return Ok(await handler.Handler(command));
        }

        [Route("")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateHospitalCommand command, [FromServices] HospitalHandler handler)
        {
            return Ok(await handler.Handler(command));
        }

        [Route("desativar")]
        [HttpPut]
        public async Task<IActionResult> Disable([FromBody]DesactiveHospitalCommand command, [FromServices] HospitalHandler handler)
        {
            return Ok(await handler.Handler(command));
        }

        [Route("ativar")]
        [HttpPut]
        public async Task<IActionResult> Enable([FromBody]ActiveHospitalCommand command, [FromServices] HospitalHandler handler)
        {
            return Ok(await handler.Handler(command));
        }
    }
}