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
using Android.Content.Res;

namespace XamarinAppAndroidIOT.Models
{
    public class SongPlayer
    {
        protected MediaPlayer player;

        public String Voice;
        public MainActivity Context;

        public SongPlayer(string voice, MainActivity ContextA)
        {
            Context = ContextA;
            Voice = voice;
        }

        public void ControlPlayer(String command)
        {
            if (Voice != "03") { 
                var song = "Voice" + Voice + "_" + command + ".mp3";
                StartPlayer(song);
            } else
            {
                Console.WriteLine("StopSong clicked on user view");
            }
        }

        public void StartPlayer(String filePath)
        {
            // Singleton of MediaPlayer
            if (player == null)
            {
                player = new MediaPlayer();
            }

            AssetFileDescriptor afd = Context.BaseContext.Assets.OpenFd(filePath);

            player.Reset();
                player.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
                player.Prepare();
                player.Start();
        }

        public MediaPlayer MediaPlayer { get; private set; }
    }
}