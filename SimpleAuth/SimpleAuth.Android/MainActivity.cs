using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SimpleAuth.Droid
{
	[Activity (Label = "SimpleAuth.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, IAuthView, IAuthRouter
	{
		int count = 1;
	    private AuthApp _app;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
		    _app = (AuthApp) Application;
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

		    var request = new Request();
		    var interactor = new AuthInteractor(request);

            var btnLogin = FindViewById<Button>(Resource.Id.button_login);
            var txtLogin = FindViewById<EditText>(Resource.Id.editText_login);
		    var txtPass = FindViewById<EditText>(Resource.Id.editText_pass);
		    var presenter = new AuthPresenter(interactor, this, this);
		    btnLogin.Click += (sender, args) =>
		    {
		        presenter.Authorize(txtLogin.Text, txtPass.Text);
		    };
		    
		}

	    public void ShowMessage(EAuthResponse response)
	    {
	        RunOnUiThread(() =>
	        {
	            Toast.MakeText(this, response.ToString(), ToastLength.Long).Show();
	        });
	    }

	    public void GoToMainPage()
	    {
	        RunOnUiThread(() =>
	        {
	            Intent i = new Intent(this, typeof(MainMainActivity));
                StartActivity(i);
	        });
        }
	}
}


