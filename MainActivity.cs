using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace XamarinAppAndroidIOT
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        Button BtnVoice1;
        Button BtnVoice2;
        Button BtnVoice3;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            BtnVoice1 = FindViewById<Button>(Resource.Id.btnVoice1);   
            BtnVoice2 = FindViewById<Button>(Resource.Id.btnVoice2);
            BtnVoice3 = FindViewById<Button>(Resource.Id.btnVoice3);

            BtnVoice1.Click += BtnVoice1_Click;
            BtnVoice2.Click += BtnVoice2_Click;
            BtnVoice3.Click += BtnVoice3_Click;

        }

        private void BtnVoice1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnVoice2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnVoice3_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}

