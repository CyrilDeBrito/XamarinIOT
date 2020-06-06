using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;

namespace XamarinAppAndroidIOT.Models
{
    public class GyroscopeReader
    {
        //Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;
        //Set isStart to know if Gyroscope is start
        bool isStarted = false;

        public GyroscopeReader()
        {
            // Register dor reading changes.
            Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
        }

        
            


        public void ToggleGyroscope()
        {
            // The Main Thread - To start the code on Main Thread
            MainThread.BeginInvokeOnMainThread(() => {
                try
                {
                    if (Gyroscope.IsMonitoring)
                    {
                        Console.WriteLine("Gyroscope Stoped");
                        Gyroscope.Stop();
                        Console.WriteLine("Gyroscope Stoped - isStarted : " + isStarted);
                    }
                    else
                    {
                        Console.WriteLine("Gyroscope Started");
                        Gyroscope.Start(speed);
                        this.isStarted = true;
                        Console.WriteLine("Gyroscope Started - isStarted : " + isStarted);
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Feature not supported on device
                }
                catch (Exception ex)
                {
                    // Other error has occurred.
                }
            });
        }

        private void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            var data = e.Reading;
            // To know the Velocity to Gyroscope
            var velocity = data.AngularVelocity;

            // Process Angular Velocity X, Y, Z, reported in rad.s-1
            Console.WriteLine(
                $"Reading: X: {velocity.X}, " +
                $"Y: {velocity.Y}, " +
                $"Z: {velocity.Z}");
        }
    }
}