using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Pax.Core;
using System.Runtime.Serialization;
using System.IO;

namespace Pax4.Core
{
    [DataContract]
    [KnownType(typeof(Pax4Modifier))]
    public class Pax4Modifier : Pax4Object
    {
        [DataMember]
        public float _duration0 = 0.0f;

        [DataMember]
        public float _duration = 0.0f;

        [DataMember]
        public float _delay = 0.0f;

        [DataMember]
        public float _timer = 0.0f;

        [DataMember]
        public float _dt = 0.0f;

        [DataMember]
        public bool _oscillating = false;

        [DataMember]
        public bool _roundTrip = false;

        [DataMember]
        public bool _continuous = false;

        [DataMember]
        public bool _done = true;

        [DataMember]
        public int _runCount = 0;

        [DataMember]
        public bool _setState1 = true;

        public Pax4Modifier(String p_name, PaxState p_parent0)
            : base(p_name, p_parent0)
        {
        }

        public override void Update(GameTime gameTime)
        {
            _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            _dt = Math.Abs(_duration - _timer);

            if (_timer <= 0.0f)
            {
                _done = true;
                _runCount++;
            }
        }

        public virtual bool Trigger()
        {
            if (!_done)
                return false;

            _timer = _duration + _delay;
            _done = false;

            return true;
        }

        public virtual void Ini(float p_duration, float p_delay = 0.0f)
        {
            _duration0 = p_duration;
            _duration = p_duration;
            _delay = p_delay;
            _done = true;
        }

        public virtual bool Stop()
        {
            _done = true;
            _duration = _duration0;
            return true;
        }

        public virtual void SetOscillating(bool p_oscillating = true)
        {
            _oscillating = p_oscillating;
            
            _roundTrip = false;
            _continuous = false;
        }

        public virtual void SetContinuous(bool p_continuous = true)
        {
            _continuous = p_continuous;

            _oscillating = false;
            _roundTrip = false;
        }

        public virtual void SetRoundTrip(bool p_roundTrip = true)
        {
            _roundTrip = p_roundTrip;

            _oscillating = true;
            _continuous = false;
        }

        #region serialize

        public override MemoryStream Serialize(bool p_volatile = false)
        {
            return Serialize(this.GetType(), p_volatile);
        }

        #endregion
    }
}