using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.DAL.Repositories.Interfaces;

namespace Infrastructure.DAL.Repositories
{
    public class ClientsRepository(ClientDBContext context) : BaseRepository<Client>(context), IClientsRepository
    {
    }
}
