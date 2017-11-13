using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SimpleAuth;

namespace Tests
{
    [TestFixture]
    public class InteractorTest
    {
        private Mock<IRequest> _requestMock;
        private IAuthInteractor _interactor;

        [SetUp]
        public void Setup()
        {
            _requestMock = new Mock<IRequest>(MockBehavior.Strict);
            _interactor = new AuthInteractor(_requestMock.Object);
        }

        [Test]
        public void CtorTest()
        {
            var fieldInfo = typeof(AuthInteractor).GetField("_request",
                BindingFlags.NonPublic | BindingFlags.Instance);
            var data = fieldInfo.GetValue(_interactor);
            Assert.AreEqual(_requestMock.Object, data);
        }

        [Test]
        public void CtorRequestNullTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _interactor = new AuthInteractor(null);
            });
        }

        [TestCase(EAuthResponse.OtherError)]
        [TestCase(EAuthResponse.InvalidData)]
        [TestCase(EAuthResponse.Success)]
        public async Task AuthTest(EAuthResponse response)
        {
            _requestMock.Setup(f => f.Authorize("", ""))
                .Returns(Task.FromResult(response));

            var resp = await _interactor.Authorize("", "");

            Assert.AreEqual(response, resp);
            _requestMock.Verify(f => f.Authorize("", ""), Times.Once);
        }
    }
}