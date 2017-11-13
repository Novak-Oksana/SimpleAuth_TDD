using System;
using System.Threading.Tasks;

namespace SimpleAuth
{
    public class AuthPresenter : IAuthPresenter
    {
        private readonly IAuthInteractor _interactor;
        private IAuthView _view;
        private readonly IAuthRouter _router;

        public AuthPresenter(IAuthInteractor interactor, IAuthView view, IAuthRouter router)
        {
            _interactor = interactor ?? throw new ArgumentNullException(nameof(interactor));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _router = router ?? throw new ArgumentNullException(nameof(router));
        }

        public async Task Authorize(string login, string pass)
        {
            var res = await _interactor.Authorize(login, pass);
            if (res == EAuthResponse.Success)
            {
                _router.GoToMainPage();
            }
            else
            {
                _view.ShowMessage(res);
            }
        }
    }
}