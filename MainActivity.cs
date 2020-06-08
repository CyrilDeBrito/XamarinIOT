using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using XamarinAppAndroidIOT.Models;

namespace XamarinAppAndroidIOT
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static AccelerometerReader accelerometerReader = new AccelerometerReader();
        public static SongPlayer songPlayer;

        Button BtnVoice1;
        Button BtnVoice2;
        Button BtnStopVoice;

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            BtnVoice1 = FindViewById<Button>(Resource.Id.btnVoice1);   
            BtnVoice2 = FindViewById<Button>(Resource.Id.btnVoice2);
            BtnStopVoice = FindViewById<Button>(Resource.Id.btnStopVoice);

            BtnVoice1.Click += BtnVoice1_Click;
            BtnVoice2.Click += BtnVoice2_Click;
            BtnStopVoice.Click += BtnStopVoice_Click;

            songPlayer = new SongPlayer("01", this);

            accelerometerReader.ToggleAccelerometer();

            songPlayer.ControlPlayer("01");
        }

        private void BtnVoice1_Click(object sender, EventArgs e)
        {
            songPlayer.Voice = "01";
        }

        private void BtnVoice2_Click(object sender, EventArgs e)
        {
            songPlayer.Voice = "02";
        }

        private void BtnStopVoice_Click(object sender, EventArgs e)
        {
            // Stop Sound
            songPlayer.Voice = "03";
        }
	}
}

