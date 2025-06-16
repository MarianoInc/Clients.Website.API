using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.DAL.Repositories;
using Infrastructure.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

public class RepositoryTests
{
    private IClientsRepository CreateRepository()
    {
        var options = new DbContextOptionsBuilder<ClientDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new ClientDBContext(options);
        return new ClientsRepository(context);
    }

    [Fact]
    public async Task Add_Client_Works()
    {
        var repo = CreateRepository();
        var client = new Client { Name = "Test", Email = "test@example.com" };

        await repo.AddAsync(client);
        var stored = await repo.GetByIdAsync(client.Id);

        Assert.NotNull(stored);
        Assert.Equal("Test", stored!.Name);
    }

    [Fact]
    public async Task GetPaged_Returns_Expected_Count()
    {
        var repo = CreateRepository();
        for (int i = 1; i <= 25; i++)
        {
            await repo.AddAsync(new Client { Name = $"Client {i}", Email = $"client{i}@mail.com" });
        }

        var (page1, total1) = await repo.GetPagedAsync(1, 10);
        var (page2, total2) = await repo.GetPagedAsync(2, 10);

        Assert.Equal(10, page1.Count);
        Assert.Equal(10, page2.Count);
        Assert.Equal(25, total1);
        Assert.Equal(25, total2);
    }
}
