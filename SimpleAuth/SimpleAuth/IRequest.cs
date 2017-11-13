using System.Threading.Tasks;

namespace SimpleAuth
{
    public interface IRequest
    {
        Task<EAuthResponse> Authorize(string login, string pass);
    }
}