using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Pax.Core;
using System.Runtime.Serialization;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4SoundLavaAndIce))]
    public class Pax4SoundLavaAndIce : Pax4Sound
    {   
        public SoundEffect _lavaandiceButtonAccepted = null;
        public SoundEffect _lavaandiceButtonDenied = null;

        public SoundEffect _lavaandiceBurning = null;
        public SoundEffect _lavaandiceBurning1 = null;
        public SoundEffect _lavaandiceFreezing = null;
        public SoundEffect _lavaandiceFreezing1 = null;

        public SoundEffect _lavaandiceLavaLaunch = null;
        public SoundEffect _lavaandiceLavaLaunch1 = null;
        public SoundEffect _lavaandiceIceLaunch = null;
        public SoundEffect _lavaandiceIceLaunch1 = null;

        public SoundEffect _lavaandiceLavaExplosion = null;
        public SoundEffect _lavaandiceLavaExplosion1 = null;
        public SoundEffect _lavaandiceIceExplosion = null;
        public SoundEffect _lavaandiceIceExplosion1 = null;

        public SoundEffect _lavaandiceTrumpet = null;

        public SoundEffect _lavaandiceTimer1 = null;
        public SoundEffect _lavaandiceTimer2 = null;

        public Pax4SoundLavaAndIce(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
            MediaPlayer.Volume = 0f; //!*
            _current = this;

            List<String> list = new List<String>();

            //******************
            //sound*************
            //******************
            list.Add("Sound/lavaandiceMenu1");
            list.Add("Sound/lavaandiceMenu2");
            list.Add("Sound/lavaandiceMission1");
            list.Add("Sound/lavaandiceMission2");
            LoadSong(list);
            list.Clear();

            list.Add("Sound/lavaandiceGameOver");
            list.Add("Sound/lavaandiceWinParade");
            LoadStateSong(list);
            list.Clear();

            list.Add("Sound/lavaandiceButtonAccepted");
            list.Add("Sound/lavaandiceButtonDenied");

            list.Add("Sound/lavaandiceBurning");
            list.Add("Sound/lavaandiceBurning1");
            list.Add("Sound/lavaandiceFreezing");
            list.Add("Sound/lavaandiceFreezing1");

            list.Add("Sound/lavaandiceLavaLaunch");
            list.Add("Sound/lavaandiceLavaLaunch1");
            list.Add("Sound/lavaandiceIceLaunch");
            list.Add("Sound/lavaandiceIceLaunch1");

            list.Add("Sound/lavaandiceLavaExplosion");
            list.Add("Sound/lavaandiceLavaExplosion1");
            list.Add("Sound/lavaandiceIceExplosion");
            list.Add("Sound/lavaandiceIceExplosion1");

            list.Add("Sound/lavaandiceTrumpet");

            list.Add("Sound/lavaandiceTimer1");
            list.Add("Sound/lavaandiceTimer2");
            LoadSoundEffect(list);
            list.Clear();

            //******************
            //soundeffect*************
            //******************

            _lavaandiceButtonAccepted = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceButtonAccepted");
            _lavaandiceButtonDenied = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceButtonDenied");

            _lavaandiceBurning = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceBurning");
            _lavaandiceBurning1 = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceBurning1");
            _lavaandiceFreezing = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceFreezing");
            _lavaandiceFreezing1 = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceFreezing1");

            _lavaandiceLavaLaunch = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceLavaLaunch");
            _lavaandiceLavaLaunch1 = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceLavaLaunch1");
            _lavaandiceIceLaunch = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceIceLaunch");
            _lavaandiceIceLaunch1 = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceIceLaunch1");

            _lavaandiceLavaExplosion = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceLavaExplosion");
            _lavaandiceLavaExplosion1 = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceLavaExplosion1");
            _lavaandiceIceExplosion = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceIceExplosion");
            _lavaandiceIceExplosion1 = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceIceExplosion1");

            _lavaandiceTrumpet = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceTrumpet");

            _lavaandiceTimer1 = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceTimer1");
            _lavaandiceTimer2 = Pax4Sound._current.GetSoundEffect("Sound/lavaandiceTimer2");            
        }       
    }
}