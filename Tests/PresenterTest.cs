using System;
using System.Reflection;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SimpleAuth;

namespace Tests
{
    [TestFixture]
    public class PresenterTest
    {
        private Mock<IAuthInteractor> _interactorMock;
        private Mock<IAuthView> _viewMock;
        private Mock<IAuthRouter> _routerMock;
        private IAuthPresenter _presenter;

        [SetUp]
        public void Setup()
        {
            _interactorMock = new Mock<IAuthInteractor>(MockBehavior.Strict);
            _viewMock = new Mock<IAuthView>(MockBehavior.Strict);
            _routerMock = new Mock<IAuthRouter>(MockBehavior.Strict);
            _presenter = new AuthPresenter(_interactorMock.Object, _viewMock.Object, _routerMock.Object);
        }

        [Test]
        public void CtorInteractorValueTest()
        {
            var fieldInfo = typeof(AuthPresenter).GetField("_interactor",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var data = fieldInfo.GetValue(_presenter);
            Assert.AreEqual(_interactorMock.Object, data);
        }

        [Test]
        public void CtorViewValueTest()
        {
            var fieldInfo = typeof(AuthPresenter).GetField("_view",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var data = fieldInfo.GetValue(_presenter);
            Assert.AreEqual(_viewMock.Object, data);
        }

        [Test]
        public void CtorRouterValueTest()
        {
            var fieldInfo = typeof(AuthPresenter).GetField("_router",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var data = fieldInfo.GetValue(_presenter);
            Assert.AreEqual(_routerMock.Object, data);
        }

        [Test]
        public void CtorInteartorNullTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _presenter = new AuthPresenter(null, _viewMock.Object, _routerMock.Object);
            });
        }

        [Test]
        public void CtorViewNullTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _presenter = new AuthPresenter(_interactorMock.Object, null, _routerMock.Object);
            });
        }

        [Test]
        public void CtorRouterNullTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _presenter = new AuthPresenter(_interactorMock.Object, _viewMock.Object, null);
            });
        }

        [Test]
        public void AuthSuccessTest()
        {
            _interactorMock.Setup(f => f.Authorize("", ""))
                .Returns(Task.FromResult(EAuthResponse.Success));
            _routerMock.Setup(f => f.GoToMainPage());

            _presenter.Authorize("", "");

            _interactorMock.Verify(f => f.Authorize("", ""));
            _routerMock.Verify(f => f.GoToMainPage());
        }

        [TestCase(EAuthResponse.OtherError)]
        [TestCase(EAuthResponse.InvalidData)]
        public void AuthSuccessTest(EAuthResponse response)
        {
            _interactorMock.Setup(f => f.Authorize("", ""))
                .Returns(Task.FromResult(response));
            _viewMock.Setup(f => f.ShowMessage(response));

            _presenter.Authorize("", "");

            _interactorMock.Verify(f => f.Authorize("", ""));
            _viewMock.Verify(f => f.ShowMessage(response));
        }
    }
}