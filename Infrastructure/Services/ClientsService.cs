using Domain.Entities;
using Domain.ViewModels;
using Infrastructure.DAL.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Services
{
    public class ClientsService : IClientsService
    {
        private readonly IClientsRepository _clientsRepo;

        public ClientsService(IClientsRepository clientsRepo)
        {
            _clientsRepo = clientsRepo;
        }

        public async Task AddAsync(Domain.Entities.Client client)
        {
            await _clientsRepo.AddAsync(client);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _clientsRepo.DeleteAsync(id);
        }

        public async Task<List<Domain.Entities.Client>> GetAllAsync()
        {
            return await _clientsRepo.GetAllAsync();
        }

        public async Task<Domain.Entities.Client?> GetByIdAsync(Guid id)
        {
            return await _clientsRepo.GetByIdAsync(id);
        }

        public async Task<(List<Domain.Entities.Client> Items, int Total)> GetPagedAsync(int page, int pageSize)
        {
            return await _clientsRepo.GetPagedAsync(page, pageSize);
        }

        public async Task UpdateAsync(Domain.Entities.Client client)
        {
            await _clientsRepo.UpdateAsync(client);
        }
    }
}
