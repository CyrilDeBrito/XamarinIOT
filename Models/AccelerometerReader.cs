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
using Java.Security;
using Xamarin.Essentials;

namespace XamarinAppAndroidIOT.Models
{
    public class AccelerometerReader
    {
        //Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;
        //Set isStart to know if Accelerometer is start
        bool isStarted = false;
        public float accX;
        public float accY;
        public float accZ;
        public double gForce;

        public AccelerometerReader()
        {
            //Register for reading changes, be sure to unsuscribe chen finished.
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }

        public void ToggleAccelerometer()
        {
            MainThread.BeginInvokeOnMainThread(() => {
                try
                {
                    if (Accelerometer.IsMonitoring)
                    {
                        Console.WriteLine("Accelerator Stoped");
                        Accelerometer.Stop();
                        Console.WriteLine("Accelerator Stoped - isStarted : " + isStarted);
                    }
                    else
                    {
                        Console.WriteLine("Accelerator Started");
                        Accelerometer.Start(speed);
                        this.isStarted = true;
                        Console.WriteLine("Accelerator Started - isStarted : " + isStarted);
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Feature not supported on device
                }
                catch (Exception ex)
                {
                    // Other error has occurred
                }
            });
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            //Process Acceleration X, Y, Z.
            accX = data.Acceleration.X;
            accY = data.Acceleration.Y;
            accZ = data.Acceleration.Z;

            // G force of earth
            var gX = accX * SensorManager.GravityEarth;
            var gY = accY * SensorManager.GravityEarth;
            var gZ = accZ * SensorManager.GravityEarth;

            //calcul force terrestre 
            gForce = Math.Sqrt(gX * gX + gY * gY + gZ * gZ);

            shockOnPhone();

            //Control on console
            Console.WriteLine($"Reading: X: {accX}, Y: {accY}, Z: {accZ}");

        }

        private void shockOnPhone()
        {
            var sensorManager = SensorManager.GravityEarth;
            if (gForce != sensorManager)
            {
                MainActivity.songPlayer.ControlPlayer("05");
            }
        }
    }
}