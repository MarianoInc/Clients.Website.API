using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class ClientDBContext : DbContext
    {
        public ClientDBContext(DbContextOptions<ClientDBContext> options) : base(options)
        {
        }

        DbSet<Client> Clients => Set<Client>();

    }
}
