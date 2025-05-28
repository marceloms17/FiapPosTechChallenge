using Core.PosTech8Nett.Api.Domain.Entities.GameInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public interface IGameService
{
    Task<IEnumerable<Game>> GetAllAsync();
    Task<Game?> GetByIdAsync(Guid id);
    Task AddAsync(Game game);
    Task UpdateAsync(Game game);
    Task DeleteAsync(Guid id);
}
