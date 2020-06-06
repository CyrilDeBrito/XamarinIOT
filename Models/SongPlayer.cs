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
using Android.Media;
using System.Runtime.CompilerServices;

namespace XamarinAppAndroidIOT.Models
{
    public class SongPlayer
    {

        protected MediaPlayer player;
        public void StartPlayer(String filePath)
        {
            if (player == null)
            {
                player = new MediaPlayer();
            }
            else
            {
                player.Reset();
                player.SetDataSource(filePath);
                player.Prepare();
                player.Start();
            }
        }


        public MediaPlayer MediaPlayer { get; private set; }

        public void PlayerSong1()
        {
            //MediaPlayer = MediaPlayer.Create(this, Resource.Raw.Voice01_01);
        }
        
    }
}