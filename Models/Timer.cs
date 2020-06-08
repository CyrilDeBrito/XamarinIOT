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
using Xamarin.Essentials;


namespace XamarinAppAndroidIOT.Models
{
    public class Timer
    {
        protected Timer timer;
        public int time;

        public void startTimer(int time)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                System.Timers.Timer Timer1 = new System.Timers.Timer();

                // Start the timer
                Timer1.Start();
                Timer1.Interval = time;
                Timer1.Enabled = false;
                Timer1.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) =>
                {
                        var tvAccX = MainActivity.accelerometerReader.accX.ToString();
                        var tvAccY = MainActivity.accelerometerReader.accY.ToString();
                        var tvAccZ = MainActivity.accelerometerReader.accZ.ToString();
                };
            });
        }
    }
}