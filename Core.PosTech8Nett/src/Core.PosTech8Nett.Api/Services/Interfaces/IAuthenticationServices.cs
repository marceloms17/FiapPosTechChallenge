using Core.PosTech8Nett.Api.Domain.Model.Authenticator;
using System.Threading.Tasks;

namespace Core.PosTech8Nett.Api.Services.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<string> LoginAsync(LoginRequest request);
    }
}
