﻿using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public CommandV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCommands()
        {
            var commands = await
           _repository.Command.GetAllCommandsAsync(trackChanges: false);
            return Ok(commands);
        }
    }
}
