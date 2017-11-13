using System.Threading.Tasks;

namespace SimpleAuth
{
    public interface IAuthPresenter
    {
        Task Authorize(string login, string pass);
    }
}