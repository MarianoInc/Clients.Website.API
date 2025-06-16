using Domain.Entities;
using Domain.ViewModels;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clients.Website.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [EnableCors("ClientsWebSiteCorsPolicy")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;

        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (clients, total) = await _clientsService.GetPagedAsync(page, pageSize);
            return Ok(new { total, clients });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var client = await _clientsService.GetByIdAsync(id);
            return client is null ? NotFound() : Ok(new WebApiResponse<Client> { IsSuccess = false, Message = "", Value = client });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            await _clientsService.AddAsync(client);
            return CreatedAtAction(nameof(Get), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Client updated)
        {
            if (id != updated.Id)
                return BadRequest("ID mismatch");

            var existing = await _clientsService.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            existing.Name = updated.Name;
            existing.Email = updated.Email;

            await _clientsService.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _clientsService.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _clientsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
