using Core.PosTech8Nett.Api.Domain.Entities.GameInformation;
using Core.PosTech8Nett.Api.Infra.DataBase.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public class GameService : IGameService
{
    private readonly IRepository<Game> _repository;

    public GameService(IRepository<Game> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
        => await _repository.GetAllAsync();

    public async Task<Game?> GetByIdAsync(Guid id)
        => await _repository.GetByIdAsync(id);

    public async Task AddAsync(Game game)
    {
        await _repository.AddAsync(game);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Game game)
    {
        _repository.Update(game);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is not null)
        {
            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }
    }
}
