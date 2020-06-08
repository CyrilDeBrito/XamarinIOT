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
        // Shock on phone to play a different songs
        public int nbShockOnPhone = 1;
        // Pitch on phone to know where he pitch
        public string pitchRotation = "";
        public int waitToReVoice = 0;

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

            if (IsShock())
            {
                ShockOnPhone();
            }
            else if (IsPitch())
            {
                PitchOnPhone();
            }
            // reset waitToReVoice if the phone is in flat position.
            if (!IsPitch())
            {
                waitToReVoice = 0;
                //Console.WriteLine("waitToReVoice : RESET TO " + waitToReVoice);
            }


            //Control on console gravity phone real time
            //Console.WriteLine($"Reading: X: {accX}, Y: {accY}, Z: {accZ}");
            //Console.WriteLine($"Reading change : X: {gX}, Y: {gY}, Z: {gZ}");

        }

        private void ShockOnPhone()
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

        private void PitchOnPhone()
        {
            Console.WriteLine("IsPitch : " + IsPitch());

            if (IsPitch())
            {
                waitToReVoice = waitToReVoice + 1;
                Console.WriteLine("waitToReVoice : " + waitToReVoice);
            }

            if (waitToReVoice == 1) { 
                switch (pitchRotation)
                {
                    // pitch : 
                    // 1: Left <-
                    // 2: top /\
                    // 3: right ->
                    // 4: down \/

                    case "left-right":
                        Console.WriteLine("Pitch :" + pitchRotation);
                        MainActivity.songPlayer.ControlPlayer("02");
                        //MainActivity.timer.startTimer(5000);
                        break;

                    case "top-down":
                        Console.WriteLine("Pitch :" + pitchRotation);
                        MainActivity.songPlayer.ControlPlayer("03");
                        //MainActivity.timer.startTimer(5000);
                        break;

                    case "through":
                        Console.WriteLine("Pitch :" + pitchRotation);
                        MainActivity.songPlayer.ControlPlayer("04");
                        //MainActivity.timer.startTimer(5000);
                        break;

                }
            }
        }

        private bool IsPitch()
        {
            // Set isShock on false to stop function shock if phone are dont shoot.
            bool isPitch = false;
            // G force of earth
            var gX = accX * SensorManager.GravityEarth;
            var gY = accY * SensorManager.GravityEarth;
            var gZ = accZ * SensorManager.GravityEarth;

            //Aproximative to be sure phone pitch
            double apporx = 1.80;
            double gXLess = 0.00 - apporx;
            double gXMore = 0.00 + apporx;
            double gYLess = 0.00 - apporx;
            double gYMore = 0.00 + apporx;
            double gZLess = 9.50 - apporx;
            double gZMore = 10.50 + apporx;

            if (gZ > gZMore || gZ < gZLess)
            {
                isPitch = true;
                pitchRotation = "through";
                
            }
            else if (gY > gYMore || gY < gYLess)
            {
                isPitch = true;
                pitchRotation = "top-down";
            }
            else if (gX > gXMore || gX < gXLess)
            {
                isPitch = true;
                pitchRotation = "left-right";
            }

            //Test X, Y, Z
            //Console.WriteLine($"Reading IsPitch: X: {gX}, Y: {gY}, Z: {gZ}");

            return isPitch;


            //flat :       IsPitch: X: -0,02872061, Y: 0,09573536, Z: 9,726713
            // Left X :    IsPitch: X:  3,647517,   Y: 0,04786768, Z: 9,200169
            // Top Y :     IsPitch: X: 0,2106178,   Y: -3,14012,   Z: 9,276756  
            // Right X :   IsPitch: X: -4,154915,   Y: 0,06701476, Z: 8,644903
            // down Y :    IsPitch: X: 0,03829415,  Y: 2,824193,   Z: 9,353346 
        }
    }
}