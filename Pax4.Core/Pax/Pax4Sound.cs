using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Runtime.Serialization;
using Pax.Core;
using System.IO;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4Sound))]
    public class Pax4Sound : PaxState
    {   
        #region Class Member
        [IgnoreDataMember]
        public static Pax4Sound _current = null;

        [IgnoreDataMember]
        public Dictionary<String, SoundEffect> _soundEffect = null;
        [IgnoreDataMember]
        public Dictionary<String, Song> _song = null;
        [IgnoreDataMember]
        public Dictionary<String, Song> _stateSong = null;

        [IgnoreDataMember]
        private Song _currentSong = null;

        [IgnoreDataMember]
        public float _delay = 0.0f;
        [IgnoreDataMember]
        public float _maxRunTime = 120.0f;
        [IgnoreDataMember]
        private float _timer = 0.0f;

        //private bool _dx = true;
        #endregion

        public Pax4Sound(String p_name,PaxState p_parent0):
            base(p_name, p_parent0)
        {
            _current = this;

            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume = 0.50f;
        }

        public void Update(GameTime gameTime)
        {
            _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer <= 0.0f)
                PlayRandomSong();
        }

        [Intent(typeof(Pax4Sound), "Reset")]
        public void Reset()
        {
            ResetSong();
            ResetSoundEffect();
        }

        [Intent(typeof(Pax4Sound), "ResetSong")]
        public void ResetSong()
        {
            _song.Clear();
            _stateSong.Clear();
        }

        [Intent(typeof(Pax4Sound), "ResetSoundEffect")]
        public void ResetSoundEffect()
        {
            _soundEffect.Clear();
        }

        public void LoadSong(List<String> p_song = null)
        {
            if(p_song == null)
                return;

            if (_song == null)
                _song = new Dictionary<String, Song>();

            Song song = null;
            for (int i = 0; i < p_song.Count; i++)
            {
                if (_song.ContainsKey(p_song[i]))
                    continue;
                song = Pax4Game._current.Content.Load<Song>(p_song[i]);
                _song.Add(p_song[i], song);
            }
        }

        [Intent(typeof(Pax4Sound), "PlaySoundEffect", typeof(String), "p_song", typeof(bool), "p_repeating")]
        public void PlaySong(String p_song = null, bool p_repeating = false)
        {
            if (p_song == null)
                return;

            if (_song == null)
                return;
            
            Song song = null;
            if (_song.TryGetValue(p_song, out song))
            {
                _currentSong = song;
                MediaPlayer.Play(song);
            }

            if (p_repeating)
                MediaPlayer.IsRepeating = true;

            _timer = _maxRunTime + _delay;
        }

        [Intent(typeof(Pax4Sound), "PlayRandomSong")]
        public void PlayRandomSong()
        {
            if (_song == null)
                return;

            Random rand = new Random();
            int i = rand.Next(0, _song.Count);
            foreach (Song song in _song.Values)
            {
                i--;
                if (i <= 0)
                {
                    _currentSong = song;
                    MediaPlayer.Play(song);
                    _timer = _maxRunTime + _delay;
                    return;
                }
            }
        }

        public void LoadSoundEffect(List<String> p_soundEffect = null)
        {
            if (p_soundEffect == null)
                return;

            if (_soundEffect == null)
                _soundEffect = new Dictionary<String, SoundEffect>();

            SoundEffect soundEffect = null;
            for (int i = 0; i < p_soundEffect.Count; i++)
            {
                if (_soundEffect.ContainsKey(p_soundEffect[i]))
                    continue;
                soundEffect = Pax4Game._current.Content.Load<SoundEffect>(p_soundEffect[i]);
                _soundEffect.Add(p_soundEffect[i], soundEffect);
            }
        }

        [Intent(typeof(Pax4Sound), "PlaySoundEffect", typeof(String), "p_soundEffect")]
        public void PlaySoundEffect(String p_soundEffect)
        {
            if (p_soundEffect == null)
                return;

            if (_soundEffect == null)
                return;
            
            SoundEffect soundEffect = null;
            if (_soundEffect.TryGetValue(p_soundEffect, out soundEffect))            
                soundEffect.Play();            
        }

        public void LoadStateSong(List<String> p_song = null)
        {
            if (p_song == null)
                return;

            if (_stateSong == null)
                _stateSong = new Dictionary<String, Song>();

            Song song = null;
            for (int i = 0; i < p_song.Count; i++)
            {
                if (_stateSong.ContainsKey(p_song[i]))
                    continue;
                song = Pax4Game._current.Content.Load<Song>(p_song[i]);
                _stateSong.Add(p_song[i], song);
            }
        }

        [Intent(typeof(Pax4Sound), "PlayStateSong", typeof(String), "p_song", typeof(bool), "p_repeating")]
        public void PlayStateSong(String p_song = null, bool p_repeating = false)
        {
            if (p_song == null)
                return;

            if (_song == null)
                return;

            Song song = null;
            if (_stateSong.TryGetValue(p_song, out song))
            {
                _currentSong = song;
                MediaPlayer.Play(song);
            }

            if (p_repeating)
                MediaPlayer.IsRepeating = true;

            _timer = _maxRunTime + _delay;
        }

        public SoundEffect GetSoundEffect(String p_soundEffect)
        {
            SoundEffect result = null;
            _soundEffect.TryGetValue(p_soundEffect, out result);            
            return result;
        }

        public override void Dx()
        {
            Reset();
            //_dx = false;

            _soundEffect = null;
            _song = null;
            _stateSong = null;

            _currentSong = null;

            if (this == _current)
                _current = null;

            base.Dx();
        }

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }
    }
}