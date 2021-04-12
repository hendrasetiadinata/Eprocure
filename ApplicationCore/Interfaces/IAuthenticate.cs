using ApplicationCore.Models;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAuthenticate
    {
        Task<AuthenticateResponse> GetTokenAsync(AuthenticateRequest request);
    }
}
