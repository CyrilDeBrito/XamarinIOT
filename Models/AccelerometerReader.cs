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
        // Speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;
        // isStart to know if Accelerometer is start
        bool isStarted = false;
        // Gravitational phone X; Y, Z
        public float accX;
        public float accY;
        public float accZ;
        // Gravitational calcul on Phone
        public double phoneGravitationalForce;
        // Earth Gravity
        public double earthForce = SensorManager.GravityEarth;
        // Shock on phone to play a different song
        public int nbShockOnPhone = 1;

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

            //Gravity of phone 
            phoneGravitationalForce = Math.Sqrt(gX * gX + gY * gY + gZ * gZ);

            //Gravity of earth
            //earthForce = SensorManager.GravityEarth;

            ShockOnPhone();

            //Control on console gravity phone real time
            //Console.WriteLine($"Reading: X: {accX}, Y: {accY}, Z: {accZ}");

        }

        private void ShockOnPhone()
        {
            if (IsShock())
            {
                 switch (nbShockOnPhone) {

                    case 1:
                        Console.WriteLine("Shock :" + nbShockOnPhone);
                        nbShockOnPhone = 2;
                        MainActivity.songPlayer.ControlPlayer("05");
                        break;

                    case 2:
                        Console.WriteLine("Shock :" + nbShockOnPhone);
                        nbShockOnPhone =3;
                        MainActivity.songPlayer.ControlPlayer("06");
                        break;

                    case 3:
                        Console.WriteLine("Shock :" + nbShockOnPhone);
                        nbShockOnPhone = 1;
                        MainActivity.songPlayer.ControlPlayer("07");
                        break;

                 }
            }
        }

        private bool IsShock()
        {
            double apporx = earthForce / 4;
            double earthForceLess = earthForce - apporx;
            double earthForceMore = earthForce + apporx;

            // Set isShock on false to stop function shock if phone are dont shoot.
            bool isShock = false;
            if (phoneGravitationalForce < earthForceLess || phoneGravitationalForce > earthForceMore)
            {
                isShock = true;
            }
            return isShock;
        }
    }
}