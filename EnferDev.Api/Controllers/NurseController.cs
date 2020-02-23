using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnferDev.Api.ViewModels;
using EnferDev.Domain.Commands;
using EnferDev.Domain.Handlers;
using EnferDev.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnferDev.Api.Controllers
{
    [Route("api/v1/enfermeiros")]
    [ApiController]
    public class NurseController : ControllerBase
    {
        private readonly IMapper _mapper;

        public NurseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] INurseRepository repository)
        {
            var result = await repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<NurseViewModel>>(result));
        }

        [Route("{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id, [FromServices] INurseRepository repository)
        {
            var result = await repository.GetById(id);
            return Ok(_mapper.Map<NurseViewModel>(result));
        }

        [Route("hospitais/{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetAllByHospital(Guid id, [FromServices] INurseRepository repository)
        {
            var result = await repository.GetByHospital(id);
            return Ok(_mapper.Map<IEnumerable<NurseViewModel>>(result));
        }

        [Route("ativos")]
        [HttpGet]
        public async Task<IActionResult> GetAllByActive([FromServices] INurseRepository repository)
        {
            var result = await repository.GetAllActive(true);
            return Ok(_mapper.Map<IEnumerable<NurseViewModel>>(result));
        }

        [Route("desativos")]
        [HttpGet]
        public async Task<IActionResult> GetAllByDesactive([FromServices] INurseRepository repository)
        {
            var result = await repository.GetAllDesactive(false);
            return Ok(_mapper.Map<IEnumerable<NurseViewModel>>(result));
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateNurseCommand command, [FromServices] NurseHandler handler)
        {
            return Ok(await handler.Handler(command));
        }

        [Route("")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateNurseCommand command, [FromServices] NurseHandler handler)
        {
            return Ok(await handler.Handler(command));
        }

        [Route("desativar")]
        [HttpPut]
        public async Task<IActionResult> Disable([FromBody]DesactiveNurseCommand command, [FromServices] NurseHandler handler)
        {
            return Ok(await handler.Handler(command));
        }

        [Route("ativar")]
        [HttpPut]
        public async Task<IActionResult> Enable([FromBody]ActiveNurseCommand command, [FromServices] NurseHandler handler)
        {
            return Ok(await handler.Handler(command));
        }
    }
}