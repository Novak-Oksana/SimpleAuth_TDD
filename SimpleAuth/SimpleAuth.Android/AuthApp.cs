using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SimpleAuth.Droid
{
    [Application]
    public class AuthApp : Application
    {
        public IAuthInteractor Interactor { get; private set; }
        public IRequest Request { get; private set; }
        protected AuthApp(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Request = new Request();
            Interactor = new AuthInteractor(Request);
        }
    }
}