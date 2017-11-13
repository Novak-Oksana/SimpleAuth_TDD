using System;
using System.Threading.Tasks;

namespace SimpleAuth
{
    public class AuthInteractor : IAuthInteractor
    {
        private readonly IRequest _request;

        public AuthInteractor(IRequest request)
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task<EAuthResponse> Authorize(string login, string pass)
        {
            var res = await _request.Authorize(login, pass);
            return res;
        }
    }
}