using Android.App;
using Android.OS;

namespace SimpleAuth.Droid
{
    [Activity]
    public class MainMainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainMain);
        }
    }
}