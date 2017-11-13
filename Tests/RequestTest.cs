using NUnit.Framework;
using SimpleAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    class RequestTest
    {
        private IRequest _request;

        [SetUp]
        public void Setup()
        {
            _request = new Request();
        }

        [TestCase("ort")]
        [TestCase("")]
        public void RequestInvalidDataTest(string login)
        {
            string pass = "12345678";
            var resp = _request.Authorize(login, pass).Result;
            Assert.AreEqual(EAuthResponse.IncorrectLogin, resp);
        }

        [TestCase("@ort")]
        public void RequestSuccessTest(string login)
        {
            string pass = "ort";
            var resp = _request.Authorize(login, pass).Result;
            Assert.AreEqual(EAuthResponse.Success, resp);
        }

        [TestCase("@")]
        [TestCase("ort@")]
        [TestCase("ort@@1")]
        public void RequestErrorTest(string login)
        {
            string pass = "12345678";
            var resp = _request.Authorize(login, pass).Result;
            Assert.AreEqual(EAuthResponse.OtherError, resp);
        }
    }
}
