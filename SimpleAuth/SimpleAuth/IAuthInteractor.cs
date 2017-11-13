using System.Threading.Tasks;

namespace SimpleAuth
{
    public interface IAuthInteractor
    {
        Task<EAuthResponse> Authorize(string login, string pass);
    }
}